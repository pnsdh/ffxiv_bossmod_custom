namespace BossMod.Endwalker.Ultimate.TOP;

// note: this is all very tied to LPDU strat
class P5Sigma(BossModule module) : BossComponent(module)
{
    public enum Glitch { Unknown, Mid, Remote }

    public struct PlayerState
    {
        public int Order;
        public int PartnerSlot;
        public bool WaveCannonTarget;
        public Angle SpreadAngle;
    }

    public Glitch ActiveGlitch;
    private readonly TOPConfig _config = Service.Config.Get<TOPConfig>();
    public PlayerState[] Players = Utils.MakeArray(PartyState.MaxPartySize, new PlayerState() { PartnerSlot = -1 });
    private WDir _waveCannonNorthDir;
    private int _numWaveCannonTargets;
    private bool _waveCannonsDone;

    public override void AddHints(int slot, Actor actor, TextHints hints)
    {
        var ps = Players[slot];
        if (ps.Order > 0)
            hints.Add($"Order: {ps.Order}", false);
    }

    public override void AddGlobalHints(GlobalHints hints)
    {
        if (ActiveGlitch != Glitch.Unknown)
            hints.Add($"Glitch: {ActiveGlitch}");
    }

    public override void DrawArenaForeground(int pcSlot, Actor pc)
    {
        var partner = Raid[Players[pcSlot].PartnerSlot];
        if (partner != null)
        {
            var distSq = (partner.Position - pc.Position).LengthSq();
            var range = DistanceRange;
            Arena.AddLine(pc.Position, partner.Position, distSq < range.min * range.min || distSq > range.max * range.max ? ArenaColor.Danger : ArenaColor.Safe);
        }

        foreach (var safeSpot in SafeSpotOffsets(pcSlot))
            Arena.AddCircle(Module.Center + safeSpot, 1, ArenaColor.Safe);
    }

    public override void OnTethered(Actor source, in ActorTetherInfo tether)
    {
        if (tether.ID == (uint)TetherID.PartySynergy)
        {
            if (Raid.TryFindSlot(source.InstanceID, out var s1) && Raid.TryFindSlot(tether.Target, out var s2))
            {
                Players[s1].PartnerSlot = s2;
                Players[s2].PartnerSlot = s1;
            }
        }
    }

    public override void OnStatusGain(Actor actor, in ActorStatus status)
    {
        switch ((SID)status.ID)
        {
            case SID.MidGlitch:
                ActiveGlitch = Glitch.Mid;
                break;
            case SID.RemoteGlitch:
                ActiveGlitch = Glitch.Remote;
                break;
        }
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if ((AID)spell.Action.ID == AID.SigmaWaveCannonAOE)
            _waveCannonsDone = true;
    }

    public override void OnEventIcon(Actor actor, uint iconID, ulong targetID)
    {
        if (!Raid.TryFindSlot(actor, out var slot))
            return;

        // LPDU: 'blue-purple-orange-green' order; 09STOP: 'orange-green-purple-blue', left to right when looking away from the arms
        var order = (IconID)iconID switch
        {
            IconID.PartySynergyCross => _config.P5Sigma09STOP ? 4 : 1,
            IconID.PartySynergySquare => _config.P5Sigma09STOP ? 3 : 2,
            IconID.PartySynergyCircle => _config.P5Sigma09STOP ? 1 : 3,
            IconID.PartySynergyTriangle => _config.P5Sigma09STOP ? 2 : 4,
            _ => 0
        };
        if (order > 0)
        {
            Players[slot].Order = order;
        }
        else if (iconID == (uint)IconID.SigmaWaveCannon)
        {
            Players[slot].WaveCannonTarget = true;
            if (++_numWaveCannonTargets == 6)
                InitSpreadPositions();
        }
    }

