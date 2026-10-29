using Dalamud;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using FFXIVClientStructs.FFXIV.Client.Graphics.Scene;
using FFXIVClientStructs.FFXIV.Client.Graphics.Vfx;
using FFXIVClientStructs.FFXIV.Client.System.Framework;
using System.Runtime.InteropServices;

namespace BossMod;

// Boss Mod (Custom): records client-spawned VFX (and their removal) plus a clock sync into replays, see ReplayExt:
// omens and static/ground VFX (CreateVfx), VFX attached to non-player actors (on-hit effects etc.) and standalone VFX objects.
// Mechanics without a telegraph are learned from the effect that plays on hit: its file and timing match the hit, which tells the shape apart.
// Each hook is optional: if it can't be found (e.g. after a patch), only that record is skipped and the rest of the replay is unaffected.
// Off by default (it's for working out new fights): hooks are only installed once their option is turned on, and disabled (original game code)
// whenever it's off; with the main option off nothing is added to replays, so they stay readable by the original Boss Mod.
// Not recorded: the transform and owner of standalone VFX objects; their own fields stay identity/unset for mechanic effects and nothing found nearby held them.
public static unsafe class VfxRecorder
{
    // same function DebugVfx calls; StartOmen calls it with a3=2, a4=0, a13=-1
    private delegate VfxData* CreateVfxDelegate(byte* path, VfxInitData* init, byte a3, byte a4, float originX, float originY, float originZ, float sizeX, float sizeY, float sizeZ, float angle, float duration, int a13);
    private delegate void ClearVfxDelegate(VfxData* self);

    // actor VFX aren't in ClientStructs: signature and arguments as in VFXEditor's ActorVfxCreate and ECommons' ActorVfx.CreateSig (both agree),
    // destructor as in ECommons' ActorVfx.DtorSig (VFXEditor's ActorVfxRemove takes the same (vfx, flags) arguments)
    private delegate VfxObject* ActorVfxCreateDelegate(byte* path, nint caster, nint target, float a4, byte a5, ushort a6, byte a7);
    private delegate nint ActorVfxDtorDelegate(VfxObject* self, byte flags);

    // standalone VFX objects: create from ClientStructs (VfxObject.Create), removal as in VFXEditor's StaticVfxRemove
    private delegate VfxObject* ObjectVfxCreateDelegate(byte* path, byte* pool);
    private delegate nint ObjectVfxRemoveDelegate(VfxObject* self);

    private const string ActorVfxCreateSig = "40 53 55 56 57 48 81 EC ?? ?? ?? ?? 0F 29 B4 24 ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 0F B6 AC 24 ?? ?? ?? ?? 0F 28 F3 49 8B F8";
    private const string ActorVfxDtorSig = "48 89 5C 24 ?? 57 48 83 EC ?? 48 8D 05 ?? ?? ?? ?? 48 8B D9 48 89 01 8B FA 48 8D 05 ?? ?? ?? ?? 48 89 81 ?? ?? ?? ?? 48 8B 89 ?? ?? ?? ?? 48 85 C9 74 ?? 48 8B 01 48 8B D3";
    private const string ObjectVfxRemoveSig = "40 53 48 83 EC 20 48 8B D9 48 8B 89 ?? ?? ?? ?? 48 85 C9 74 28 33 D2 E8 ?? ?? ?? ?? 48 8B 8B ?? ?? ?? ?? 48 85 C9";

    private static HookAddress<CreateVfxDelegate>? _createHook;
    private static HookAddress<ClearVfxDelegate>? _clearHook;
    private static HookAddress<ActorVfxCreateDelegate>? _actorCreateHook;
    private static HookAddress<ActorVfxDtorDelegate>? _actorDtorHook;
    private static HookAddress<ObjectVfxCreateDelegate>? _objectCreateHook;
    private static HookAddress<ObjectVfxRemoveDelegate>? _objectRemoveHook;
    private static bool _omenHooksTried;
    private static bool _actorHooksTried;
    private static bool _objectHooksTried;

