using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gsx.BambuLabPrinter.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Gsx.BambuLabPrinter.Devices;

public class EfCoreDeviceRepository : EfCoreRepository<IBambuLabPrinterDbContext, Device, Guid>, IDeviceRepository
{
    public EfCoreDeviceRepository(IDbContextProvider<IBambuLabPrinterDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task<bool> ExistsAsync(string serial, CancellationToken cancellationToken = default)
    {
        Check.NotNullOrWhiteSpace(serial, nameof(serial));
        return await (await GetDbSetAsync()).AnyAsync(p => p.Serial == serial, GetCancellationToken(cancellationToken));
    }
}
