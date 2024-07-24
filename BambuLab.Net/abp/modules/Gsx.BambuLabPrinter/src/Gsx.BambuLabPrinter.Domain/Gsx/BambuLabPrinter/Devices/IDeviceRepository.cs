using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Gsx.BambuLabPrinter.Devices;

public interface IDeviceRepository : IBasicRepository<Device, Guid>
{
    Task<bool> ExistsAsync(string serial, CancellationToken cancellationToken = default);

    Task<List<Device>> GetListAsync(
        Guid? accountId = null,
        string filter = null,
        string sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default);

    Task<long> GetCountAsync(
        Guid? accountId = null,
        string filter = null);
}