    // detours call these rather than hook.Original: a game thread may still be inside a detour while the plugin unloads and the hooks get disposed
    private static CreateVfxDelegate? _createOriginal;
    private static ClearVfxDelegate? _clearOriginal;
    private static ActorVfxCreateDelegate? _actorCreateOriginal;
    private static ActorVfxDtorDelegate? _actorDtorOriginal;
    private static ObjectVfxCreateDelegate? _objectCreateOriginal;
    private static ObjectVfxRemoveDelegate? _objectRemoveOriginal;

    private enum Group : byte { Omen, Actor, Object }
    private static readonly bool[] _groupEnabled = new bool[3];

    private static readonly System.Threading.Lock _lock = new();
    private static readonly List<WorldState.Operation> _pending = [];
    private static readonly Dictionary<ulong, Group> _live = []; // handles we recorded, so removals of other VFX aren't logged
    private static readonly Dictionary<string, uint> _pathIds = [];

    private static bool _syncRequested = true;
    private static long _lastServerMinute;

    public static void RequestClockSync() => _syncRequested = true;

    // called every frame after the world state update
    public static void Flush(WorldState ws)
    {
        if (Service.IsMock)
            return;
        UpdateHooks();

        List<WorldState.Operation> ops;
        lock (_lock)
        {
            ops = [.. _pending];
            _pending.Clear();
        }
        foreach (var op in ops)
            ws.Execute(op);

        if (!Enabled)
            return;
        var serverTime = ServerTime();
        var minute = serverTime / 60;
        if (_syncRequested || serverTime != 0 && minute != _lastServerMinute)
        {
            _syncRequested = false;
            _lastServerMinute = minute;
            ws.Execute(new ReplayExt.OpClock(ws.CurrentTime, DateTime.UtcNow, serverTime));
        }
    }

    public static void Dispose()
    {
        _createHook?.Dispose();
        _clearHook?.Dispose();
        _actorCreateHook?.Dispose();
        _actorDtorHook?.Dispose();
        _objectCreateHook?.Dispose();
        _objectRemoveHook?.Dispose();
    }

    // while the plugin is paused (a conflicting fork is running) nothing is recorded: hooks go back to the original game code and nothing queues up
    public static void Suspend()
    {
        for (var g = Group.Omen; g <= Group.Object; ++g)
            SetGroup(g, false);
        lock (_lock)
            _pending.Clear();
    }

    private static void UpdateHooks()
    {
        var config = Service.Config.Get<ReplayManagementConfig>();
        if (config.RecordVfx && !_omenHooksTried)
        {
            _omenHooksTried = true;
            _createHook = TryHook(() => new HookAddress<CreateVfxDelegate>("E8 ?? ?? ?? ?? 48 8B D8 48 8D 95", CreateVfxDetour, false), "omen/static VFX create");
            _clearHook = TryHook(() => new HookAddress<ClearVfxDelegate>("E8 ?? ?? ?? ?? 4D 89 A4 DE ?? ?? ?? ??", ClearVfxDetour, false), "omen/static VFX clear");
            _createOriginal = _createHook?.Original;
            _clearOriginal = _clearHook?.Original;
        }
        SetGroup(Group.Omen, config.RecordVfx);

        var actor = config.RecordVfx && config.RecordActorVfx;
        if (actor && !_actorHooksTried)
        {
            _actorHooksTried = true;
            _actorCreateHook = TryHook(() => new HookAddress<ActorVfxCreateDelegate>(ActorVfxCreateSig, ActorVfxCreateDetour, false), "actor VFX create");
            _actorDtorHook = TryHook(() => new HookAddress<ActorVfxDtorDelegate>(ActorVfxDtorSig, ActorVfxDtorDetour, false), "actor VFX destroy");
            _actorCreateOriginal = _actorCreateHook?.Original;
            _actorDtorOriginal = _actorDtorHook?.Original;
        }
        SetGroup(Group.Actor, actor);

        var objects = config.RecordVfx && config.RecordVfxObjects;
        if (objects && !_objectHooksTried)
        {
            _objectHooksTried = true;
            _objectCreateHook = TryHook(() => new HookAddress<ObjectVfxCreateDelegate>(VfxObject.Addresses.Create.Value, ObjectVfxCreateDetour, false), "VFX object create");
            _objectRemoveHook = TryHook(() => new HookAddress<ObjectVfxRemoveDelegate>(ObjectVfxRemoveSig, ObjectVfxRemoveDetour, false), "VFX object remove");
            _objectCreateOriginal = _objectCreateHook?.Original;
            _objectRemoveOriginal = _objectRemoveHook?.Original;
        }
        SetGroup(Group.Object, objects);
    }