    public override void OnActorPlayActionTimelineEvent(Actor actor, ushort id)
    {
        switch ((OID)actor.OID)
        {
            case OID.RightArmUnit: // TODO: can it be left unit instead?..
                if (id == 0x1E43)
                    _waveCannonNorthDir -= actor.Position - Module.Center;
                break;
            case OID.BossP5:
                if (id == 0x1E43)
                    _waveCannonNorthDir = actor.Position - Module.Center; // just in case...
                break;
        }
    }

    private (float min, float max) DistanceRange => ActiveGlitch switch
    {
        Glitch.Mid => (20, 26),
        Glitch.Remote => (34, 50),
        _ => (0, 50)
    };

    private void InitSpreadPositions()
    {
        if (_config.P5Sigma09STOP)
        {
            InitSpreadPositions09STOP();
            return;
        }

        var northAngle = Angle.FromDirection(_waveCannonNorthDir);
        var waveCannonsPerPair = new BitMask[4];
        for (var i = 0; i < Players.Length; ++i)
        {
            var ps = Players[i];
            if (ps.WaveCannonTarget && ps.Order > 0)
                waveCannonsPerPair[ps.Order - 1].Set(i);
        }
        var nextSingle = 0;
        var nextDouble = 0;
        foreach (var mask in waveCannonsPerPair)
        {
            if (mask.NumSetBits() == 2)
            {
                var s1 = mask.LowestSetBit();
                var s2 = mask.HighestSetBit();
                var dir = (Raid[s2]?.Position ?? default) - (Raid[s1]?.Position ?? default); // s1 to s2
                if (_waveCannonNorthDir.OrthoL().Dot(dir) > 0)
                    Utils.Swap(ref s1, ref s2); // s1 is now N/W, s2 is S/E
                if (nextSingle == 0)
                {
                    Players[s1].SpreadAngle = northAngle;
                    Players[s2].SpreadAngle = northAngle + 180.Degrees();
                }
                else
                {
                    Players[s1].SpreadAngle = northAngle + 90.Degrees();
                    Players[s2].SpreadAngle = northAngle - 90.Degrees();
                }
                ++nextSingle;
            }
            else
            {
                var s1 = mask.LowestSetBit();
                var s2 = Players[s1].PartnerSlot;
                if (nextDouble == 0)
                {
                    Players[s1].SpreadAngle = northAngle - 135.Degrees();
                    Players[s2].SpreadAngle = northAngle + 45.Degrees();
                }
                else
                {
                    Players[s1].SpreadAngle = northAngle + 135.Degrees();
                    Players[s2].SpreadAngle = northAngle - 45.Degrees();
                }
                ++nextDouble;
            }
        }

        foreach (ref var p in Players.AsSpan())
            p.SpreadAngle = p.SpreadAngle.Normalized();
    }

    // 09STOP, 'north' = away from the arms (towards omega M): left double pair - upper N, lower S; right double pair - upper W, lower E;
    // left single pair - marker behind the left fist (SW), partner NE; right single pair - marker behind the right fist (SE), partner NW
    private void InitSpreadPositions09STOP()
    {
        var north = _waveCannonNorthDir.Normalized();
        var northAngle = Angle.FromDirection(north);
        var waveCannonsPerPair = new BitMask[4];
        for (var i = 0; i < Players.Length; ++i)
            if (Players[i].WaveCannonTarget && Players[i].Order > 0)
                waveCannonsPerPair[Players[i].Order - 1].Set(i);

        var nextSingle = 0;
        var nextDouble = 0;
        foreach (var mask in waveCannonsPerPair) // in order, i.e. left to right
        {
            if (mask.NumSetBits() == 2)
            {
                var upper = mask.LowestSetBit();
                var lower = mask.HighestSetBit();
                if (north.Dot((Raid[lower]?.Position ?? default) - (Raid[upper]?.Position ?? default)) > 0)
                    Utils.Swap(ref upper, ref lower);
                Players[upper].SpreadAngle = nextDouble == 0 ? northAngle : northAngle + 90.Degrees();
                Players[lower].SpreadAngle = nextDouble == 0 ? northAngle + 180.Degrees() : northAngle - 90.Degrees();
                ++nextDouble;
            }
            else if (mask.Any())
            {
                var marker = mask.LowestSetBit();
                var partner = Players[marker].PartnerSlot;
                Players[marker].SpreadAngle = nextSingle == 0 ? northAngle + 135.Degrees() : northAngle - 135.Degrees();
                if (partner >= 0)
                    Players[partner].SpreadAngle = nextSingle == 0 ? northAngle - 45.Degrees() : northAngle + 45.Degrees();
                ++nextSingle;
            }
        }

        foreach (ref var p in Players.AsSpan())
            p.SpreadAngle = p.SpreadAngle.Normalized();
    }

