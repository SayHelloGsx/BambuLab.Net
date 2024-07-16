using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Gsx.BambuLabPrinter.Notification.MongoDB;

[ConnectionStringName(NotificationDbProperties.ConnectionStringName)]
public class NotificationMongoDbContext : AbpMongoDbContext, INotificationMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureNotification();
    }
}
