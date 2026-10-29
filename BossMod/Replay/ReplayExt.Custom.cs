using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace BossMod;

// Boss Mod (Custom): replay records that upstream doesn't have, kept out of upstream's version numbering.
// Every record is 'XEXT' + inner tag (uint32) + payload (int32 length + bytes, written with BinaryWriter, little endian),
// so readers can skip inner tags they don't know and new ones can be added later without breaking old readers.
// The payload layout is the same in binary and text replays (text writes the tag as a number and the payload as hex).
public static class ReplayExt
{
    public static readonly FourCC Tag = new("XEXT"u8);
    public static readonly FourCC TagVfxPath = new("VFXP"u8);
    public static readonly FourCC TagVfxCreate = new("VFXC"u8);
    public static readonly FourCC TagVfxActor = new("VFXB"u8); // not VFXA: an earlier test build wrote VFXA with another layout
    public static readonly FourCC TagVfxObject = new("VFXO"u8);
    public static readonly FourCC TagVfxRemove = new("VFXR"u8);
    public static readonly FourCC TagClock = new("UTCS"u8);

    public static void Write(ReplayRecorder.Output output, FourCC inner, byte[] payload) => output.EmitFourCC("XEXT"u8).Emit(inner.Value).Emit(payload);

    public static byte[] Build(Action<BinaryWriter> fill)
    {
        using var ms = new MemoryStream();
        using (var w = new BinaryWriter(ms, Encoding.UTF8, true))
            fill(w);
        return ms.ToArray();
    }

    // records read back from a replay are turned back into their typed ops, with paths resolved through the file's VFXP entries (which aren't kept as ops),
    // so re-saving a replay (or a part of it: split log, isolated encounter) declares the paths it uses again;
    // VFXP entries are per parser, as path ids are only meaningful within the file they were read from
    private static readonly ConditionalWeakTable<object, Dictionary<uint, string>> _parsedPaths = [];

    public static WorldState.Operation? Parse(object parser, FourCC inner, byte[] payload)
    {
        try
        {
            var paths = _parsedPaths.GetOrCreateValue(parser);
            string PathOf(uint id) => paths.GetValueOrDefault(id, "");
            using var r = new BinaryReader(new MemoryStream(payload), Encoding.UTF8);
            if (inner == TagVfxPath)
            {
                paths[r.ReadUInt32()] = r.ReadString();
                return null;
            }
            if (inner == TagVfxCreate && payload.Length >= 69)
            {
                var pathId = r.ReadUInt32();
                var kind = r.ReadByte();
                var a3 = r.ReadByte();
                var a4 = r.ReadByte();
                var a13 = r.ReadInt32();
                var owner = r.ReadUInt64();
                var target = r.ReadUInt64();
                var pos = new Vector3(r.ReadSingle(), r.ReadSingle(), r.ReadSingle());
                var yaw = r.ReadSingle();
                var scale = new Vector3(r.ReadSingle(), r.ReadSingle(), r.ReadSingle());
                var duration = r.ReadSingle();
                var handle = r.ReadUInt64();
                var init = r.ReadBytes(r.ReadUInt16());
                return new OpVfxCreate(pathId, PathOf(pathId), kind, a3, a4, a13, owner, target, pos, yaw, scale, duration, handle, init);
            }
            if (inner == TagVfxActor && payload.Length >= 52)
            {
                var pathId = r.ReadUInt32();
                return new OpVfxActorCreate(pathId, PathOf(pathId), r.ReadUInt64(), r.ReadUInt64(), r.ReadSingle(), r.ReadByte(), r.ReadUInt16(), r.ReadByte(), r.ReadUInt64(), r.ReadUInt64(), r.ReadUInt64());
            }
            if (inner == TagVfxObject && payload.Length >= 16)
            {
                // test builds wrote extra fields after these, which are dropped
                var pathId = r.ReadUInt32();
                var poolId = r.ReadUInt32();
                return new OpVfxObjectCreate(pathId, PathOf(pathId), poolId, PathOf(poolId), r.ReadUInt64());
            }
            if (inner == TagVfxRemove && payload.Length >= 8)
                return new OpVfxRemove(r.ReadUInt64());
            if (inner == TagClock && payload.Length >= 24)
                return new OpClock(new(r.ReadInt64()), new(r.ReadInt64(), DateTimeKind.Utc), r.ReadInt64());
        }
        catch (EndOfStreamException)
        {
        }
        return new OpRaw(inner, payload); // unknown or malformed (e.g. records of earlier test builds): kept as-is
    }

    // a record that isn't understood: kept as-is, so re-saving a replay keeps it
    public sealed record class OpRaw(FourCC Inner, byte[] Payload) : WorldState.Operation
    {
        public readonly byte[] Payload = Payload;

        protected override void Exec(WorldState ws) { }
        public override void Write(ReplayRecorder.Output output) => ReplayExt.Write(output, Inner, Payload);
    }