    private IEnumerable<WDir> SafeSpotOffsets(int slot) => _config.P5Sigma09STOP ? SafeSpotOffsets09STOP(slot) : SafeSpotOffsetsLPDU(slot);

    // 09STOP spots, measured from the 09STOP diagrams
    private IEnumerable<WDir> SafeSpotOffsets09STOP(int slot)
    {
        var p = Players[slot];
        if (_waveCannonNorthDir == default || p.Order == 0)
            yield break; // no safe spots yet

        var north = _waveCannonNorthDir.Normalized();
        if (_numWaveCannonTargets < 6)
        {
            // two rows between the fists, circle-triangle-square-cross from left
            var left = p.Order switch
            {
                1 => 4.3f,
                2 => 1.4f,
                3 => -1.5f,
                _ => -4.3f
            };
            yield return left * north.OrthoL() - 4.4f * north;
            yield return left * north.OrthoL() - 6.9f * north;
            yield break;
        }

        if (!_waveCannonsDone)
        {
            // everyone at the same distance from the center: close - in front of the floor markers, far - 1y from the arena edge
            var radius = ActiveGlitch == Glitch.Mid ? 11.5f : 19;
            yield return radius * p.SpreadAngle.ToDirection();
        }
    }

    private IEnumerable<WDir> SafeSpotOffsetsLPDU(int slot)
    {
        var p = Players[slot];
        if (_waveCannonNorthDir == default)
            yield break; // no safe spots yet

        if (_numWaveCannonTargets < 6)
        {
            var dir = _waveCannonNorthDir.Normalized();
            yield return (20 - 2.5f * p.Order) * dir + 2 * dir.OrthoL();
            yield return (20 - 2.5f * p.Order) * dir + 2 * dir.OrthoR();
            yield break;
        }

        if (!_waveCannonsDone)
        {
            yield return (ActiveGlitch == Glitch.Mid ? 11 : 19) * p.SpreadAngle.ToDirection();
            yield break;
        }
    }
}

class P5SigmaHyperPulse(BossModule module) : Components.BaitAwayTethers(module, new AOEShapeRect(100, 3), (uint)TetherID.SigmaHyperPulse, AID.SigmaHyperPulse)
{
    public override void DrawArenaForeground(int pcSlot, Actor pc)
    {
        foreach (var b in CurrentBaits)
            Arena.Actor(b.Source, ArenaColor.Object, true);
        base.DrawArenaForeground(pcSlot, pc);
    }
}

class P5SigmaWaveCannon(BossModule module) : Components.GenericBaitAway(module, AID.SigmaWaveCannonAOE)
{
    private BitMask _waveCannonTargets;

    private static readonly AOEShapeCone _shapeWaveCannon = new(100, 22.5f.Degrees()); // TODO: verify angle

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID == AID.SigmaWaveCannon)
            foreach (var p in Raid.WithSlot(true).IncludedInMask(_waveCannonTargets).Actors())
                CurrentBaits.Add(new(caster, p, _shapeWaveCannon));
    }

    public override void OnEventIcon(Actor actor, uint iconID, ulong targetID)
    {
        if (iconID == (uint)IconID.SigmaWaveCannon)
            _waveCannonTargets.Set(Raid.FindSlot(actor.InstanceID));
    }
}

