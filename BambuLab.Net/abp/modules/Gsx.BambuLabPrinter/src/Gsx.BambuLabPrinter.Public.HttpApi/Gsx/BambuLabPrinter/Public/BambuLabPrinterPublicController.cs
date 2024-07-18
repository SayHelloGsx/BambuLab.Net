using Gsx.BambuLabPrinter.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Gsx.BambuLabPrinter.Public;

public abstract class BambuLabPrinterPublicController : AbpControllerBase
{
    protected BambuLabPrinterPublicController()
    {
        LocalizationResource = typeof(BambuLabPrinterResource);
    }
}
