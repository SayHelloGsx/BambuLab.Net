using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Gsx.BambuLabPrinter;

[Dependency(ReplaceServices = true)]
public class BambuLabPrinterBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "BambuLabPrinter";
}