class P5SigmaTowers(BossModule module) : Components.GenericTowers(module)
{
    private int _soakerSum;
    private readonly WPos?[] _safeSpots09STOP = new WPos?[PartyState.MaxPartySize];

    public override void DrawArenaForeground(int pcSlot, Actor pc)
    {
        base.DrawArenaForeground(pcSlot, pc);
        if (Towers.Count > 0 && _safeSpots09STOP[pcSlot] is WPos spot)
            Arena.AddCircle(spot, 1, ArenaColor.Safe);
    }

    public override void OnActorCreated(Actor actor)
    {
        var numSoakers = (OID)actor.OID switch
        {
            OID.Tower2 => 1,
            OID.Tower3 => 2,
            _ => 0,
        };
        if (numSoakers == 0)
            return;

        Towers.Add(new(actor.Position, 3, numSoakers, numSoakers));
        _soakerSum += numSoakers;
        if (_soakerSum == PartyState.MaxPartySize)
            InitAssignments();
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if ((AID)spell.Action.ID is AID.StorageViolation1 or AID.StorageViolation2 or AID.StorageViolationObliteration)
        {
            ++NumCasts;
            Towers.Clear();
            Array.Fill(_safeSpots09STOP, null);
        }
    }

    private void InitAssignments()
    {
        var sigma = Module.FindComponent<P5Sigma>();
        if (sigma == null)
            return;
        if (Service.Config.Get<TOPConfig>().P5Sigma09STOP)
        {
            InitAssignments09STOP(sigma);
            return;
        }

        WDir relNorth = default;
        foreach (var t in Towers)
            relNorth -= t.Position - Module.Center;

        foreach (ref var tower in Towers.AsSpan())
        {
            var offset = tower.Position - Module.Center;
            var left = relNorth.OrthoL().Dot(offset) > 0;
            if (Towers.Count == 5)
            {
                // 1's are rel W (A) and E (D), 2's are N (12), SW (B3), SE (C4)
                if (tower.MinSoakers == 1)
                    AssignPlayers(sigma, ref tower, (left ? 180 : -90).Degrees()); // A/D
                else if (relNorth.Dot(offset) > 0)
                    AssignPlayers(sigma, ref tower, 135.Degrees(), 45.Degrees()); // 1/2
                else if (left)
                    AssignPlayers(sigma, ref tower, 90.Degrees(), -45.Degrees()); // B/3
                else
                    AssignPlayers(sigma, ref tower, 0.Degrees(), -135.Degrees()); // C/4
            }
            else
            {
                // 1's are NW (1), NE (2), SW (B), SE (C), 2's are W (A3) and E (D4)
                if (tower.MinSoakers == 2)
                    AssignPlayers(sigma, ref tower, (left ? 180 : -90).Degrees(), (left ? -45 : -135).Degrees()); // A3/D4
                else if (relNorth.Dot(offset) > 0)
                    AssignPlayers(sigma, ref tower, (left ? 135 : 45).Degrees()); // 1/2
                else
                    AssignPlayers(sigma, ref tower, (left ? 90 : 0).Degrees()); // B/C
            }
        }
    }

    // 09STOP macros: each floor marker (09STOP waymark layout: A north, 1 NE, B east, 2 SE, C south, 3 SW, D west, 4 NW) has a macro showing which tower to take
    // in a picture with the 4-tower side on top; so the spread spot is the absolute map direction, the tower is relative to the 4-tower side
    // (compass angles: clockwise in map view; spread = absolute, close/far = relative to the 4-tower side)
    private static readonly (int spread, int close, int far)[] _towers09STOP =
    [
        (0, -27, -42), // A: top left
        (45, -65, -42), // 1: left (close), top left (far)
        (90, 27, 42), // B: top right
        (135, 65, 42), // 2: right (close), top right (far)
        (180, 65, 86), // C: right
        (-135, -151, 180), // 3: bottom left (close), bottom (far)
        (-90, -65, -86), // D: left
        (-45, 151, 180), // 4: bottom right (close), bottom (far)
    ];

