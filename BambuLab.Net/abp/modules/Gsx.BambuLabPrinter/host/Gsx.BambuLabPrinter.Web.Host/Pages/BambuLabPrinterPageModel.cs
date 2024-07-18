using Gsx.BambuLabPrinter.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Gsx.BambuLabPrinter.Pages;

public abstract class BambuLabPrinterPageModel : AbpPageModel
{
    protected BambuLabPrinterPageModel()
    {
        LocalizationResourceType = typeof(BambuLabPrinterResource);
    }
}
