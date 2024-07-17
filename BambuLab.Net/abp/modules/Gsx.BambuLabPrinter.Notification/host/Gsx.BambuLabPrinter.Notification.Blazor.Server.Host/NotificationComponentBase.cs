using Gsx.BambuLabPrinter.Notification.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Gsx.BambuLabPrinter.Notification.Blazor.Server.Host;

public abstract class NotificationComponentBase : AbpComponentBase
{
    protected NotificationComponentBase()
    {
        LocalizationResource = typeof(BambuLabPrinterNotificationResource);
    }
}
