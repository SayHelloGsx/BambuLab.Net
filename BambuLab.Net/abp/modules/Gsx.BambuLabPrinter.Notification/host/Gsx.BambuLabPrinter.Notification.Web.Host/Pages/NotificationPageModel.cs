using Gsx.BambuLabPrinter.Notification.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Gsx.BambuLabPrinter.Notification.Pages;

public abstract class NotificationPageModel : AbpPageModel
{
    protected NotificationPageModel()
    {
        LocalizationResourceType = typeof(NotificationResource);
    }
}
