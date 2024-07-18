using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Gsx.BambuLabPrinter.Admin;

[DependsOn(
    typeof(BambuLabPrinterAdminApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class BambuLabPrinterAdminHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(BambuLabPrinterAdminApplicationContractsModule).Assembly,
            BambuLabPrinterAdminRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<BambuLabPrinterAdminHttpApiClientModule>();
        });

    }
}
