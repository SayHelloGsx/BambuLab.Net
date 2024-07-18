using Gsx.BambuLabPrinter.Public;
using Volo.Abp.Modularity;

namespace Gsx.BambuLabPrinter;

[DependsOn(
    typeof(BambuLabPrinterPublicApplicationModule),
    typeof(BambuLabPrinterDomainTestModule)
    )]
public class BambuLabPrinterApplicationTestModule : AbpModule
{

}
