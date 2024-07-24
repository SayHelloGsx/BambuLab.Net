using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BambuLab.Sdk.Http;
using Gsx.BambuLabPrinter.Accounts;
using Gsx.BambuLabPrinter.Devices;
using Gsx.BambuLabPrinter.Public.Accounts;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;

namespace Gsx.BambuLabPrinter.Public.Devices;

[Authorize]
public class DevicePublicAppService : BambuLabPrinterPublicAppServiceBase, IDevicePublicAppService
{
    protected virtual IDeviceRepository DeviceRepository { get; }
    protected virtual IBambuLabAccountRepository BambuLabAccountRepository { get; }
    protected virtual DeviceManager DeviceManager { get; }
    protected virtual BambuLabCloudService BambuLabCloudService { get; }

    public DevicePublicAppService(
        IDeviceRepository deviceRepository,
        IBambuLabAccountRepository bambuLabAccountRepository,
        DeviceManager deviceManager,
        BambuLabCloudService bambuLabCloudService)
    {
        DeviceRepository = deviceRepository;
        BambuLabAccountRepository = bambuLabAccountRepository;
        DeviceManager = deviceManager;
        BambuLabCloudService = bambuLabCloudService;
    }

    public virtual async Task<DeviceDto> GetAsync(Guid id)
    {
        var device = await DeviceRepository.GetAsync(id);
        return ObjectMapper.Map<Device, DeviceDto>(device);
    }

    public virtual async Task<List<DeviceFromCloudDto>> GetListFromCloudAsync(string account)
    {
        var bambuLabCloudRequester = await BambuLabCloudService.CreateBambuLabCloudRequesterAndLoginAsync(account, CurrentUser.Id.Value);
        return await GetListFromCloudAsync(bambuLabCloudRequester);
    }

    public virtual async Task<List<DeviceDto>> CreateListAsync(CreateDeviceListDto input)
    {
        var bambuLabCloudRequester = await BambuLabCloudService.CreateBambuLabCloudRequesterAndLoginAsync(input.AccountId, CurrentUser.Id.Value);
        var deviceFromCloudDtoList = await GetListFromCloudAsync(bambuLabCloudRequester);
        
        var bambuLabAccount = await BambuLabAccountRepository.GetAsync(input.AccountId);

        var deviceList = new List<Device>();

        foreach (var deviceFromCloudDto in deviceFromCloudDtoList.Where(p => input.Serials.Contains(p.Serial)))
        {
            var device = await DeviceManager.CreateDeviceAsync(bambuLabAccount, deviceFromCloudDto.Serial, deviceFromCloudDto.Name, deviceFromCloudDto.ModelName, deviceFromCloudDto.ProductName, deviceFromCloudDto.AccessCode);
            deviceList.Add(device);
        }

        await DeviceRepository.InsertManyAsync(deviceList);
        return ObjectMapper.Map<List<Device>, List<DeviceDto>>(deviceList);
    }

    public virtual async Task<PagedResultDto<DeviceDto>> GetListAsync(DeviceGetListInput input)
    {
        await CheckOwnerAsync(input.AccountId);

        var totalCount = await DeviceRepository.GetCountAsync(input.AccountId, input.Filter);
        var deviceList = await DeviceRepository.GetListAsync(input.AccountId, input.Filter, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<DeviceDto>(totalCount, ObjectMapper.Map<List<Device>, List<DeviceDto>>(deviceList));
    }

    public virtual async Task<DeviceDto> SyncFromCloudAsync(Guid id)
    {
        var device = await DeviceRepository.GetAsync(id);
        await CheckOwnerAsync(device.AccountId);
        var bambuLabCloudRequester = await BambuLabCloudService.CreateBambuLabCloudRequesterAndLoginAsync(device.AccountId, CurrentUser.Id.Value);
        var deviceFromCloudDtoList = await GetListFromCloudAsync(bambuLabCloudRequester);
        var deviceFromCloud = deviceFromCloudDtoList.FirstOrDefault(p => p.Serial == device.Serial);

        if (null == deviceFromCloud)
        {
            throw new DeviceNotFoundFromCloudException();
        }

        device.SetName(deviceFromCloud.Name);
        device.SetAccessCode(deviceFromCloud.AccessCode);
        device = await DeviceRepository.UpdateAsync(device);

        return ObjectMapper.Map<Device, DeviceDto>(device);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        var device = await DeviceRepository.GetAsync(id);
        await CheckOwnerAsync(device.AccountId);
        await DeviceRepository.DeleteAsync(device);
    }

    protected virtual async Task CheckOwnerAsync(Guid accountId)
    {
        var bambuLabAccount = await BambuLabAccountRepository.GetAsync(accountId);

        if (bambuLabAccount.OwnerId != CurrentUser.Id.Value)
        {
            throw new BambuLabAccountNotBelongToOwnerException();
        }
    }

    protected static async Task<List<DeviceFromCloudDto>> GetListFromCloudAsync(BambuLabCloudRequester bambuLabCloudRequester)
    {
        var devices = await bambuLabCloudRequester.GetDeviceListAsync();

        var deviceDtoList = new List<DeviceFromCloudDto>();

        for (int i = 0, imax = devices.Devices.Count; i < imax; i++)
        {
            deviceDtoList.Add(new DeviceFromCloudDto
            {
                Serial = devices.GetDevId(i),
                Name = devices.GetName(i),
                ModelName = devices.GetDevModelName(i),
                ProductName = devices.GetDevProductName(i),
                AccessCode = devices.GetDevAccessCode(i)
            });
        }

        return deviceDtoList;
    }
}
