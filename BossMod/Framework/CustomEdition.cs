using Dalamud.Interface.ImGuiNotification;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace BossMod;

// Boss Mod (Custom) keeps the original's InternalName (other plugins look Boss Mod up by it), so it can't be installed next to the original;
// forks with their own name (BossModReborn) can, and running both at once would double every hook, window and automation,
// so this edition steps aside while one of them is loaded.
public static class CustomEdition
{
    private static readonly string[] ConflictingInternalNames = ["BossModReborn"];

    private static string? LoadedConflict(IDalamudPluginInterface dalamud) => dalamud.InstalledPlugins.FirstOrDefault(p => p.IsLoaded && ConflictingInternalNames.Contains(p.InternalName))?.Name;

    // loaded while a conflicting plugin is running: the plugin stays loaded but starts nothing
    public static bool CheckDormant(IDalamudPluginInterface dalamud)
    {
        if (LoadedConflict(dalamud) is not { } name)
            return false;

        var msg = $"{name} 플러그인이 켜져 있어서 Boss Mod (Custom)은 아무것도 하지 않습니다. {name} 플러그인을 끈 뒤 Boss Mod (Custom)을 껐다 켜 주세요.";
        var services = dalamud.Create<Services>();
        services?.Log.Warning($"[Custom] {name} is loaded, not starting");
        services?.Notifications.AddNotification(new() { Title = "Boss Mod (Custom)", Content = msg, Type = NotificationType.Warning, InitialDuration = TimeSpan.FromSeconds(30) });
        services?.Chat.PrintError(msg, "VBM");
        return true;
    }

    // a conflicting plugin got loaded after us (e.g. both enabled at game start, when plugins load in parallel): pause until it is unloaded
    public static bool Paused { get; private set; }
    private static DateTime _nextCheck;

    public static bool UpdatePaused(IDalamudPluginInterface dalamud)
    {
        var now = DateTime.Now;
        if (now < _nextCheck)
            return Paused;
        _nextCheck = now.AddSeconds(2);

        var conflict = LoadedConflict(dalamud);
        if ((conflict != null) != Paused)
        {
            Paused = conflict != null;
            var msg = Paused
                ? $"{conflict} 플러그인이 함께 켜져 있어서 Boss Mod (Custom)을 일시 중지했습니다. 둘 중 하나만 켜 주세요."
                : "함께 켜져 있던 플러그인이 꺼져서 Boss Mod (Custom)을 다시 시작합니다.";
            Service.Log($"[Custom] conflicting plugin loaded: {conflict ?? "none"}");
            Service.ShowNotification(msg, Paused ? NotificationType.Warning : NotificationType.Info);
            if (Paused)
                Service.ChatGui.PrintError(msg, "VBM");
            else
                Service.ChatMessage(msg);
        }
        return Paused;
    }

    private sealed class Services
    {
        [PluginService] public IPluginLog Log { get; private set; } = null!;
        [PluginService] public INotificationManager Notifications { get; private set; } = null!;
        [PluginService] public IChatGui Chat { get; private set; } = null!;
    }
}
