using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace Gsx.BambuLabPrinter.Notification.Blazor.Server;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerThemingModule),
    typeof(NotificationBlazorModule)
    )]
public class NotificationBlazorServerModule : AbpModule
{

}
