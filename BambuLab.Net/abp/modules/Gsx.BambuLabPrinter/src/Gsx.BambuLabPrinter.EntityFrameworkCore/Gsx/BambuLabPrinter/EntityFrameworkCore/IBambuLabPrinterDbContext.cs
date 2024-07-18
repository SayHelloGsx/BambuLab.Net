using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Gsx.BambuLabPrinter.EntityFrameworkCore;

[ConnectionStringName(BambuLabPrinterDbProperties.ConnectionStringName)]
public interface IBambuLabPrinterDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
