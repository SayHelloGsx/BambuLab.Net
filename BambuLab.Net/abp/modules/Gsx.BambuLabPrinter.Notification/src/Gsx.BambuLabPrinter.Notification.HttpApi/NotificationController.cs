using Gsx.BambuLabPrinter.Notification.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Gsx.BambuLabPrinter.Notification;

public abstract class NotificationController : AbpControllerBase
{
    protected NotificationController()
    {
        LocalizationResource = typeof(NotificationResource);
    }
}
