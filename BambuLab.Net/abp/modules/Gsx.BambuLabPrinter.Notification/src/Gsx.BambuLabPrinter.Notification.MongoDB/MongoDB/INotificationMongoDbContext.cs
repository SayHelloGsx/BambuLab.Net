using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Gsx.BambuLabPrinter.Notification.MongoDB;

[ConnectionStringName(NotificationDbProperties.ConnectionStringName)]
public interface INotificationMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
