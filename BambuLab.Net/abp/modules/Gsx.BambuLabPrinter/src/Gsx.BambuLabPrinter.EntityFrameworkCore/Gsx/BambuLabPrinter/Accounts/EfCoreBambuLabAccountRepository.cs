using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gsx.BambuLabPrinter.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Gsx.BambuLabPrinter.Accounts;

public class EfCoreBambuLabAccountRepository : EfCoreRepository<IBambuLabPrinterDbContext, BambuLabAccount, Guid>, IBambuLabAccountRepository
{
    public EfCoreBambuLabAccountRepository(IDbContextProvider<IBambuLabPrinterDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<bool> ExistsAsync(string account, CancellationToken cancellationToken = default)
    {
        Check.NotNullOrWhiteSpace(account, nameof(account));
        return await (await GetDbSetAsync()).AnyAsync(p => p.Account == account, GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<BambuLabAccount>> GetListAsync(
        Guid? ownerId = null,
        string filter = null,
        string sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default)
    {
        return await (await GetListQueryAsync(ownerId, filter))
            .OrderBy(sorting.IsNullOrEmpty() ? "creationTime desc" : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<long> GetCountAsync(Guid? ownerId = null, string filter = null, CancellationToken cancellationToken = default)
    {
        var query = await GetListQueryAsync(ownerId, filter);

        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual async Task<IQueryable<BambuLabAccount>> GetListQueryAsync(Guid? ownerId, string filter = null)
    {
        return (await GetDbSetAsync())
            .WhereIf(ownerId.HasValue, p => p.OwnerId == ownerId)
            .WhereIf(!filter.IsNullOrWhiteSpace(), p => p.Account.Contains(filter));
    }
}
