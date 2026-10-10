namespace BossMod.Endwalker.Ultimate.TOP;

class P3OversampledWaveCannon(BossModule module) : BossComponent(module)
{
    private Actor? _boss;
    private Angle _bossAngle;
    private readonly Angle[] _playerAngles = new Angle[PartyState.MaxPartySize];
    private readonly int[] _playerOrder = new int[PartyState.MaxPartySize];
    private int _numPlayerAngles;
    private readonly List<int> _monitorOrder = [];
    private readonly TOPConfig _config = Service.Config.Get<TOPConfig>();

    private DateTime _resolve;
    private readonly ArcList[] _safeAngles = Utils.GenArray(PartyState.MaxPartySize, () => new ArcList(default, 50));

    private static readonly AOEShapeCone _shape = new(40, 90.Degrees());

    public override void AddHints(int slot, Actor actor, TextHints hints)
    {
        if (_playerOrder[slot] != 0)
            hints.Add($"Order: {(IsMonitor(slot) != default ? "M" : "N")}{_playerOrder[slot]}", false);

        var numHitBy = AOEs(slot).Count(a => !a.source && _shape.Check(actor.Position, a.origin, a.rot));
        if (numHitBy != 1)
            hints.Add($"Hit by {numHitBy} monitors!");
    }

    public override void AddMovementHints(int slot, Actor actor, MovementHints movementHints)
    {
        foreach (var p in SafeSpots(slot).Where(p => p.assigned))
            movementHints.Add(actor.Position, p.pos, ArenaColor.Safe);
    }

    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        hints.PredictedDamage.Add(new(Raid.WithSlot().Mask(), _resolve, AIHints.PredictedDamageType.Raidwide));

        if (!IsMonitor(slot) || !_config.P3MonitorForbiddenDirections)
            return;

        if (_config.P3Monitors09STOP)
        {
            // 09STOP: face the middle of the widest cleave arc that hits exactly the intended pair (M1 -> M2+M3, M2 -> N1+M1, M3 -> N4+N5),
            // based on actual positions, or on the assigned spots if nobody is in place yet
            var cleave = BestCleave09STOP(slot, actual: true) ?? BestCleave09STOP(slot, actual: false);
            if (cleave != null)
            {
                var facing = cleave.Value - _playerAngles[slot];
                hints.ForbiddenDirections.Add(((facing + 180.Degrees()).Normalized(), 160.Degrees(), _resolve));
            }
            return;
        }

        var safeCW = _bossAngle.Rad > 0;

        var targetCW = (_config.P3LastMonitorSouth, _playerOrder[slot]) switch
        {
            (_, 1) => safeCW,
            (_, 3) => !safeCW,
            (true, 2) => !safeCW,
            (false, 2) => safeCW,
            _ => false
        };

        var al = _safeAngles[slot];
        al.Forbidden.Clear();
        al.Center = actor.Position;

        var safeConePlayers = Raid.WithoutSlot().ClockOrder(actor, Arena.Center, !targetCW).Skip(2).Take(2).ToList();
        if (targetCW)
            safeConePlayers.Reverse();

        var angleRight = actor.AngleTo(safeConePlayers[0]);
        var angleLeft = actor.AngleTo(safeConePlayers[1]);

        // forbid angle ranges that don't face the player toward or away from their intended targets
        al.ForbidArc(angleLeft, (angleRight + 180.Degrees()).Normalized());
        al.ForbidArc((angleLeft + 180.Degrees()).Normalized(), angleRight);

        // forbid any angle that would hit the boss with the monitor; eliminates one of the two remaining facing cones
        var dirToUnsafeCleave = actor.DirectionTo(Arena.Center).ToAngle() - _playerAngles[slot];
        al.ForbidArc(dirToUnsafeCleave - 90.Degrees(), dirToUnsafeCleave + 90.Degrees());

