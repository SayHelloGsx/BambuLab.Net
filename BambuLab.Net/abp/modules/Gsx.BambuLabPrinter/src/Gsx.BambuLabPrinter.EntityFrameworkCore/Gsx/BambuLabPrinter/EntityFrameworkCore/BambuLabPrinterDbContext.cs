using Gsx.BambuLabPrinter.Accounts;
using Gsx.BambuLabPrinter.Devices;
using Gsx.BambuLabPrinter.Users;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Gsx.BambuLabPrinter.EntityFrameworkCore;

[ConnectionStringName(BambuLabPrinterDbProperties.ConnectionStringName)]
public class BambuLabPrinterDbContext : AbpDbContext<BambuLabPrinterDbContext>, IBambuLabPrinterDbContext
{
    public DbSet<BambuLabPrinterUser> User { get; set; }
    public DbSet<BambuLabAccount> BambuLabAccounts { get; set; }
    public DbSet<Device> Devices { get; set; }


    public BambuLabPrinterDbContext(DbContextOptions<BambuLabPrinterDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureBambuLabPrinter();
    }
}