    private static void SetGroup(Group group, bool enabled)
    {
        if (_groupEnabled[(int)group] == enabled)
            return;
        _groupEnabled[(int)group] = enabled;
        switch (group)
        {
            case Group.Omen:
                SetEnabled(_createHook, enabled);
                SetEnabled(_clearHook, enabled);
                break;
            case Group.Actor:
                SetEnabled(_actorCreateHook, enabled);
                SetEnabled(_actorDtorHook, enabled);
                break;
            case Group.Object:
                SetEnabled(_objectCreateHook, enabled);
                SetEnabled(_objectRemoveHook, enabled);
                break;
        }
        if (!enabled)
        {
            // their removals won't be seen anymore: forget them, so a reused address can't produce a removal for an old handle
            lock (_lock)
                foreach (var (h, _) in _live.Where(kv => kv.Value == group).ToList())
                    _live.Remove(h);
        }
    }

    private static void SetEnabled<T>(HookAddress<T>? hook, bool enabled) where T : Delegate
    {
        if (hook != null && hook.Enabled != enabled)
            hook.Enabled = enabled;
    }

    private static HookAddress<T>? TryHook<T>(Func<HookAddress<T>> create, string what) where T : Delegate
    {
        try
        {
            return create();
        }
        catch (Exception ex)
        {
            Service.Log($"[Custom] {what} hook not available, VFX of that kind won't be recorded: {ex.Message}");
            return null;
        }
    }

    private static long ServerTime()
    {
        try
        {
            return Framework.GetServerTime();
        }
        catch (Exception)
        {
            return 0;
        }
    }

    private static bool Enabled => Service.Config.Get<ReplayManagementConfig>().RecordVfx;

    private static uint PathId(string path)
    {
        if (!_pathIds.TryGetValue(path, out var id))
            _pathIds[path] = id = (uint)_pathIds.Count + 1;
        return id;
    }

    private static void Track(ulong handle, Group group)
    {
        if (_live.Count > 20000)
            _live.Clear(); // safety net in case some removals don't go through the hooks; removals of handles forgotten here aren't recorded
        if (handle != 0)
            _live[handle] = group;
    }

    // everything read from game objects in the actor/object hooks goes through SafeMemory: a bad guess about an argument then can't crash the game
    private static bool SafeRead<T>(nint address, out T value) where T : unmanaged
    {
        value = default;
        if (address < 0x10000 || !SafeMemory.ReadBytes(address, sizeof(T), out var bytes))
            return false;
        value = MemoryMarshal.Read<T>(bytes);
        return true;
    }

    private static string SafeString(nint address) => address >= 0x10000 ? SafeMemory.ReadString(address, 260) ?? "" : "";

    // GameObject: EntityId at 0x78, ObjectKind at 0x90, SubKind at 0x91
    private static (ulong id, ObjectKind kind, BattleNpcSubKind subKind) SafeActor(nint obj)
        => SafeRead<uint>(obj + 0x78, out var id) && SafeRead<byte>(obj + 0x90, out var kind) && SafeRead<byte>(obj + 0x91, out var sub)
            ? (id != 0xE0000000 ? id : 0, (ObjectKind)kind, (BattleNpcSubKind)sub) : (0, ObjectKind.None, BattleNpcSubKind.None);

