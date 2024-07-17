using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Gsx.BambuLabPrinter.Notification;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(BambuLabPrinterNotificationDomainSharedModule)
)]
public class BambuLabPrinterNotificationDomainModule : AbpModule
{

}
