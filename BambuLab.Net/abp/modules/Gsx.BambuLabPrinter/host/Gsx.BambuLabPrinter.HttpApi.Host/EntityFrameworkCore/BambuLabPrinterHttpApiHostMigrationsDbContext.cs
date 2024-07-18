using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Gsx.BambuLabPrinter.EntityFrameworkCore;

public class BambuLabPrinterHttpApiHostMigrationsDbContext : AbpDbContext<BambuLabPrinterHttpApiHostMigrationsDbContext>
{
    public BambuLabPrinterHttpApiHostMigrationsDbContext(DbContextOptions<BambuLabPrinterHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureBambuLabPrinter();
    }
}
