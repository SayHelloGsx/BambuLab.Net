using Gsx.BambuLabPrinter.Notification.MongoDB;
using Gsx.BambuLabPrinter.Notification.Samples;
using Xunit;

namespace Gsx.BambuLabPrinter.Notification.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<NotificationMongoDbTestModule>
{

}
