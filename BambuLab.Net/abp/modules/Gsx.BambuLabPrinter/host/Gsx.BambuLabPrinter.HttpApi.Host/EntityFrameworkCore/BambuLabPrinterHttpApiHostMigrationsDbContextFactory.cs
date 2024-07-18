using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Gsx.BambuLabPrinter.EntityFrameworkCore;

public class BambuLabPrinterHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<BambuLabPrinterHttpApiHostMigrationsDbContext>
{
    public BambuLabPrinterHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<BambuLabPrinterHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("BambuLabPrinter"));

        return new BambuLabPrinterHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
