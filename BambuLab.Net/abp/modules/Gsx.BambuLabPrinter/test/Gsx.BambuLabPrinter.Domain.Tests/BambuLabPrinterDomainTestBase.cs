using Volo.Abp.Modularity;

namespace Gsx.BambuLabPrinter;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class BambuLabPrinterDomainTestBase<TStartupModule> : BambuLabPrinterTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
