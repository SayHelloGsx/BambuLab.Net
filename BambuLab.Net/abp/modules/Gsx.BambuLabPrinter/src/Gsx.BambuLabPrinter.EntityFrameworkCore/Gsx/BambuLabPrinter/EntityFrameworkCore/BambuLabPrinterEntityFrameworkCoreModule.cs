using Gsx.BambuLabPrinter.Accounts;
using Gsx.BambuLabPrinter.Devices;
using Gsx.BambuLabPrinter.Users;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Gsx.BambuLabPrinter.EntityFrameworkCore;

[DependsOn(
    typeof(BambuLabPrinterDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class BambuLabPrinterEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<BambuLabPrinterDbContext>(options =>
        {
            options.AddRepository<BambuLabAccount, EfCoreBambuLabAccountRepository>();
            options.AddRepository<Device, EfCoreDeviceRepository>();
            options.AddRepository<BambuLabPrinterUser, EfCoreBambuLabPrinterUserRepository>();
        });
    }
}
