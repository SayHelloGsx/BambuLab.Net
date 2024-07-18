using Gsx.BambuLabPrinter.Accounts;
using Gsx.BambuLabPrinter.Devices;
using Gsx.BambuLabPrinter.Localization.Accounts;
using Gsx.BambuLabPrinter.Localization.Devices;
using Gsx.BambuLabPrinter.Users;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Users.EntityFrameworkCore;

namespace Gsx.BambuLabPrinter.EntityFrameworkCore;

public static class BambuLabPrinterDbContextModelCreatingExtensions
{
    public static void ConfigureBambuLabPrinter(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<BambuLabPrinterUser>(b =>
        {
            b.ToTable(BambuLabPrinterDbProperties.DbTablePrefix + "Users", BambuLabPrinterDbProperties.DbSchema);

            b.ConfigureByConvention();
            b.ConfigureAbpUser();

            b.HasIndex(p => new { p.TenantId, p.UserName });
            b.HasIndex(p => new { p.TenantId, p.Email });

            b.ApplyObjectExtensionMappings();
        });

        builder.Entity<BambuLabAccount>(b =>
        {
            b.ToTable(BambuLabPrinterDbProperties.DbTablePrefix + "Accounts", BambuLabPrinterDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.Property(p => p.Account).IsRequired().HasMaxLength(BambuLabAccountConsts.MaxAccountLength);

            b.HasIndex(p => p.Account);
            b.HasIndex(p => p.OwnerId);
        });

        builder.Entity<Device>(b =>
        {
            b.ToTable(BambuLabPrinterDbProperties.DbTablePrefix + "Devices", BambuLabPrinterDbProperties.DbSchema);

            b.ConfigureByConvention();

            b.Property(p => p.Name).HasMaxLength(DeviceConsts.MaxNameLength);
            b.Property(p => p.Serial).IsRequired().HasMaxLength(DeviceConsts.MaxSerialLength);
            b.Property(p => p.ModelName).HasMaxLength(DeviceConsts.MaxModelNameLength);
            b.Property(p => p.ProductName).HasMaxLength(DeviceConsts.MaxProductNameLength);
            b.Property(p => p.AccessCode).HasMaxLength(DeviceConsts.MaxAccessCodeLength);

            b.HasIndex(p => p.Name);
            b.HasIndex(p => p.AccountId);
        });
    }
}
