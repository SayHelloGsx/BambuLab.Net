using Gsx.BambuLabPrinter.Notification.Localization;
using Volo.Abp.Application.Services;

namespace Gsx.BambuLabPrinter.Notification;

public abstract class NotificationAppService : ApplicationService
{
    protected NotificationAppService()
    {
        LocalizationResource = typeof(BambuLabPrinterNotificationResource);
        ObjectMapperContext = typeof(NotificationApplicationModule);
    }
}
