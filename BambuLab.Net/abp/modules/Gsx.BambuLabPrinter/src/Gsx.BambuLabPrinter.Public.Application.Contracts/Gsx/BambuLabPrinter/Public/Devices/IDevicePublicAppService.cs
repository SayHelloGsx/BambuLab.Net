using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Gsx.BambuLabPrinter.Public.Devices;

public interface IDevicePublicAppService : IApplicationService
{
    Task<DeviceDto> GetAsync(Guid id);
    Task<List<DeviceFromCloudDto>> GetListFromCloudAsync(string account);
    Task<List<DeviceDto>> CreateListAsync(CreateDeviceListDto input);
    Task<PagedResultDto<DeviceDto>> GetListAsync(DeviceGetListInput input);
    Task<DeviceDto> SyncFromCloudAsync(Guid id);
    Task DeleteAsync(Guid id);
}
