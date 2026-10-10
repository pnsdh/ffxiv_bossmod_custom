using Dalamud.Bindings.ImGui;
using System.IO;

namespace BossMod;

[ConfigDisplay(Name = "Language", Order = -10)]
public sealed class LocalizationConfig : ConfigNode
{
    public enum UILanguage
    {
        [PropertyDisplay("English")]
        English,

        [PropertyDisplay("한국어")]
        Korean,
    }

    [PropertyDisplay("UI language", tooltip: "Translates settings and mechanic hints. Text without a translation is shown in English.")]
    public UILanguage Language = UILanguage.Korean;

    private string _lastExport = "";

    public override void DrawCustom(UITree tree, WorldState ws)
    {
        if (Language == UILanguage.English)
            return;

        ImGui.TextUnformatted(Loc.Tr($"Untranslated texts seen so far: {Loc.Misses.Count}"));
        if (ImGui.Button(Loc.Tr("Export untranslated texts")))
        {
            var path = Path.Combine(Plugin.GetStorageDir(), "untranslated.txt");
            File.WriteAllLines(path, Loc.Misses.Order());
            Service.Log($"Exported {Loc.Misses.Count} untranslated texts to {path}");
            _lastExport = path;
        }
        if (_lastExport.Length > 0)
        {
            ImGui.SameLine();
            ImGui.TextUnformatted(Loc.Tr($"Saved to {_lastExport}"));
        }
    }
}
