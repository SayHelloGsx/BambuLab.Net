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
}