    // VFXP: uint32 id, string (7-bit encoded length + UTF-8); a path (or pool name) dictionary entry, written into a file right before the first record that uses the id
    // VFXC: uint32 path id, uint8 kind (1 = static/ground, 2 = omen), uint8 a3, uint8 a4, int32 a13 (raw CreateVfx arguments),
    //       uint64 owner instance id (0 = unknown), uint64 target instance id (0 = none),
    //       float x, y, z, float yaw (radians), float scale x, y, z, float duration, uint64 handle (VfxData address),
    //       uint16 init data length + init data bytes (raw VfxInitData, for finding the owner later)
    public sealed record class OpVfxCreate(uint PathId, string Path, byte Kind, byte A3, byte A4, int A13, ulong Owner, ulong Target, Vector3 Position, float Yaw, Vector3 Scale, float Duration, ulong Handle, byte[] Init) : WorldState.Operation
    {
        public readonly byte[] Init = Init;

        protected override void Exec(WorldState ws) { }
        public override void Write(ReplayRecorder.Output output)
        {
            DeclarePath(output, PathId, Path, Timestamp);
            ReplayExt.Write(output, TagVfxCreate, Build(w =>
            {
                w.Write(PathId);
                w.Write(Kind);
                w.Write(A3);
                w.Write(A4);
                w.Write(A13);
                w.Write(Owner);
                w.Write(Target);
                w.Write(Position.X);
                w.Write(Position.Y);
                w.Write(Position.Z);
                w.Write(Yaw);
                w.Write(Scale.X);
                w.Write(Scale.Y);
                w.Write(Scale.Z);
                w.Write(Duration);
                w.Write(Handle);
                w.Write((ushort)Init.Length);
                w.Write(Init);
            }));
        }
    }

    // VFXB: uint32 path id, uint64 caster instance id, uint64 target instance id (0 = none or unreadable),
    //       float a4, uint8 a5, uint16 a6, uint8 a7 (raw ActorVfxCreate arguments), uint64 handle (VfxObject address),
    //       uint64 raw caster pointer, uint64 raw target pointer
    // an effect attached to an actor that isn't a player, pet, chocobo or minion
    public sealed record class OpVfxActorCreate(uint PathId, string Path, ulong Caster, ulong Target, float A4, byte A5, ushort A6, byte A7, ulong Handle, ulong RawCaster, ulong RawTarget) : WorldState.Operation
    {
        protected override void Exec(WorldState ws) { }
        public override void Write(ReplayRecorder.Output output)
        {
            DeclarePath(output, PathId, Path, Timestamp);
            ReplayExt.Write(output, TagVfxActor, Build(w =>
            {
                w.Write(PathId);
                w.Write(Caster);
                w.Write(Target);
                w.Write(A4);
                w.Write(A5);
                w.Write(A6);
                w.Write(A7);
                w.Write(Handle);
                w.Write(RawCaster);
                w.Write(RawTarget);
            }));
        }
    }

    // VFXO: uint32 path id, uint32 pool name id (both in the VFXP dictionary), uint64 handle (VfxObject address)
    // a standalone VFX object (VfxObject.Create), e.g. mechanic effects spawned by action timelines and background layout effects (see the pool name)
    public sealed record class OpVfxObjectCreate(uint PathId, string Path, uint PoolId, string Pool, ulong Handle) : WorldState.Operation
    {
        protected override void Exec(WorldState ws) { }
        public override void Write(ReplayRecorder.Output output)
        {
            DeclarePath(output, PathId, Path, Timestamp);
            DeclarePath(output, PoolId, Pool, Timestamp);
            ReplayExt.Write(output, TagVfxObject, Build(w =>
            {
                w.Write(PathId);
                w.Write(PoolId);
                w.Write(Handle);
            }));
        }
    }

    // VFXR: uint64 handle (matches a VFXC, VFXB or VFXO handle)
    public sealed record class OpVfxRemove(ulong Handle) : WorldState.Operation
    {
        protected override void Exec(WorldState ws) { }
        public override void Write(ReplayRecorder.Output output) => ReplayExt.Write(output, TagVfxRemove, Build(w => w.Write(Handle)));
    }

    // UTCS: int64 replay clock (DateTime ticks, same clock as every other op), int64 local UTC (DateTime ticks), int64 server time (unix seconds, 0 = unknown);
    // written when recording starts and whenever the server clock crosses a minute, so the server second boundary is within one frame
    public sealed record class OpClock(DateTime ReplayTime, DateTime LocalUtc, long ServerTime) : WorldState.Operation
    {
        protected override void Exec(WorldState ws) { }
        public override void Write(ReplayRecorder.Output output) => ReplayExt.Write(output, TagClock, Build(w =>
        {
            w.Write(ReplayTime.Ticks);
            w.Write(LocalUtc.Ticks);
            w.Write(ServerTime);
        }));
    }

    private static readonly ConditionalWeakTable<ReplayRecorder.Output, HashSet<uint>> _declaredPaths = [];

    // ids are global for the session, but every file has to declare the ones it uses
    private static void DeclarePath(ReplayRecorder.Output output, uint id, string path, DateTime timestamp)
    {
        if (!_declaredPaths.GetOrCreateValue(output).Add(id))
            return;
        Write(output, TagVfxPath, Build(w =>
        {
            w.Write(id);
            w.Write(path);
        }));
        output.EndEntry();
        output.StartEntry(timestamp);
    }
}
