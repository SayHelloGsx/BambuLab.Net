using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Gsx.BambuLabPrinter.Public;

[DependsOn(
    typeof(BambuLabPrinterPublicApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class BambuLabPrinterPublicHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(BambuLabPrinterPublicApplicationContractsModule).Assembly,
            BambuLabPrinterPublicRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<BambuLabPrinterPublicHttpApiClientModule>();
        });

    }
}
