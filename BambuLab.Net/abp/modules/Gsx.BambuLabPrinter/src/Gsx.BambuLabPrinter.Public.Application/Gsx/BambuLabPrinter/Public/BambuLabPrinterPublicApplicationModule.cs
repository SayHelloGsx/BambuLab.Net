using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace Gsx.BambuLabPrinter.Public;

[DependsOn(
    typeof(BambuLabPrinterDomainModule),
    typeof(BambuLabPrinterPublicApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class BambuLabPrinterPublicApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<BambuLabPrinterPublicApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<BambuLabPrinterPublicApplicationModule>(validate: true);
        });
    }
}
