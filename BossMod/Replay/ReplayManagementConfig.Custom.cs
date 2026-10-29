namespace BossMod;

// Boss Mod (Custom): see VfxRecorder
public partial class ReplayManagementConfig
{
    [PropertyDisplay("Record ground and omen effects in replays", tooltip: "Adds the static/ground effects and omens the game spawns (file, position, rotation, size) and a clock sync to recorded replays, for working out new fights.\nThese replays can't be opened by the original Boss Mod.")]
    public bool RecordVfx = false;

    [PropertyDisplay("Also record effects attached to enemies", tooltip: "On-hit and other effects attached to bosses and helpers. Effects of players, pets and chocobos are skipped.", depends: nameof(RecordVfx))]
    public bool RecordActorVfx = true;

    [PropertyDisplay("Also record standalone effect objects", tooltip: "Effects that aren't omens and aren't attached to anyone, e.g. mechanic effects and background effects. Includes effects of players.", depends: nameof(RecordVfx))]
    public bool RecordVfxObjects = true;
}
