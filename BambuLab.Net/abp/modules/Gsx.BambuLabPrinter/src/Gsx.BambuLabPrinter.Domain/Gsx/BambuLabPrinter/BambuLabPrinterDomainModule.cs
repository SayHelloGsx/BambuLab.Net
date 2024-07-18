using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Gsx.BambuLabPrinter;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(BambuLabPrinterDomainSharedModule)
)]
public class BambuLabPrinterDomainModule : AbpModule
{

}
