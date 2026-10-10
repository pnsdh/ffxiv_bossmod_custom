using System.Text.Json.Serialization;

namespace BossMod.Endwalker.Ultimate.TOP;

[ConfigDisplay(Order = 0x210, Parent = typeof(EndwalkerConfig))]
public partial class TOPConfig() : ConfigNode()
{
    [PropertyDisplay("Apply 09STOP (Korean strat) to all TOP options", tooltip: "Sets every TOP option that has a 09STOP choice. Options without one are left unchanged.", renderer: typeof(Apply09STOPRenderer))]
    [JsonIgnore] public readonly ApplyButton Apply09STOPButton = new();

    [PropertyDisplay("P1 Program Loop: assignments", tooltip: "G1 takes the first free spot clockwise from NW, G2 counterclockwise.\nIf both players with the same number end up in the same group, the lower priority one flexes.")]
    [GroupDetails(["G1 prio1", "G1 prio2", "G1 prio3", "G1 prio4", "G2 prio1", "G2 prio2", "G2 prio3", "G2 prio4"])]
    [GroupPreset("LPDU (global): M1>M2>MT>OT>R1>R2>H1>H2", [5, 4, 1, 0, 7, 6, 3, 2])]
    [GroupPreset("NA (snake prio): TMRH, group 2 north", [4, 0, 7, 3, 5, 1, 6, 2])]
    [GroupPreset("09STOP (global): H1>T1>T2>D1>D2>D3>D4>H2", [6, 5, 7, 0, 4, 3, 2, 1])]
    public GroupAssignmentUnique P1ProgramLoopAssignments = new() { Assignments = [6, 5, 7, 0, 4, 3, 2, 1] };

    [PropertyDisplay("P1 Program Loop: use global priority", tooltip: "Treat the 8 columns as one priority order (left = lowest) instead of per-group priorities; the lower priority player of each pair flexes to G2.")]
    public bool P1ProgramLoopGlobalPriority = true;

    [PropertyDisplay("P1 Pantokrator: assignments", tooltip: "G1 north, G2 south, adjusting clockwise.\nIf both players with the same number end up in the same group, the lower priority one flexes.")]
    [GroupDetails(["G1 prio1", "G1 prio2", "G1 prio3", "G1 prio4", "G2 prio1", "G2 prio2", "G2 prio3", "G2 prio4"])]
    [GroupPreset("LPDU (light parties): flex T>M>R", [0, 4, 3, 7, 1, 5, 2, 6])]
    [GroupPreset("NA (snake prio): TMRH, group 2 north", [4, 0, 7, 3, 5, 1, 6, 2])]
    [GroupPreset("09STOP (global): H1>T1>T2>D1>D2>D3>D4>H2", [6, 5, 7, 0, 4, 3, 2, 1])]
    public GroupAssignmentUnique P1PantokratorAssignments = new() { Assignments = [6, 5, 7, 0, 4, 3, 2, 1] };

    [PropertyDisplay("P1 Pantokrator: group positions")]
    [PropertyCombo("North/South", "NE/SW")]
    public bool P1PantokratorNESW = false;

    [PropertyDisplay("P1 Pantokrator: group positions if the first flames are N/S")]
    [PropertyCombo("G1 east, G2 west (LPDU)", "G1 west, G2 east (09STOP)")]
    public bool P1PantokratorNSFlameG1West = true;

    [PropertyDisplay("P1 Pantokrator: use global priority", tooltip: "Treat the 8 columns as one priority order (left = lowest) instead of per-group priorities; the lower priority player of each pair flexes to G2.")]
    public bool P1PantokratorGlobalPriority = true;

    [PropertyDisplay("P2 Party Synergy: assignments", tooltip: "G1 left, G2 right when looking at the eye.\nIf both players with the same number end up in the same group, the lower priority one flexes.")]
    [GroupDetails(["G1 prio1", "G1 prio2", "G1 prio3", "G1 prio4", "G2 prio1", "G2 prio2", "G2 prio3", "G2 prio4"])]
    [GroupPreset("LPDU (light parties): flex R>M>H", [3, 7, 2, 6, 1, 5, 0, 4])]
    [GroupPreset("NA (HRMT conga)", [0, 4, 3, 7, 1, 5, 2, 6])]
    [GroupPreset("09STOP (global): H1>T1>T2>D1>D2>D3>D4>H2", [6, 5, 7, 0, 4, 3, 2, 1])]
    public GroupAssignmentUnique P2PartySynergyAssignments = new() { Assignments = [6, 5, 7, 0, 4, 3, 2, 1] };

    [PropertyDisplay("P2 Party Synergy: use global priority", tooltip: "Treat the 8 columns as one priority order (left = lowest) instead of per-group priorities; the lower priority player of each pair flexes to G2.")]
    public bool P2PartySynergyGlobalPriority = true;

