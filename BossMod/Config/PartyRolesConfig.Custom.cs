using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using System.Reflection;

namespace BossMod;

// Boss Mod (Custom): fights that split the party by role (TOP groups etc.) silently drop their position hints while roles aren't assigned,
// so remind the player in chat (local only) instead of guessing the roles automatically
public partial class PartyRolesConfig
{
    public enum WarningChannel
    {
        [PropertyDisplay("Dalamud default (same as other plugin messages)")]
        Default,

        [PropertyDisplay("Echo")]
        Echo,

        [PropertyDisplay("System messages")]
        SystemMessage,

        [PropertyDisplay("Error messages")]
        ErrorMessage,

        [PropertyDisplay("Notices")]
        Notice,

        [PropertyDisplay("Urgent")]
        Urgent,
    }

    [PropertyDisplay("Warn in chat when roles aren't assigned", tooltip: "Only you see the message. It is shown once per duty and party, on entering a duty whose boss modules use role assignments.")]
    public bool WarnUnassigned = true;

    [PropertyDisplay("Chat channel for the warning", depends: nameof(WarnUnassigned))]
    public WarningChannel WarnChannel = WarningChannel.Default;

    [PropertyDisplay("Warn in ultimates", depends: nameof(WarnUnassigned))]
    public bool WarnUltimate = true;

    [PropertyDisplay("Warn in savage raids", depends: nameof(WarnUnassigned))]
    public bool WarnSavage = true;

    [PropertyDisplay("Warn in extreme trials", depends: nameof(WarnUnassigned))]
    public bool WarnExtreme = true;

    [PropertyDisplay("Warn in unreal trials", depends: nameof(WarnUnassigned))]
    public bool WarnUnreal = true;

    [PropertyDisplay("Warn in other content", depends: nameof(WarnUnassigned))]
    public bool WarnOther = false;

    public bool WarnFor(BossModuleInfo.Category category) => category switch
    {
        BossModuleInfo.Category.Ultimate => WarnUltimate,
        BossModuleInfo.Category.Savage => WarnSavage,
        BossModuleInfo.Category.Extreme => WarnExtreme,
        BossModuleInfo.Category.Unreal => WarnUnreal,
        _ => WarnOther
    };
}

public static class PartyRolesWarning
{
    private static DateTime _nextCheck;
    private static string _lastWarned = ""; // duty + party the warning was shown for, so wipes don't repeat it
    private static DalamudLinkPayload? _openSettings;
    private static readonly Dictionary<string, bool> _usesRoles = []; // by type name: module assemblies get reloaded
    private static (uint cfc, BossModuleInfo.Category[] categories) _duty; // categories of the current duty's fights that use roles

    public static void Update(WorldState ws)
    {
        var now = DateTime.Now;
        if (now < _nextCheck)
            return;
        _nextCheck = now.AddSeconds(2);

        try
        {
            Check(ws);
        }
        catch (Exception ex)
        {
            Service.Log($"[Custom] party roles warning failed: {ex}"); // never let a reminder break the frame update
        }
    }

    private static void Check(WorldState ws)
    {
        var config = Service.Config.Get<PartyRolesConfig>();
        if (ws.CurrentCFCID == 0 || config.AssignmentsPerSlot(ws.Party).Length > 0)
        {
            _lastWarned = ""; // left the duty or fixed the roles: warn again next time
            if (ws.CurrentCFCID == 0)
                _duty = default; // re-scan on the next duty, modules might have been reloaded meanwhile
            return;
        }
        if (!config.WarnUnassigned)
            return;

        var members = ws.Party.Members.Take(PartyState.MaxPartySize).Where(m => m.IsValid()).Select(m => m.ContentId).Order().ToList();
        if (members.Count != PartyState.MaxPartySize)
            return; // roles are assigned per 8-player party

        // judge by every fight of the duty rather than the loaded module, so multi-module duties (DSW) warn on entry, not when a later phase starts
        if (_duty.cfc != ws.CurrentCFCID)
            _duty = (ws.CurrentCFCID, [.. BossModuleRegistry.RegisteredModules.Values.Where(i => i.GroupType == BossModuleInfo.GroupType.CFC && i.GroupID == ws.CurrentCFCID && UsesRoles(i.ModuleType)).Select(i => i.Category).Distinct()]);
        if (!_duty.categories.Any(config.WarnFor))
            return;

        var key = $"{ws.CurrentCFCID}:{string.Join(',', members)}";
        if (key == _lastWarned)
            return;
        _lastWarned = key;

        _openSettings ??= Service.ChatGui.AddChatLinkHandler(0, (_, _) => Service.Mediator.Publish(new ShowConfigNode(typeof(PartyRolesConfig))));
        var msg = new SeStringBuilder()
            .AddText("파티 역할이 배정되지 않았습니다. ")
            .Add(_openSettings)
            .AddUiForeground("[설정 열기]", 37)
            .Add(RawPayload.LinkTerminator)
            .Build();
        Print(config.WarnChannel, msg);
    }

