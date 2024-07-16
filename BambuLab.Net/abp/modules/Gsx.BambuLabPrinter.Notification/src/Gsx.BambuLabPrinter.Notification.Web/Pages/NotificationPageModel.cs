using Gsx.BambuLabPrinter.Notification.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Gsx.BambuLabPrinter.Notification.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class NotificationPageModel : AbpPageModel
{
    protected NotificationPageModel()
    {
        LocalizationResourceType = typeof(NotificationResource);
        ObjectMapperContext = typeof(NotificationWebModule);
    }
}
