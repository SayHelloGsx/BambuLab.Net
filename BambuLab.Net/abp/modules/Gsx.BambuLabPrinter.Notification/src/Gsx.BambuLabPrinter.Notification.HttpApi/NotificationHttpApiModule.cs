using Localization.Resources.AbpUi;
using Gsx.BambuLabPrinter.Notification.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Gsx.BambuLabPrinter.Notification;

[DependsOn(
    typeof(NotificationApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class NotificationHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(NotificationHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<BambuLabPrinterNotificationResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
