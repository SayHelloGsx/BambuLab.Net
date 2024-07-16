using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Gsx.BambuLabPrinter.Notification.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class NotificationBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Notification";
}
