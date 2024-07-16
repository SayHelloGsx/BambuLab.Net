using Gsx.BambuLabPrinter.Notification.Samples;
using Xunit;

namespace Gsx.BambuLabPrinter.Notification.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<NotificationMongoDbTestModule>
{

}
