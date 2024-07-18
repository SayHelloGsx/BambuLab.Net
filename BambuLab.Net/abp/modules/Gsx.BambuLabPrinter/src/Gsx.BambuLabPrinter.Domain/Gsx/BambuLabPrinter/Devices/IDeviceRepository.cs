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
}