    public static void Dispose()
    {
        if (_openSettings != null)
            Service.ChatGui.RemoveChatLinkHandler(0);
        _openSettings = null;
    }

    // a fight cares about roles if any code in its namespace resolves them (group assignments in its config, or direct role lookups);
    // fights that only want each player's job role fall back to the job and work the same without assignments
    private static bool UsesRoles(Type moduleType)
    {
        if (_usesRoles.TryGetValue(moduleType.FullName ?? "", out var cached))
            return cached;

        var ns = moduleType.Namespace ?? "";
        var res = false;
        try
        {
            var types = moduleType.Assembly.GetTypes().Where(t => t.Namespace == ns).ToList();
            res = types.Any(t => t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).Any(f => f.FieldType.IsAssignableTo(typeof(GroupAssignment))))
                || types.Any(CallsRoleLookup);
        }
        catch (Exception ex)
        {
            Service.Log($"[Custom] failed to check role usage of {moduleType.FullName}: {ex.Message}");
        }
        _usesRoles[moduleType.FullName ?? ""] = res;
        return res;
    }

    private static readonly MethodBase[] RoleLookups =
    [
        typeof(PartyRolesConfig).GetMethod(nameof(PartyRolesConfig.AssignmentsPerSlot))!,
        typeof(PartyRolesConfig).GetMethod(nameof(PartyRolesConfig.SlotsPerAssignment))!,
        typeof(PartyRolesConfig).GetProperty("Item")!.GetMethod!,
        .. typeof(GroupAssignment).GetMethods().Where(m => m.Name is nameof(GroupAssignment.Resolve) or nameof(GroupAssignment.BuildGroupMask)),
    ];

    private static bool CallsRoleLookup(Type t)
    {
        const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
        foreach (var m in t.GetMethods(flags).Cast<MethodBase>().Concat(t.GetConstructors(flags)))
        {
            var il = m.GetMethodBody()?.GetILAsByteArray();
            if (il == null)
                continue;
            // look for call/callvirt with a method token; a stray byte pair can't resolve to one of the exact targets
            for (var i = 0; i + 4 < il.Length; ++i)
            {
                if (il[i] is not (0x28 or 0x6F))
                    continue;
                try
                {
                    var target = t.Module.ResolveMethod(BitConverter.ToInt32(il, i + 1));
                    if (target != null && RoleLookups.Contains(target))
                        return true;
                }
                catch (Exception) { }
            }
        }
        return false;
    }

    private static void Print(PartyRolesConfig.WarningChannel channel, SeString msg)
    {
        var type = channel switch
        {
            PartyRolesConfig.WarningChannel.Echo => XivChatType.Echo,
            PartyRolesConfig.WarningChannel.SystemMessage => XivChatType.SystemMessage,
            PartyRolesConfig.WarningChannel.ErrorMessage => XivChatType.ErrorMessage,
            PartyRolesConfig.WarningChannel.Notice => XivChatType.Notice,
            PartyRolesConfig.WarningChannel.Urgent => XivChatType.Urgent,
            _ => XivChatType.None
        };
        if (type == XivChatType.None)
            Service.ChatGui.Print(msg, "VBM");
        else
            Service.ChatGui.Print(new XivChatEntry { Type = type, Message = new SeStringBuilder().AddText("[VBM] ").Append(msg).Build() });
    }
}
