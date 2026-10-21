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

public class Plugin(IDalamudPluginInterface dalamud) : HostedPlugin(dalamud), IAsyncDalamudPlugin
{
    public string Name => "Boss Mod";

    private readonly bool _dormant = CustomEdition.CheckDormant(dalamud); // a conflicting fork is running, see CustomEdition

    Task IAsyncDalamudPlugin.LoadAsync(CancellationToken cancellationToken) => _dormant ? Task.CompletedTask : LoadAsync(cancellationToken);

    public override HostedPluginOptions ConfigureOptions() => new() { UseMediatorService = true };
    public override void ConfigureContainer(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterSingletonSelfAndInterfaces<TickService>();

#if LOCAL_CS
        containerBuilder.RegisterBuildCallback(ctx =>
        {
            var sigScanner = ctx.Resolve<ISigScanner[]>();
            if (sigScanner.Length == 0)
                return;

            var dataManager = ctx.Resolve<IDataManager>();
            var dalamud = ctx.Resolve<IDalamudPluginInterface>();
            var logger = ctx.Resolve<IPluginLog>();

            logger.Info("LOCAL_CS: initializing our custom address resolver");

            InteropGenerator.Runtime.Resolver.GetInstance.Setup(sigScanner[0].SearchBase, dataManager.GameData.Repositories["ffxiv"].Version, new(dalamud.ConfigDirectory.FullName + "/cs.json"));
            FFXIVClientStructs.Interop.Generated.Addresses.Register();
            InteropGenerator.Runtime.Resolver.GetInstance.Resolve();
        });
#endif
    }
    public override void ConfigureServices(IServiceCollection serviceCollection) { }

    public static string GetStorageDir() => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "vbm");
}