    // party skills make up most actor VFX and are known anyway
    private static bool IsPlayerSide((ulong id, ObjectKind kind, BattleNpcSubKind subKind) a) => a.kind is ObjectKind.Pc or ObjectKind.Companion
        || a.kind == ObjectKind.BattleNpc && a.subKind is BattleNpcSubKind.Pet or BattleNpcSubKind.Buddy;

    private static VfxData* CreateVfxDetour(byte* path, VfxInitData* init, byte a3, byte a4, float originX, float originY, float originZ, float sizeX, float sizeY, float sizeZ, float angle, float duration, int a13)
    {
        var res = _createOriginal!(path, init, a3, a4, originX, originY, originZ, sizeX, sizeY, sizeZ, angle, duration, a13);
        try
        {
            if (Enabled && path != null)
            {
                var p = Marshal.PtrToStringUTF8((nint)path) ?? "";
                var initBytes = init != null ? new ReadOnlySpan<byte>(init, sizeof(VfxInitData)).ToArray() : [];
                lock (_lock)
                {
                    var kind = (byte)(p.StartsWith("vfx/omen/", StringComparison.OrdinalIgnoreCase) ? 2 : 1);
                    _pending.Add(new ReplayExt.OpVfxCreate(PathId(p), p, kind, a3, a4, a13, 0, 0, new(originX, originY, originZ), angle, new(sizeX, sizeY, sizeZ), duration, (ulong)res, initBytes));
                    Track((ulong)res, Group.Omen);
                }
            }
        }
        catch (Exception ex)
        {
            Service.Log($"[Custom] failed to record VFX: {ex.Message}");
        }
        return res;
    }

    private static void ClearVfxDetour(VfxData* self)
    {
        RecordRemoval((ulong)self);
        _clearOriginal!(self);
    }

    private static VfxObject* ActorVfxCreateDetour(byte* path, nint caster, nint target, float a4, byte a5, ushort a6, byte a7)
    {
        var res = _actorCreateOriginal!(path, caster, target, a4, a5, a6, a7);
        try
        {
            var casterInfo = SafeActor(caster);
            if (Enabled && !IsPlayerSide(casterInfo))
            {
                var p = SafeString((nint)path);
                var targetInfo = SafeActor(target);
                lock (_lock)
                {
                    _pending.Add(new ReplayExt.OpVfxActorCreate(PathId(p), p, casterInfo.id, targetInfo.id, a4, a5, a6, a7, (ulong)res, (ulong)caster, (ulong)target));
                    Track((ulong)res, Group.Actor);
                }
            }
        }
        catch (Exception ex)
        {
            Service.Log($"[Custom] failed to record actor VFX: {ex.Message}");
        }
        return res;
    }

    private static nint ActorVfxDtorDetour(VfxObject* self, byte flags)
    {
        RecordRemoval((ulong)self);
        return _actorDtorOriginal!(self, flags);
    }

    private static VfxObject* ObjectVfxCreateDetour(byte* path, byte* pool)
    {
        var res = _objectCreateOriginal!(path, pool);
        try
        {
            if (Enabled)
            {
                var p = SafeString((nint)path);
                var poolName = SafeString((nint)pool);
                lock (_lock)
                {
                    _pending.Add(new ReplayExt.OpVfxObjectCreate(PathId(p), p, PathId(poolName), poolName, (ulong)res));
                    Track((ulong)res, Group.Object);
                }
            }
        }
        catch (Exception ex)
        {
            Service.Log($"[Custom] failed to record VFX object: {ex.Message}");
        }
        return res;
    }

    private static nint ObjectVfxRemoveDetour(VfxObject* self)
    {
        RecordRemoval((ulong)self);
        return _objectRemoveOriginal!(self);
    }

    private static void RecordRemoval(ulong handle)
    {
        try
        {
            lock (_lock)
            {
                if (_live.Remove(handle))
                    _pending.Add(new ReplayExt.OpVfxRemove(handle));
            }
        }
        catch (Exception ex)
        {
            Service.Log($"[Custom] failed to record VFX removal: {ex.Message}");
        }
    }
}
