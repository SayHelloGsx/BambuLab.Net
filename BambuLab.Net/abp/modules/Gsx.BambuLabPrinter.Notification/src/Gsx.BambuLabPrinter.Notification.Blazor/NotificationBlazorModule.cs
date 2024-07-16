using Microsoft.Extensions.DependencyInjection;
using Gsx.BambuLabPrinter.Notification.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace Gsx.BambuLabPrinter.Notification.Blazor;

[DependsOn(
    typeof(NotificationApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpAutoMapperModule)
    )]
public class NotificationBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<NotificationBlazorModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<NotificationBlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new NotificationMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(NotificationBlazorModule).Assembly);
        });
    }
}
