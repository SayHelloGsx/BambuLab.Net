using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Gsx.BambuLabPrinter.Public;

[DependsOn(
    typeof(BambuLabPrinterDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class BambuLabPrinterPublicApplicationContractsModule : AbpModule
{

}
