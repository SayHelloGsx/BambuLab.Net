using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Gsx.BambuLabPrinter.Accounts;

public interface IBambuLabAccountRepository : IBasicRepository<BambuLabAccount, Guid>
{
    Task<bool> ExistsAsync(string account, CancellationToken cancellationToken = default);

    Task<List<BambuLabAccount>> GetListAsync(
        Guid? ownerId = null,
        string filter = null,
        string sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default);

    Task<long> GetCountAsync(Guid? ownerId = null, string filter = null, CancellationToken cancellationToken = default);
}
