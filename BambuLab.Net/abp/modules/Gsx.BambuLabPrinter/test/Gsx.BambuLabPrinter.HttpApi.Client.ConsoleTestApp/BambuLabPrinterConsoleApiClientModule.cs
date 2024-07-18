using Gsx.BambuLabPrinter.Public;
using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Gsx.BambuLabPrinter;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BambuLabPrinterPublicHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class BambuLabPrinterConsoleApiClientModule : AbpModule
{

}