        foreach (var (min, max) in al.Forbidden.Segments)
            hints.ForbiddenDirections.Add((((max + min) / 2).Radians(), ((max - min) / 2).Radians() + 1.Degrees(), _resolve));
    }

    public override void DrawArenaBackground(int pcSlot, Actor pc)
    {
        foreach (var a in AOEs(pcSlot))
        {
            if (a.source)
                _shape.Outline(Arena, a.origin, a.rot, ArenaColor.Danger);
            else
                _shape.Draw(Arena, a.origin, a.rot, a.safe ? ArenaColor.SafeFromAOE : ArenaColor.AOE);
        }
    }

    public override void DrawArenaForeground(int pcSlot, Actor pc)
    {
        foreach (var p in SafeSpots(pcSlot))
            Arena.AddCircle(p.pos, 1, p.assigned ? ArenaColor.Safe : ArenaColor.Danger);
    }

    public override void OnStatusGain(Actor actor, in ActorStatus status)
    {
        var angle = (SID)status.ID switch
        {
            SID.OversampledWaveCannonLoadingL => 90.Degrees(),
            SID.OversampledWaveCannonLoadingR => -90.Degrees(),
            _ => default
        };
        if (angle != default && Raid.TryFindSlot(actor.InstanceID, out var slot))
        {
            _playerAngles[slot] = angle;
            if (++_numPlayerAngles == 3)
            {
                int n = 0, m = 0;
                foreach (var sg in Service.Config.Get<TOPConfig>().P3MonitorsAssignments.Resolve(Raid).OrderBy(sg => sg.group))
                {
                    _playerOrder[sg.slot] = IsMonitor(sg.slot) ? ++m : ++n;
                    if (IsMonitor(sg.slot))
                        _monitorOrder.Add(sg.slot);
                }
            }
        }
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        var angle = (AID)spell.Action.ID switch
        {
            AID.OversampledWaveCannonL => 90.Degrees(),
            AID.OversampledWaveCannonR => -90.Degrees(),
            _ => default
        };
        if (angle != default)
        {
            _boss = caster;
            _bossAngle = angle;
            _resolve = Module.CastFinishAt(spell);
        }
    }

    private bool IsMonitor(int slot) => _playerAngles[slot] != default;

    private IEnumerable<(WPos pos, bool assigned)> SafeSpots(int slot)
    {
        if (_numPlayerAngles < 3 || _bossAngle == default)
            yield break;

        WPos adjust(float x, float z) => Module.Center + new WDir(_bossAngle.Rad < 0 ? -x : x, z);
        if (_config.P3Monitors09STOP)
        {
            var isMonitor = IsMonitor(slot);
            var numSpots = isMonitor ? _monitorSpots09STOP.Length : _otherSpots09STOP.Length;
            for (var i = 1; i <= numSpots; ++i)
                yield return (Spot09STOP(isMonitor, i), _playerOrder[slot] == i);
            yield break;
        }
        if (IsMonitor(slot))
        {
            var nextSlot = 0;
            if (!_config.P3LastMonitorSouth)
                yield return (adjust(10, -11), _playerOrder[slot] == ++nextSlot);
            yield return (adjust(-11, -9), _playerOrder[slot] == ++nextSlot);
            yield return (adjust(-11, +9), _playerOrder[slot] == ++nextSlot);
            if (_config.P3LastMonitorSouth)
                yield return (adjust(10, 11), _playerOrder[slot] == ++nextSlot);
        }
        else
        {
            var nextSlot = 0;
            yield return (adjust(1, -15), _playerOrder[slot] == ++nextSlot);
            if (_config.P3LastMonitorSouth)
                yield return (adjust(10, -11), _playerOrder[slot] == ++nextSlot);
            yield return (adjust(15, -4), _playerOrder[slot] == ++nextSlot);
            yield return (adjust(15, +4), _playerOrder[slot] == ++nextSlot);
            if (!_config.P3LastMonitorSouth)
                yield return (adjust(10, 11), _playerOrder[slot] == ++nextSlot);
            yield return (adjust(1, 15), _playerOrder[slot] == ++nextSlot);
        }
    }

    private IEnumerable<(WPos origin, Angle rot, bool safe, bool source)> AOEs(int slot)
    {
        var isMonitor = IsMonitor(slot);
        int order;
        if (_config.P3Monitors09STOP)
        {
            order = (isMonitor, _playerOrder[slot]) switch
            {
                (true, 1) => 2, // M1 is hit by M2
                (true, _) => 1, // M2/M3 are hit by M1
                (_, 1) => 2, // N1 is hit by M2
                (_, 2 or 3) => 0, // N2/N3 are hit by boss
                _ => 3, // N4/N5 are hit by M3
            };
        }
        else if (_config.P3LastMonitorSouth)
        {
            // NA strat, M3 is SE/SW
            order = (isMonitor, _playerOrder[slot]) switch
            {
                (true, 1 or 2) => 0, // M1/M2 are hit by boss
                (true, 3) => 2, // M3 is hit by M2

                (_, 1 or 2) => 1, // N1/N2 are hit by M1
                (_, 3 or 4) => 3, // N3/N4 are hit by M3
                _ => 2 // N5 is hit by M2
            };
        }
        else
        {
            // EU strat, M1 is NE/NW
            order = (isMonitor, _playerOrder[slot]) switch
            {
                (_, 1) => 2, // N1/M1 are hit by M2
                (true, _) => 0, // M2/M3 are hit by boss
                (_, 2 or 3) => 1, // N2/N3 are hit by M1
                _ => 3, // N4/N5 are hit by M3
            };
        }
        foreach (var aoe in AOEs())
            if (aoe.origin != null)
                yield return (aoe.origin.Position, aoe.origin.Rotation + aoe.offset, aoe.order == order, isMonitor && aoe.order == _playerOrder[slot]);
    }

    // 09STOP spots for boss monitor on the left (west cleave), mirrored for the right; each moved at most 3y from the 09STOP diagram so that every
    // monitor keeps ~+-19 degrees of facing tolerance (~+-14 if everyone stands 1.5y off), and N1/N4/N5 stay ~3y clear of the boss cleave
    private static readonly (float x, float z)[] _monitorSpots09STOP = [(7.5f, -11), (13.5f, -6.5f), (15, 1.5f)];
    private static readonly (float x, float z)[] _otherSpots09STOP = [(3, -17), (-4, -1), (-14, -1), (3, 10), (3, 14.5f)];

    private WPos Spot09STOP(bool monitor, int order)
    {
        var (x, z) = (monitor ? _monitorSpots09STOP : _otherSpots09STOP)[order - 1];
        return Module.Center + new WDir(_bossAngle.Rad < 0 ? -x : x, z);
    }

    private Angle? BestCleave09STOP(int slot, bool actual)
    {
        (bool monitor, int order)[] targets = _playerOrder[slot] switch
        {
            1 => [(true, 2), (true, 3)],
            2 => [(false, 1), (true, 1)],
            3 => [(false, 4), (false, 5)],
            _ => []
        };
        if (targets.Length == 0)
            return null;

        WPos? position(int s) => actual ? Raid[s]?.Position : _playerOrder[s] != 0 ? Spot09STOP(IsMonitor(s), _playerOrder[s]) : null;
        var source = position(slot);
        if (source == null)
            return null;
        List<WPos> hit = [], avoid = [];
        foreach (var (s, p) in Raid.WithSlot())
        {
            if (s == slot || p.IsDead || position(s) is not WPos pos)
                continue;
            if (targets.Contains((IsMonitor(s), _playerOrder[s])))
                hit.Add(pos);
            else
                avoid.Add(pos);
        }
        if (hit.Count != targets.Length)
            return null;

        // cleave is a 180-degree cone: a target must be fully in front, everyone else fully behind (1y = hitbox + small margin)
        var valid = new bool[360];
        for (var i = 0; i < 360; ++i)
        {
            var dir = i.Degrees().ToDirection();
            valid[i] = hit.All(p => (p - source.Value).Dot(dir) > 1) && avoid.All(p => (p - source.Value).Dot(dir) < -1);
        }
        int bestStart = -1, bestLength = 0;
        for (var start = 0; start < 360; ++start)
        {
            if (!valid[start] || valid[(start + 359) % 360])
                continue;
            var length = 0;
            while (length < 360 && valid[(start + length) % 360])
                ++length;
            if (length > bestLength)
                (bestStart, bestLength) = (start, length);
        }
        return bestLength > 0 ? (bestStart + bestLength * 0.5f).Degrees() : null;
    }

    private IEnumerable<(Actor? origin, Angle offset, int order)> AOEs()
    {
        yield return (_boss, _bossAngle, 0);
        for (var i = 0; i < _monitorOrder.Count; ++i)
        {
            var slot = _monitorOrder[i];
            yield return (Raid[slot], _playerAngles[slot], i + 1);
        }
    }
}

class P3OversampledWaveCannonSpread(BossModule module) : Components.UniformStackSpread(module, 0, 7)
{
    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID is AID.OversampledWaveCannonR or AID.OversampledWaveCannonL)
            AddSpreads(Raid.WithoutSlot(true), Module.CastFinishAt(spell));
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if ((AID)spell.Action.ID == AID.OversampledWaveCannonAOE)
            Spreads.Clear();
    }
}
