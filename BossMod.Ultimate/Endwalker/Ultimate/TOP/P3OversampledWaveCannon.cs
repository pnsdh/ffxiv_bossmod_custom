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
            if (SafestCleave09STOP(slot) is var (cleave, halfWidth))
            {
                var facing = cleave - _playerAngles[slot];
                var allowed = Math.Clamp(halfWidth - 2, 2, 15);
                hints.ForbiddenDirections.Add(((facing + 180.Degrees()).Normalized(), (180 - allowed).Degrees(), _resolve));
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
        foreach (var p in SafeSpots(pcSlot).Where(p => p.assigned))
            Arena.AddCircle(p.pos, 1, ArenaColor.Safe);
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

    // 09STOP spots for boss monitor on the left (west cleave), mirrored for the right: the 09STOP diagram with N1/N4/N5 hugging the boss cleave line from outside
    // and N2 from inside, which lets N3 move ~3y closer to the center; N1/N5 1y from the arena edge; everyone is at least 8.5y apart (hit players explode
    // in 7y circles; the diagram has M1-M2 at 7.8y) and monitors facing exactly E/N/S keep +-11..15 degrees of tolerance (diagram: +-8..11)
    private static readonly (float x, float z)[] _monitorSpots09STOP = [(8, -12), (16, -8.5f), (12.5f, 5.5f)];
    private static readonly (float x, float z)[] _otherSpots09STOP = [(1, -19), (-1.5f, -1), (-11, -0.5f), (1, 10), (1, 19)];
    private const float _explosionRadius = 7;

    private WPos Spot09STOP(bool monitor, int order)
    {
        var (x, z) = (monitor ? _monitorSpots09STOP : _otherSpots09STOP)[order - 1];
        return Module.Center + new WDir(_bossAngle.Rad < 0 ? -x : x, z);
    }

    // strat cleave direction of a monitor: M1 away from the boss monitor side, M2 north, M3 south
    private Angle StratCleave09STOP(int order) => order switch
    {
        1 => _bossAngle.Rad > 0 ? 90.Degrees() : -90.Degrees(),
        2 => 180.Degrees(),
        _ => 0.Degrees()
    };

    // 09STOP auto-facing for a monitor, as a safety net for people standing in wrong spots. Uses actual positions, but assumes the boss and the other monitors
    // aim exactly as the strat: their actual facing can't be trusted (it may change right before the snapshot and arrives with network delay).
    // Every directly hit player explodes in a 7y circle, so a player takes one hit per hit player within 7.5y (7y + hitbox) of them.
    // Picks a cleave direction that (1) doesn't make anyone take two hits, (2) hits the planned pair (M1 -> M2+M3, M2 -> N1+M1, M3 -> N4+N5),
    // (3) doesn't hit anyone else; among the directions with the lowest cost, takes the middle of the range closest to the strat direction.
    private (Angle cleave, float halfWidth)? SafestCleave09STOP(int slot)
    {
        var self = Raid[slot];
        if (self == null || _boss == null)
            return null;
        (bool monitor, int order)[] plan = _playerOrder[slot] switch
        {
            1 => [(true, 2), (true, 3)],
            2 => [(false, 1), (true, 1)],
            3 => [(false, 4), (false, 5)],
            _ => []
        };
        if (plan.Length == 0)
            return null;
        var strat = (int)MathF.Round(StratCleave09STOP(_playerOrder[slot]).Deg + 360) % 360;

        List<(int slot, WPos origin, WDir dir)> otherSources = [(-1, _boss.Position, (_boss.Rotation + _bossAngle).ToDirection())];
        foreach (var m in _monitorOrder)
            if (m != slot && Raid[m] is { IsDead: false } monitor)
                otherSources.Add((m, monitor.Position, StratCleave09STOP(_playerOrder[m]).ToDirection()));

        // 'maybe hit': any part of the hitbox inside, 'surely hit': the whole hitbox inside; no extra margin, since the strat has people hugging the boss cleave line
        static bool maybeHit(WPos origin, WDir dir, WPos p) => (p - origin).Dot(dir) > -0.5f;
        static bool surelyHit(WPos origin, WDir dir, WPos p) => (p - origin).Dot(dir) > 0.5f;

        List<(int slot, WPos pos, bool planned)> players = [.. Raid.WithSlot().Where(sp => !sp.Item2.IsDead).Select(sp => (sp.Item1, sp.Item2.Position, plan.Contains((IsMonitor(sp.Item1), _playerOrder[sp.Item1]))))];
        List<WPos> fixedTargets = [];
        foreach (var (s, pos, _) in players)
            foreach (var src in otherSources)
                if (src.slot != s && maybeHit(src.origin, src.dir, pos))
                    fixedTargets.Add(pos);

        const float explosionCheck = (_explosionRadius + 0.5f) * (_explosionRadius + 0.5f); // + player hitbox
        var cost = new int[360];
        List<WPos> targets = [];
        for (var i = 0; i < 360; ++i)
        {
            var dir = i.Degrees().ToDirection();
            targets.Clear();
            targets.AddRange(fixedTargets);
            foreach (var (s, pos, planned) in players)
            {
                if (s == slot)
                    continue;
                var maybe = maybeHit(self.Position, dir, pos);
                if (maybe)
                    targets.Add(pos);
                if (planned && !surelyHit(self.Position, dir, pos))
                    cost[i] += 10;
                else if (!planned && maybe)
                    cost[i] += 1;
            }
            foreach (var (_, pos, _) in players)
                if (targets.Count(t => (t - pos).LengthSq() < explosionCheck) >= 2)
                    cost[i] += 1000; // two hits = death
        }

        var best = cost.Min();
        (int start, int length, int distance)? choice = null;
        for (var start = 0; start < 360; ++start)
        {
            if (cost[start] != best || cost[(start + 359) % 360] == best && cost.Any(c => c != best))
                continue;
            var length = 0;
            while (length < 360 && cost[(start + length) % 360] == best)
                ++length;
            var mid = (start + length / 2) % 360;
            var distance = Math.Min(Math.Abs(mid - strat), 360 - Math.Abs(mid - strat));
            if (choice == null || distance < choice.Value.distance)
                choice = (start, length, distance);
            if (length == 360)
                break;
        }
        if (choice == null)
            return null;
        var (bestStart, bestLength, _) = choice.Value;
        return bestLength == 360 ? (strat.Degrees(), 15) : ((bestStart + bestLength * 0.5f).Degrees(), bestLength * 0.5f);
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
