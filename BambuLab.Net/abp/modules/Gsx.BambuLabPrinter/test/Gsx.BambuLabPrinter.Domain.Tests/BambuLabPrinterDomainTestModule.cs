using Volo.Abp.Modularity;

namespace Gsx.BambuLabPrinter;

[DependsOn(
    typeof(BambuLabPrinterDomainModule),
    typeof(BambuLabPrinterTestBaseModule)
)]
public class BambuLabPrinterDomainTestModule : AbpModule
{

}
