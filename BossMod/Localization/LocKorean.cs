namespace BossMod;

// Korean translation table, split into per-area files (LocKorean.*.cs).
// To find strings that still need a translation: python scripts/loc_extract.py [--hints <module dir>]
static partial class LocKorean
{
    public static IEnumerable<(string en, string ko)> Entries => [.. Manual, .. Core, .. Modules, .. Ultimate, .. UI];

    // strings passed to Loc.Tr directly from code (not picked up by the extractor)
    private static readonly (string, string)[] Manual =
    [
        ("Untranslated texts seen so far: {0}", "지금까지 표시된 미번역 문구: {0}"),
        ("Export untranslated texts", "미번역 문구 내보내기"),

        // interpolated hints whose values are enum names; exact entries take precedence over the "{0}" patterns
        ("Glitch: Mid", "결함: 중거리"),
        ("Glitch: Remote", "결함: 원거리"),
    ];
}
