using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Gsx.BambuLabPrinter.Notification.Blazor.WebAssembly;

[DependsOn(
    typeof(NotificationBlazorModule),
    typeof(NotificationHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
    )]
public class NotificationBlazorWebAssemblyModule : AbpModule
{

}
