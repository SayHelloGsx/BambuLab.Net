using Volo.Abp.Modularity;

namespace Gsx.BambuLabPrinter.Notification;

[DependsOn(
    typeof(BambuLabPrinterNotificationDomainModule),
    typeof(NotificationTestBaseModule)
)]
public class NotificationDomainTestModule : AbpModule
{

}