    private void InitAssignments09STOP(P5Sigma sigma)
    {
        WDir fourSide = default;
        foreach (var t in Towers)
            fourSide += t.Position - Module.Center;
        var fourAngle = Angle.FromDirection(fourSide);
        var far = Towers.Count == 5;

        foreach (ref var tower in Towers.AsSpan())
            tower.ForbiddenSoakers = new(0xFF);
        var assigned = Utils.MakeArray(sigma.Players.Length, -1);

        for (var i = 0; i < sigma.Players.Length; ++i)
        {
            var spreadAngle = sigma.Players[i].SpreadAngle;
            if (sigma.Players[i].Order == 0)
                continue; // not assigned
            // BossMod angles grow counterclockwise in map view (north = 180), compass angles clockwise (north = 0)
            var spreadCompass = 180 - spreadAngle.Normalized().Deg;
            var entry = _towers09STOP.MinBy(e => Math.Abs((e.spread - spreadCompass + 540) % 360 - 180));
            var target = fourAngle - (far ? entry.far : entry.close).Degrees();
            var best = -1;
            var bestDist = float.MaxValue;
            for (var t = 0; t < Towers.Count; ++t)
            {
                var dist = (Angle.FromDirection(Towers[t].Position - Module.Center) - target).Normalized().Abs().Rad;
                if (dist < bestDist)
                    (best, bestDist) = (t, dist);
            }
            if (best >= 0)
            {
                Towers.AsSpan()[best].ForbiddenSoakers.Clear(i);
                assigned[i] = best;
            }
        }

        // close: stand in the tower center (two soakers share it); far: at the tower edge, as far as possible from the tethered partner's tower
        for (var i = 0; i < assigned.Length; ++i)
        {
            if (assigned[i] < 0)
                continue;
            var pos = Towers[assigned[i]].Position;
            var partner = sigma.Players[i].PartnerSlot;
            if (far && partner >= 0 && assigned[partner] >= 0 && assigned[partner] != assigned[i])
            {
                pos += 2.5f * (pos - Towers[assigned[partner]].Position).Normalized(); // tower radius 3 - hitbox
                var offset = pos - Module.Center;
                if (offset.Length() > 19)
                    pos = Module.Center + 19 * offset.Normalized(); // keep 1y from the arena edge
            }
            _safeSpots09STOP[i] = pos;
        }
    }

    private void AssignPlayers(P5Sigma sigma, ref Tower tower, params Angle[] angles)
    {
        for (var i = 0; i < sigma.Players.Length; ++i)
            if (!angles.Any(a => a.AlmostEqual(sigma.Players[i].SpreadAngle, 0.1f)))
                tower.ForbiddenSoakers.Set(i);
    }
}

class P5SigmaRearLasers(BossModule module) : Components.GenericAOEs(module)
{
    public Angle StartingDir { get; private set; }
    public Angle Rotation { get; private set; }
    private DateTime _activation;

    private static readonly AOEShapeRect _shape = new(25, 6, 25);

    public bool Active => Rotation != default;

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        if (!Active)
            yield break;
        for (var i = NumCasts + 1; i < 14; ++i)
            yield return new(_shape, Module.Center, StartingDir + i * Rotation, _activation.AddSeconds(0.6 * i), Risky: false);
        if (NumCasts < 14)
            yield return new(_shape, Module.Center, StartingDir + NumCasts * Rotation, _activation.AddSeconds(0.6 * NumCasts), ArenaColor.Danger);
    }

    public override void OnEventIcon(Actor actor, uint iconID, ulong targetID)
    {
        if ((OID)actor.OID != OID.RearPowerUnit)
            return;
        var rot = (IconID)iconID switch
        {
            IconID.RotateCW => -9.Degrees(),
            IconID.RotateCCW => 9.Degrees(),
            _ => default
        };
        if (rot == default)
            return;
        StartingDir = actor.Rotation;
        Rotation = rot;
        _activation = WorldState.FutureTime(10.1f);
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if ((AID)spell.Action.ID is AID.RearLasersFirst or AID.RearLasersRest)
            ++NumCasts;
    }
}

