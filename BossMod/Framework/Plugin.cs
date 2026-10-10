using Autofac;
using BossMod.Services;
using DalaMock.Host.Hosting;
using DalaMock.Shared.Extensions;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BossMod;

public class Plugin : HostedPlugin, IAsyncDalamudPlugin
{
    public string Name => "Boss Mod";

    private readonly bool _dormant; // a conflicting fork is running, see CustomEdition

    public Plugin(IDalamudPluginInterface dalamud, ISigScanner sigScanner, IDataManager dataManager) : base(dalamud)
    {
        _dormant = CustomEdition.CheckDormant(dalamud);
        if (_dormant)
            return;

#if LOCAL_CS
        if (sigScanner == null) // nonexistent in mock environment
            return;

        InteropGenerator.Runtime.Resolver.GetInstance.Setup(sigScanner.SearchBase, dataManager.GameData.Repositories["ffxiv"].Version, new(dalamud.ConfigDirectory.FullName + "/cs.json"));
        FFXIVClientStructs.Interop.Generated.Addresses.Register();
        InteropGenerator.Runtime.Resolver.GetInstance.Resolve();
#endif
    }

    Task IAsyncDalamudPlugin.LoadAsync(CancellationToken cancellationToken) => _dormant ? Task.CompletedTask : LoadAsync(cancellationToken);

    public override HostedPluginOptions ConfigureOptions() => new() { UseMediatorService = true };
    public override void ConfigureContainer(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterSingletonSelfAndInterfaces<TickService>();
    }
    public override void ConfigureServices(IServiceCollection serviceCollection) { }

    public static string GetStorageDir() => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "vbm");
}
