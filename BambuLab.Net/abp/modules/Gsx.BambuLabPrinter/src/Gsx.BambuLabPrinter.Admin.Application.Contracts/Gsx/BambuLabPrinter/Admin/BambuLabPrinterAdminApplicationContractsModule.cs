using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Gsx.BambuLabPrinter.Admin;

[DependsOn(
    typeof(BambuLabPrinterDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class BambuLabPrinterAdminApplicationContractsModule : AbpModule
{

}
