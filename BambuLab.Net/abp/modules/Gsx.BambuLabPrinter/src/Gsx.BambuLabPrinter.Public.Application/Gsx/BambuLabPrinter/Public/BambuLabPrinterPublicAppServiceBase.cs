using Gsx.BambuLabPrinter.Localization;
using Volo.Abp.Application.Services;

namespace Gsx.BambuLabPrinter.Public;

public abstract class BambuLabPrinterPublicAppServiceBase : ApplicationService
{
    protected BambuLabPrinterPublicAppServiceBase()
    {
        LocalizationResource = typeof(BambuLabPrinterResource);
        ObjectMapperContext = typeof(BambuLabPrinterPublicApplicationModule);
    }
}
