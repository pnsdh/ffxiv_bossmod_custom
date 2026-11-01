namespace BossMod.Endwalker.Ultimate.TOP;

// head markers the party places for the P5 sigma/omega hello world baiters (by hand or with an automarker such as Lemegeton);
// a marker only counts for the hello world set it was placed for: markers present when this component starts (left over from an earlier mechanic)
// are ignored, and so are markers placed before the latest initial hello world hit when asking about a later set (omega places a new set after the first);
// markers on anything but party members (e.g. the boss) are ignored too; the markers of a set are frozen at its initial hit, so that the next set's markers
// (placed right after it) don't change what the previous set's components show while its jumps resolve
class P5HeadMarkers(BossModule module) : BossComponent(module)
{
    private readonly ulong[] _targets = [.. Enumerable.Range(0, (int)Sign.Count).Select(i => module.WorldState.Waymarks[(Sign)i])];
    private readonly int[] _placedForSet = [.. Enumerable.Repeat(-1, (int)Sign.Count)];
    private readonly List<(ulong[] targets, int[] placedForSet)> _frozenSets = [];

    public override void Update()
    {
        for (int i = 0; i < (int)Sign.Count; ++i)
        {
            var target = WorldState.Waymarks[(Sign)i];
            if (target != _targets[i])
            {
                _targets[i] = target;
                _placedForSet[i] = target != 0 ? _frozenSets.Count : -1;
            }
        }
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if ((AID)spell.Action.ID == AID.HelloDistantWorld)
            _frozenSets.Add(([.. _targets], [.. _placedForSet]));
    }

    // set = index of the hello world within this component's lifetime (0 = first)
    public bool IsPlaced(Sign sign, int set) => Holder(sign, set) != null;
    public Actor? Holder(Sign sign, int set)
    {
        var (targets, placedForSet) = set < _frozenSets.Count ? _frozenSets[set] : (_targets, _placedForSet);
        return placedForSet[(int)sign] == set ? Raid[Raid.FindSlot(targets[(int)sign])] : null;
    }

    // omega baiter markers for the spots '1' (first distant), '2'/'3' (near, A side / C side or 9/3 o'clock side) and '4' (second distant):
    // 09STOP sheet / Lemegeton: attack 1/4 distant, 2/3 near; other parties: attack 1/2 distant and 3/4 near, either near spot (or no near markers at all)
    public static readonly Sign[] OmegaSigns = [Sign.Attack1, Sign.Attack2, Sign.Attack3, Sign.Attack4, Sign.Bind1, Sign.Bind2];
    private static readonly Sign[][] _omegaSpots1To4 = [[Sign.Attack1], [Sign.Attack2], [Sign.Attack3], [Sign.Attack4]];
    private static readonly Sign[][] _omegaSpotsDistant12 = [[Sign.Attack1], [Sign.Attack3, Sign.Attack4], [Sign.Attack3, Sign.Attack4], [Sign.Attack2]];
    // auto with attack 1/2 but no 3/4 placed: either 1/2 distant without near markers, or 1/4 distant with 3/4 still to come (or forgotten);
    // attack 2 gets both of its possible spots, and neither is taken from players without a marker
    private static readonly Sign[][] _omegaSpotsUndecided = [[Sign.Attack1], [Sign.Attack2, Sign.Attack3], [Sign.Attack3], [Sign.Attack4, Sign.Attack2]];

    public static Sign[][] OmegaBaitSigns(P5HeadMarkers? marks, int set) => marks == null ? _omegaSpots1To4 : Service.Config.Get<TOPConfig>().P5OmegaMarkerNumbering switch
    {
        TOPConfig.P5OmegaMarkers.Attack1To4 => _omegaSpots1To4,
        TOPConfig.P5OmegaMarkers.Distant12 => _omegaSpotsDistant12,
        _ => marks.OmegaAuto(set)
    };

    // attack 3/4 placed or no attack 2 -> 1/4 distant, otherwise undecided (no time-based guess: people marking by hand can take seconds between markers)
    private Sign[][] OmegaAuto(int set) => IsPlaced(Sign.Attack3, set) || IsPlaced(Sign.Attack4, set) || !IsPlaced(Sign.Attack2, set) ? _omegaSpots1To4 : _omegaSpotsUndecided;

    // spots to show a player, from a list of (spot, markers that claim it); expected = all markers the strat uses for this set, exempt = players whose markers don't matter
    // (hello world targets, who already know their spot); the spots are shown if there are at most 2 of them or candidate spots are enabled
    public static IEnumerable<WPos> Spots(P5HeadMarkers? marks, Actor actor, int set, List<(WPos pos, Sign[] signs)> spots, Sign[] expected, params Actor?[] exempt)
    {
        var config = Service.Config.Get<TOPConfig>();
        if (marks != null && config.P5FollowHeadMarkers)
            spots = marks.MarkedSpots(actor, set, spots, expected, exempt) ?? spots;
        return spots.Count <= 2 || config.P5ShowCandidateSpots ? spots.Select(s => s.pos) : [];
    }

    // the spots of the player's own marker, or the spots not yet taken by other players' markers (a spot shared by two markers is free until both are placed);
    // null (= all spots, as without markers) if the markers don't fit the strat, so that a wrong guess never hides the right spot
    // (a party consistently using another numbering than the selected one can't be detected though, see the omega numbering option)
    private List<(WPos pos, Sign[] signs)>? MarkedSpots(Actor actor, int set, List<(WPos pos, Sign[] signs)> spots, Sign[] expected, Actor?[] exempt)
    {
        Sign? own = null;
        List<Sign> taken = [];
        for (int i = 0; i < (int)Sign.Count; ++i)
        {
            var holder = Holder((Sign)i, set);
            if (holder == null || exempt.Contains(holder))
                continue;
            if (!expected.Contains((Sign)i))
                return null; // a marker this strat doesn't use: the party marks some other way
            if (holder == actor)
                own = (Sign)i;
            else
                taken.Add((Sign)i);
        }

        // own marker: its spots; a marker for a spot this player can't take (e.g. a bind spot without dynamis) means the assumptions are off
        // no marker: the free spots; none left means the same
        var result = own != null ? spots.FindAll(s => s.signs.Contains(own.Value)) : spots.FindAll(s => s.signs.Length == 0 || s.signs.Any(sign => !taken.Contains(sign)));
        return result.Count > 0 ? result : null;
    }
}
