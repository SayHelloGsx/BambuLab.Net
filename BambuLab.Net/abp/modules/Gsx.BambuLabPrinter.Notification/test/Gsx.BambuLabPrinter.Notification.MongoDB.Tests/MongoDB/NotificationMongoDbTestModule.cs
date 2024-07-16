using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace Gsx.BambuLabPrinter.Notification.MongoDB;

[DependsOn(
    typeof(NotificationApplicationTestModule),
    typeof(NotificationMongoDbModule)
)]
public class NotificationMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
