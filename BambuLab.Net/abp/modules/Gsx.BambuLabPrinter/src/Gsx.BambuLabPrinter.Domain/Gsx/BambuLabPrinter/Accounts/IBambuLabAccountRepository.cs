﻿using System;
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
}
