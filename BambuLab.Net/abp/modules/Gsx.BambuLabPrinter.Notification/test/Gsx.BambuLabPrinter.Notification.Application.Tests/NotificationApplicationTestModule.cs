using Volo.Abp.Modularity;

namespace Gsx.BambuLabPrinter.Notification;

[DependsOn(
    typeof(NotificationApplicationModule),
    typeof(NotificationDomainTestModule)
    )]
public class NotificationApplicationTestModule : AbpModule
{

}
