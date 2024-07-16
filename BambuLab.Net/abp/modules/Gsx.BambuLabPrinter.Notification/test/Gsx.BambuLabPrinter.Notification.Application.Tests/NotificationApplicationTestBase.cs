﻿using Volo.Abp.Modularity;

namespace Gsx.BambuLabPrinter.Notification;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class NotificationApplicationTestBase<TStartupModule> : NotificationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
