using System.Text.RegularExpressions;

namespace BossMod;

// UI text translation: English source strings are looked up in a translation table at display time,
// so upstream code keeps its English literals and unknown/new strings simply fall back to English.
// Table keys may contain {0}, {1}... placeholders to match interpolated strings, e.g. "Order: {0}, group: {1}".
public static class Loc
{
    private static LocalizationConfig? _config;
    private static readonly Dictionary<string, string> _exact = [];
    private static readonly List<(Regex pattern, string replacement)> _patterns = [];
    private static readonly Dictionary<string, string> _patternCache = [];
    private static readonly Dictionary<string, string[]> _arrayCache = []; // keyed by joined contents, since attributes are re-instantiated on every lookup

    // strings that had no translation while a non-English language was active (for finding gaps)
    public static readonly HashSet<string> Misses = [];

    static Loc()
    {
        foreach (var (en, ko) in LocKorean.Entries)
        {
            if (en.Contains("{0}", StringComparison.Ordinal))
            {
                var parts = Regex.Split(en, @"\{\d+\}");
                var pattern = "^" + string.Join("(.*?)", parts.Select(Regex.Escape)) + "$";
                var replacement = Regex.Replace(ko.Replace("$", "$$", StringComparison.Ordinal), @"\{(\d+)\}", m => $"${{{int.Parse(m.Groups[1].Value) + 1}}}");
                _patterns.Add((new Regex(pattern, RegexOptions.Compiled | RegexOptions.Singleline), replacement));
            }
            else
            {
                _exact[en] = ko;
            }
        }
    }

    public static bool Enabled
    {
        get
        {
            if (_config == null)
            {
                try
                {
                    _config = Service.Config?.Get<LocalizationConfig>();
                }
                catch (InvalidOperationException)
                {
                    // config types not registered yet
                }
            }
            return _config?.Language == LocalizationConfig.UILanguage.Korean;
        }
    }

    public static string Tr(string text)
    {
        if (text.Length == 0 || !Enabled)
            return text;
        if (_exact.TryGetValue(text, out var result) || _patternCache.TryGetValue(text, out result))
            return result;

        result = text;
        foreach (var (pattern, replacement) in _patterns)
        {
            if (pattern.IsMatch(text))
            {
                result = pattern.Replace(text, replacement);
                break;
            }
        }

        if (ReferenceEquals(result, text))
        {
            if (Misses.Count < 4096)
                Misses.Add(text);
        }
        else if (_patternCache.Count < 4096) // interpolated strings can have unbounded variety
            _patternCache[text] = result;
        return result;
    }

    // array variant for attribute value lists
    public static string[] Tr(string[] texts)
    {
        if (!Enabled || texts.Length == 0)
            return texts;
        var key = string.Join('\u0001', texts);
        if (!_arrayCache.TryGetValue(key, out var result))
            _arrayCache[key] = result = [.. texts.Select(Tr)];
        return result;
    }
}
