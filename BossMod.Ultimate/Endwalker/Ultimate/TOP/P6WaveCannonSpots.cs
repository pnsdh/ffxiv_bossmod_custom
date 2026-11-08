namespace BossMod.Endwalker.Ultimate.TOP;

// optional 09STOP spots for the P6 wave cannon proteans (TOPConfig.P6WaveCannon09STOP), by party role; bearings are clockwise from north, as in the strat sheet
abstract class P6WaveCannonSpots(BossModule module) : BossComponent(module)
{
    protected static WDir Polar(float bearing, float radius) => radius * (180 - bearing).Degrees().ToDirection();

    protected abstract (WDir offset, bool safe)? Spot(PartyRolesConfig.Assignment role);

    public override void DrawArenaForeground(int pcSlot, Actor pc)
    {
        if (!Service.Config.Get<TOPConfig>().P6WaveCannon09STOP)
            return;
        var role = Service.Config.Get<PartyRolesConfig>()[Raid.Members[pcSlot].ContentId];
        if (role != PartyRolesConfig.Assignment.Unassigned && Spot(role) is var (offset, safe))
            Arena.AddCircle(Module.Center + offset, 1, safe ? ArenaColor.Safe : ArenaColor.Object);
    }
}

// first wave cannon: 10y from the boss, the usual 8-way spread with T1 and D3 swapped; then the stack at 6 o'clock with the tanks in front (2023 KR ACT logs: 3.4-4.5y and 8-9.8y)
class P6WaveCannonSpots1(BossModule module) : P6WaveCannonSpots(module)
{
    private static readonly float[] _bearings = [315, 180, 270, 90, 225, 135, 0, 45]; // MT, OT, H1, H2, M1, M2, R1, R2

    protected override (WDir offset, bool safe)? Spot(PartyRolesConfig.Assignment role)
    {
        if (Module.FindComponent<P6WaveCannonProteans>() != null)
            return (Polar(_bearings[(int)role], 10), true);
        return (Polar(180, role is PartyRolesConfig.Assignment.MT or PartyRolesConfig.Assignment.OT ? 4 : 9), true);
    }
}

// second wave cannon, during the exasquares: one spot per protean set, depending on the exasquare pattern; both sets' spots are inside a band that explodes right before
// that set's protean, so a spot is only shown as safe after that explosion and as a preview before it; the second spot is kept until the explosion after the second set
class P6WaveCannonSpots2(BossModule module) : P6WaveCannonSpots(module)
{
    private static readonly (float axis, float side)[] _axes = [(0, -1), (90, -1), (270, -1), (180, -1), (180, 1), (90, 1), (270, 1), (0, 1)]; // MT, OT, H1, H2, M1, M2, R1, R2
    private DateTime _proteanCastStart;
    private DateTime _lastExplosions;
    private int _explosionsAfterCast; // 1 = ~1.1s before the first protean set, 2 = between the sets, 3 = ~0.9s after the second set
    private int _numProteans;

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID == AID.P6WaveCannonProtean)
            _proteanCastStart = WorldState.CurrentTime;
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        switch ((AID)spell.Action.ID)
        {
            case AID.P6WaveCannonProteanAOE:
                ++_numProteans;
                break;
            case AID.CosmoArrowFirst or AID.CosmoArrowRest:
                // the protean cast starts together with an explosion set, which shouldn't count; later sets are 2s apart
                var now = WorldState.CurrentTime;
                if (_proteanCastStart != default && (now - _proteanCastStart).TotalSeconds > 1 && (now - _lastExplosions).TotalSeconds > 0.5f)
                {
                    ++_explosionsAfterCast;
                    _lastExplosions = now;
                }
                break;
        }
    }

    protected override (WDir offset, bool safe)? Spot(PartyRolesConfig.Assignment role)
    {
        var pattern = Module.FindComponent<P6CosmoArrow>()?.CurPattern ?? P6CosmoArrow.Pattern.Unknown;
        if (pattern == P6CosmoArrow.Pattern.Unknown || _explosionsAfterCast >= 3)
            return null;

        // (distance off the axis, distance along the axis) for the player on the clockwise side of the north axis
        var second = _numProteans > 0;
        var (dx, dy) = pattern == P6CosmoArrow.Pattern.InOut
            ? (second ? (3.5f, 8.5f) : (4.85f, 11.15f))
            : (second ? (3.75f, 8.75f) : (3.5f, 9.85f));
        var (axis, side) = _axes[(int)role];
        var bearing = axis + side * MathF.Atan2(dx, dy).Radians().Deg;
        return (Polar(bearing, MathF.Sqrt(dx * dx + dy * dy)), _explosionsAfterCast >= (second ? 2 : 1));
    }
}
