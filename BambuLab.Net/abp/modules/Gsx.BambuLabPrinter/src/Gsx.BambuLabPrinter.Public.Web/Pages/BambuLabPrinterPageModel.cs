using Gsx.BambuLabPrinter.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Gsx.BambuLabPrinter.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class BambuLabPrinterPageModel : AbpPageModel
{
    protected BambuLabPrinterPageModel()
    {
        LocalizationResourceType = typeof(BambuLabPrinterResource);
        ObjectMapperContext = typeof(BambuLabPrinterPublicWebModule);
    }
}
