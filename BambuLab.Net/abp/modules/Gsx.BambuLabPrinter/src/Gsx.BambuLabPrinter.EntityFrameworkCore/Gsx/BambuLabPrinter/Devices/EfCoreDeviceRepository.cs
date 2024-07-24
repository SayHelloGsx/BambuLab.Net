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
using System.Linq.Dynamic.Core;
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

    public virtual async Task<List<Device>> GetListAsync(
        Guid? accountId = null,
        string filter = null,
        string sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default)
    {
        return await (await GetListQueryAsync(accountId, filter))
            .OrderBy(sorting.IsNullOrEmpty() ? "creationTime desc" : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<long> GetCountAsync(
        Guid? accountId = null,
        string filter = null)
    {
        var query = await GetListQueryAsync(accountId, filter);
        return await query.LongCountAsync();
    }

    protected virtual async Task<IQueryable<Device>> GetListQueryAsync(Guid? accountId, string filter = null)
    {
        return (await GetDbSetAsync())
            .WhereIf(accountId.HasValue, p => p.AccountId == accountId)
            .WhereIf(!filter.IsNullOrWhiteSpace(), p => p.Serial.Contains(filter) || p.Name.Contains(filter) || p.ProductName.Contains(filter));
    }
}
