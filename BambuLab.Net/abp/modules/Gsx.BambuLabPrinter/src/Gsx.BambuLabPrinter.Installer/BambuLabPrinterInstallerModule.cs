using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Gsx.BambuLabPrinter;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class BambuLabPrinterInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<BambuLabPrinterInstallerModule>();
        });
    }
}
