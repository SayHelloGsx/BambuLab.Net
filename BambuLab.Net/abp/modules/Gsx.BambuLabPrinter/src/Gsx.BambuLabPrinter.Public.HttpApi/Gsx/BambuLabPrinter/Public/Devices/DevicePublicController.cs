using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Gsx.BambuLabPrinter.Public.Devices;

[RemoteService(Name = BambuLabPrinterPublicRemoteServiceConsts.RemoteServiceName)]
[Area(BambuLabPrinterPublicRemoteServiceConsts.ModuleName)]
[ControllerName("DevicePublic")]
[Route("api/bambulab-printer/public/devices")]
public class DevicePublicController : AbpControllerBase, IDevicePublicAppService
{
    private readonly IDevicePublicAppService _devicePublicAppService;

    public DevicePublicController(IDevicePublicAppService devicePublicAppService)
    {
        _devicePublicAppService = devicePublicAppService;
    }

    [HttpPost]
    public Task<List<DeviceDto>> CreateListAsync(CreateDeviceListDto input)
    {
        return _devicePublicAppService.CreateListAsync(input);
    }

    [HttpDelete]
    [Route("{id}")]
    public Task DeleteAsync(Guid id)
    {
        return _devicePublicAppService.DeleteAsync(id);
    }

    [HttpGet]
    [Route("{id}")]
    public Task<DeviceDto> GetAsync(Guid id)
    {
        return _devicePublicAppService.GetAsync(id);
    }

    [HttpGet]
    public Task<PagedResultDto<DeviceDto>> GetListAsync(DeviceGetListInput input)
    {
        return _devicePublicAppService.GetListAsync(input);
    }

    [HttpGet]
    [Route("{account}/from-cloud")]
    public Task<List<DeviceFromCloudDto>> GetListFromCloudAsync(string account)
    {
        return _devicePublicAppService.GetListFromCloudAsync(account);
    }

    [HttpPut]
    [Route("{id}/sync")]
    public Task<DeviceDto> SyncFromCloudAsync(Guid id)
    {
        return _devicePublicAppService.SyncFromCloudAsync(id);
    }
}