    [PropertyDisplay("P2 Party Synergy: marker order from the eye", tooltip: "With Remote Glitch, G2 uses this order reversed according to the option below.")]
    [PropertyCombo("Cross, square, circle, triangle (LPDU, NA)", "Circle, triangle, square, cross (09STOP)")]
    public bool P2PartySynergyMarkerOrder09STOP = true;

    [PropertyDisplay("P2 Party Synergy: G2 order with Remote Glitch")]
    [PropertyCombo("GPOB (only B and G swap) (NA)", "GOPB (reverse order) (LPDU)")]
    public bool P2PartySynergyG2ReverseAll = true;

    [PropertyDisplay("P2 Party Synergy: who swaps if both stacks are in the same group")]
    [PropertyCombo("Northernmost pair (NA)", "Southernmost pair (LPDU)")]
    public bool P2PartySynergyStackSwapSouth = false;

    [PropertyDisplay("P3 Intermission: spot priority, west to east")]
    [GroupDetails(["1", "2", "3", "4", "5", "6", "7", "8"])]
    [GroupPreset("LPDU (RMTH HTMR)", [2, 5, 3, 4, 1, 6, 0, 7])]
    [GroupPreset("NA (HRMT conga)", [3, 4, 0, 7, 2, 5, 1, 6])]
    [GroupPreset("09STOP (HTTDDDDH)", [1, 2, 0, 7, 3, 4, 5, 6])]
    public GroupAssignmentUnique P3IntermissionAssignments = new() { Assignments = [1, 2, 0, 7, 3, 4, 5, 6] };

    [PropertyDisplay("P3 Intermission: spread/stack position")]
    [PropertyCombo("Stacks S, spreads N", "Stacks N, spreads S")]
    public bool P3IntermissionStacksNorth = true;

    [PropertyDisplay("P3 Monitors: priority, north to south")]
    [GroupDetails(["1", "2", "3", "4", "5", "6", "7", "8"])]
    [GroupPreset("LPDU (HTMR)", [2, 3, 0, 1, 4, 5, 6, 7])]
    [GroupPreset("NA (HRMT conga)", [3, 4, 0, 7, 2, 5, 1, 6])]
    [GroupPreset("09STOP (HHTTDDDD)", [2, 3, 0, 1, 4, 5, 6, 7])]
    public GroupAssignmentUnique P3MonitorsAssignments = new() { Assignments = [2, 3, 0, 1, 4, 5, 6, 7] };

    public enum P3MonitorsLayout
    {
        [PropertyDisplay("Monitors on the boss monitor side, safe side monitor north (LPDU)")]
        LPDU,

        [PropertyDisplay("Monitors on the boss monitor side, safe side monitor south (Aether)")]
        Aether,

        [PropertyDisplay("Monitors opposite the boss monitor, facing E/W, N, S (09STOP)")]
        Strat09STOP,
    }

    [PropertyDisplay("P3 Monitors: layout")]
    public P3MonitorsLayout P3MonitorsLayoutChoice = P3MonitorsLayout.Strat09STOP;

    [JsonIgnore] public bool P3Monitors09STOP => P3MonitorsLayoutChoice == P3MonitorsLayout.Strat09STOP;
    [JsonIgnore] public bool P3LastMonitorSouth => P3MonitorsLayoutChoice == P3MonitorsLayout.Aether;

    [PropertyDisplay("Monitors: automatically face the correct direction", tooltip: "Applies to P3 and P5 Delta monitors.\nRequires Settings -> Action Tweaks -> Smart Character Orientation.")]
    public bool P3MonitorForbiddenDirections = true;

    [PropertyDisplay("P4 Wave Cannon: priority, north to south", tooltip: "Assuming the south player flexes.")]
    [GroupDetails(["W1", "E1", "W2", "E2", "W3", "E3", "W4", "E4"])]
    [GroupPreset("LPDU (TRHM)", [0, 1, 4, 5, 6, 7, 2, 3])]
    [GroupPreset("09STOP (TRHM)", [0, 1, 4, 5, 6, 7, 2, 3])]
    public GroupAssignmentUnique P4WaveCannonAssignments = new() { Assignments = [0, 1, 4, 5, 6, 7, 2, 3] };

    [PropertyDisplay("P5 Delta: layout")]
    [PropertyCombo("LPDU", "09STOP")]
    public bool P5Delta09STOP = true;

    [PropertyDisplay("P5 Sigma: layout", tooltip: "09STOP: line up between the arms, spread by marker count, towers by the 09STOP floor marker macros.")]
    [PropertyCombo("LPDU", "09STOP")]
    public bool P5Sigma09STOP = true;

    [PropertyDisplay("P5: show candidate spots", tooltip: "For players whose exact spot isn't known yet, e.g. Hello World baiters whose roles depend on head markers placed by the party.\nOnly affects cases with 3 or more candidate spots; up to 2 spots are always shown.")]
    public bool P5ShowCandidateSpots = false;
}
