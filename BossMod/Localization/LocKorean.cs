namespace BossMod;

// Korean translation table, split into per-area files (LocKorean.*.cs).
// To find strings that still need a translation: python scripts/loc_extract.py --hints BossMod.Modules BossMod.Ultimate BossMod/Components BossMod/BossModule BossMod/Modules --states BossMod.Modules BossMod.Ultimate BossMod/Modules
static partial class LocKorean
{
    public static IEnumerable<(string en, string ko)> Entries => [.. Manual, .. Core, .. Modules, .. Ultimate, .. UI, .. Hints, .. Timeline];

    // strings passed to Loc.Tr directly from code (not picked up by the extractor)
    private static readonly (string, string)[] Manual =
    [
        ("Untranslated texts seen so far: {0}", "지금까지 표시된 미번역 문구: {0}"),
        ("Export untranslated texts", "미번역 문구 내보내기"),
        ("Saved to {0}", "저장 위치: {0}"),
        ("Replays", "리플레이"),

        // kept as-is (job abbreviations in Action Tweaks etc.), listed so they don't show up as untranslated
        ("AI", "AI"),
        ("DNC", "DNC"), ("DRG", "DRG"), ("GNB", "GNB"), ("MCH", "MCH"), ("MNK", "MNK"), ("PCT", "PCT"),
        ("PLD", "PLD"), ("RPR", "RPR"), ("SCH", "SCH"), ("WAR", "WAR"), ("WHM", "WHM"),

        // config node names generated from class names (boss/content abbreviations), kept as-is
        ("SharedActions", "SharedActions"), ("ActionTweaks", "ActionTweaks"), ("OutOfCombatActions", "OutOfCombatActions"), ("SmartRotation", "SmartRotation"), ("AIHints", "AIHints"), ("BossModule", "BossModule"),
        ("ZoneModule", "ZoneModule"), ("Color", "Color"), ("PartyRoles", "PartyRoles"), ("Developer", "Developer"), ("Localization", "Localization"), ("Module", "Module"),
        ("ReplayManagement", "ReplayManagement"), ("Ex3Sphene", "Ex3Sphene"), ("Ex4Zelenia", "Ex4Zelenia"), ("Ex8Enuo", "Ex8Enuo"), ("TradeTortoise", "TradeTortoise"), ("ForkedTower", "ForkedTower"),
        ("Q01TheFinalVerse", "Q01TheFinalVerse"), ("RM05SDancingGreen", "RM05SDancingGreen"), ("RM06SSugarRiot", "RM06SSugarRiot"), ("RM08SHowlingBlade", "RM08SHowlingBlade"), ("RM09SVampFatale", "RM09SVampFatale"), ("RM10STheXtremes", "RM10STheXtremes"),
        ("RM11STheTyrant", "RM11STheTyrant"), ("RM12S2TheLindwurm", "RM12S2TheLindwurm"), ("C033SStatice", "C033SStatice"), ("P12S1Athena", "P12S1Athena"), ("P12S2PallasAthena", "P12S2PallasAthena"), ("P1S", "P1S"),
        ("P4S2", "P4S2"), ("P8S1", "P8S1"), ("P8S2", "P8S2"), ("AutoDD", "AutoDD"), ("A34RedGirl", "A34RedGirl"), ("DuelFarm", "DuelFarm"),
        ("BozjaFarm", "BozjaFarm"), ("ZadnorFarm", "ZadnorFarm"), ("FRU", "FRU"), ("UMAD", "UMAD"), ("DSW1", "DSW1"), ("DSW2", "DSW2"),
        ("TOP", "TOP"), ("TEA", "TEA"), ("UCOB", "UCOB"), ("UWU", "UWU"),

        // interpolated hints whose values are enum names; exact entries take precedence over the "{0}" patterns
        ("Glitch: Mid", "결함: 중거리"),
        ("Glitch: Remote", "결함: 원거리"),

        // slash command argument hints (not translated, listed so the extractor treats them as done)
        ("<config-type> <field> [<value>]", "<config-type> <field> [<value>]"),
        ("<preset>", "<preset>"),
        ("<name>/slot<N>", "<name>/slot<N>"),
    ];
}
