using Volo.Abp.Modularity;

namespace Gsx.BambuLabPrinter;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class BambuLabPrinterApplicationTestBase<TStartupModule> : BambuLabPrinterTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
