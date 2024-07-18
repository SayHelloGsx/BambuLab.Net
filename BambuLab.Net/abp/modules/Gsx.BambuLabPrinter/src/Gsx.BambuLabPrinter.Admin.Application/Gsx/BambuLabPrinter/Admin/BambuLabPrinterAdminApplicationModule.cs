using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Gsx.BambuLabPrinter.Admin;

[DependsOn(
    typeof(BambuLabPrinterDomainModule),
    typeof(BambuLabPrinterAdminApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class BambuLabPrinterAdminApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<BambuLabPrinterAdminApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<BambuLabPrinterAdminApplicationModule>(validate: true);
        });
    }
}