class P5SigmaDoubleAOEs(BossModule module) : Components.GenericAOEs(module)
{
    public bool Show;
    public List<AOEInstance> AOEs = [];

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor) => Show ? AOEs : Enumerable.Empty<AOEInstance>();

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if ((AID)spell.Action.ID is AID.SuperliminalSteel or AID.OptimizedBlizzard)
            ++NumCasts;
    }

    public override void OnActorPlayActionTimelineEvent(Actor actor, ushort id)
    {
        if (id != 0x1E43 || (OID)actor.OID != OID.BossP5)
            return;
        if (actor.ModelState.ModelState == 4)
        {
            AOEs.Add(new(new AOEShapeRect(40, 40, -4), actor.Position, actor.Rotation + 90.Degrees(), WorldState.FutureTime(15.1f)));
            AOEs.Add(new(new AOEShapeRect(40, 40, -4), actor.Position, actor.Rotation - 90.Degrees(), WorldState.FutureTime(15.1f)));
        }
        else
        {
            AOEs.Add(new(new AOEShapeCross(100, 5), actor.Position, actor.Rotation, WorldState.FutureTime(15.1f)));
            Show = true; // cross can be shown from the start
        }
    }
}

class P5SigmaNearDistantWorld(BossModule module) : P5NearDistantWorld(module)
{
    private readonly P5SigmaRearLasers? _lasers = module.FindComponent<P5SigmaRearLasers>();
    private BitMask _dynamisStacks;

    public override void DrawArenaForeground(int pcSlot, Actor pc)
    {
        base.DrawArenaForeground(pcSlot, pc);
        foreach (var p in SafeSpots(pcSlot, pc))
            Arena.AddCircle(p, 1, ArenaColor.Safe);
    }

    public override void OnStatusGain(Actor actor, in ActorStatus status)
    {
        base.OnStatusGain(actor, status);
        if ((SID)status.ID == SID.QuickeningDynamis)
            _dynamisStacks.Set(Raid.FindSlot(actor.InstanceID));
    }

    private IEnumerable<WPos> SafeSpots(int slot, Actor actor)
    {
        if (_lasers == null || Service.Config.Get<TOPConfig>().P5Sigma09STOP) // note: we assume StartingDir is relative south, Rotation is +- 9 degrees; LPDU-specific spots
            yield break;

        if (actor == NearWorld)
        {
            yield return Module.Center + 10 * (_lasers.StartingDir + 10 * _lasers.Rotation).ToDirection();
        }
        else if (actor == DistantWorld)
        {
            yield return Module.Center + 10 * _lasers.StartingDir.ToDirection();
        }
        else
        {
            // TODO: figure out a way to assign safespots - for now, assume no-dynamis always go south (and so can be second far baiters or any near baiters), dynamis can go anywhere
            yield return Module.Center + 19 * _lasers.StartingDir.ToDirection(); // '4' - second far bait spot
            yield return Module.Center + 19 * (_lasers.StartingDir + 9 * _lasers.Rotation).ToDirection(); // '2' - first near bait spot
            yield return Module.Center + 19 * (_lasers.StartingDir + 11 * _lasers.Rotation).ToDirection(); // '3' - second near bait spot
            if (_dynamisStacks[slot])
            {
                yield return Module.Center - 19 * _lasers.StartingDir.ToDirection(); // '1' - first far bait spot
                yield return Module.Center - 19 * (_lasers.StartingDir + 5 * _lasers.Rotation).ToDirection(); // first (far) laser bait spot
                yield return Module.Center - 19 * (_lasers.StartingDir - 5 * _lasers.Rotation).ToDirection(); // second (stay) laser bait spot
            }
        }
    }
}
