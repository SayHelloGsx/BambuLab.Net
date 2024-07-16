﻿using Gsx.BambuLabPrinter.Notification.Samples;
using Xunit;

namespace Gsx.BambuLabPrinter.Notification.MongoDB.Samples;

[Collection(MongoTestCollection.Name)]
public class SampleRepository_Tests : SampleRepository_Tests<NotificationMongoDbTestModule>
{
    /* Don't write custom repository tests here, instead write to
     * the base class.
     * One exception can be some specific tests related to MongoDB.
     */
}