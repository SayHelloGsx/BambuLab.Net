using Volo.Abp.Modularity;

namespace Gsx.BambuLabPrinter.Notification;

[DependsOn(
    typeof(NotificationDomainModule),
    typeof(NotificationTestBaseModule)
)]
public class NotificationDomainTestModule : AbpModule
{

}
