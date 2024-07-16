using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Gsx.BambuLabPrinter.Notification;

[Dependency(ReplaceServices = true)]
public class NotificationBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Notification";
}
