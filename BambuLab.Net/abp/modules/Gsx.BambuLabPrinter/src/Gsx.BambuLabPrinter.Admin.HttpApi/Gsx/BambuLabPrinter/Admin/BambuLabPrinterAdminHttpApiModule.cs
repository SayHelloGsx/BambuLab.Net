using Localization.Resources.AbpUi;
using Gsx.BambuLabPrinter.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Gsx.BambuLabPrinter.Admin;

[DependsOn(
    typeof(BambuLabPrinterAdminApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class BambuLabPrinterAdminHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(BambuLabPrinterAdminHttpApiModule).Assembly);
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
