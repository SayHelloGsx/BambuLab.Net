using Localization.Resources.AbpUi;
using Gsx.BambuLabPrinter.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Gsx.BambuLabPrinter.Public;

[DependsOn(
    typeof(BambuLabPrinterPublicApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class BambuLabPrinterPublicHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(BambuLabPrinterPublicHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<BambuLabPrinterResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
