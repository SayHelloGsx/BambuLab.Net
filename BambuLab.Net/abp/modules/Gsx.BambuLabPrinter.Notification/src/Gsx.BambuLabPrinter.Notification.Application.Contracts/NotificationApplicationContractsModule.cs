using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Gsx.BambuLabPrinter.Notification;

[DependsOn(
    typeof(BambuLabPrinterNotificationDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class NotificationApplicationContractsModule : AbpModule
{

}
