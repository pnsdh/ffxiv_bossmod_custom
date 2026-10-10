using Dalamud.Bindings.ImGui;

namespace BossMod.Endwalker.Ultimate.TOP;

// 09STOP: the Korean server strat; it's also the default for every option that has a 09STOP choice
public partial class TOPConfig
{
    // global priority H1 > T1 > T2 > D1 > D2 > D3 > D4 > H2 (indexed by MT, OT, H1, H2, M1, M2, R1, R2; higher value keeps G1)
    public static readonly int[] Priority09STOP = [6, 5, 7, 0, 4, 3, 2, 1];

    public override void DrawCustom(UITree tree, WorldState ws)
    {
        ImGui.Separator();
        if (ImGui.Button(Loc.Tr("Apply 09STOP (Korean strat) to all TOP options")))
        {
            Apply09STOP();
            Modified.Fire();
        }
        ImGui.SameLine();
        UIMisc.HelpMarker(Loc.Tr("Sets every TOP option that has a 09STOP choice. Options without one are left unchanged."));
    }

    public void Apply09STOP()
    {
        // P1 Program Loop: earlier in the line takes N->CW (G1), later takes W->CCW (G2)
        P1ProgramLoopAssignments.Assignments = [.. Priority09STOP];
        P1ProgramLoopGlobalPriority = true;

        // P1 Pantokrator: H1 group NW side, H2 group SE side; N/S first flames -> H1 group west
        P1PantokratorAssignments.Assignments = [.. Priority09STOP];
        P1PantokratorGlobalPriority = true;
        P1PantokratorNESW = false;
        P1PantokratorNSFlameG1West = true;

        // P2 Party Synergy: H1 side left of the eye; circle-triangle-square-cross from the eye, right side reversed on Remote Glitch,
        // pair closer to the eye swaps if both stacks are in the same group
        P2PartySynergyAssignments.Assignments = [.. Priority09STOP];
        P2PartySynergyGlobalPriority = true;
        P2PartySynergyMarkerOrder09STOP = true;
        P2PartySynergyG2ReverseAll = true;
        P2PartySynergyStackSwapSouth = false;

        // P3 Intermission: line W->E H1 T1 T2 D1 D2 D3 D4 H2; spreads W/SW/SE/E, stacks and no-debuff NW/NE
        P3IntermissionAssignments.Assignments = [1, 2, 0, 7, 3, 4, 5, 6];
        P3IntermissionStacksNorth = true;

        // P3 Monitors: priority from north H1 H2 T1 T2 D1 D2 D3 D4; monitors stand opposite the boss monitor
        P3MonitorsAssignments.Assignments = [2, 3, 0, 1, 4, 5, 6, 7];
        P3Monitors09STOP = true;
    }
}
