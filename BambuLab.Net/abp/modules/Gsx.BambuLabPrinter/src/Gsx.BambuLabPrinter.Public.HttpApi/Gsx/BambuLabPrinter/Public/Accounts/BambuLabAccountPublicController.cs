using System;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Gsx.BambuLabPrinter.Public.Accounts;

[RemoteService(Name = BambuLabPrinterPublicRemoteServiceConsts.RemoteServiceName)]
[Area(BambuLabPrinterPublicRemoteServiceConsts.ModuleName)]
[ControllerName("BambuLabAccountPublic")]
[Route("api/bambulab-printer/public/bambulab-accounts")]
public class BambuLabAccountPublicController : AbpControllerBase, IBambuLabAccountPublicAppService
{
    private readonly IBambuLabAccountPublicAppService _bambuLabAccountPublicAppService;

    public BambuLabAccountPublicController(IBambuLabAccountPublicAppService bambuLabAccountPublicAppService)
    {
        _bambuLabAccountPublicAppService = bambuLabAccountPublicAppService;
    }

    [HttpPost]
    public Task<BambuLabAccountDto> CreateAsync(CreateBambuLabAccountDto input)
    {
        return _bambuLabAccountPublicAppService.CreateAsync(input);
    }

    [HttpDelete]
    public Task DeleteAsync(Guid id)
    {
        return _bambuLabAccountPublicAppService.DeleteAsync(id);
    }

    [HttpGet]
    [Route("{id}")]
    public Task<BambuLabAccountDto> GetAsync(Guid id)
    {
        return _bambuLabAccountPublicAppService.GetAsync(id);
    }

    [HttpGet]
    public Task<PagedResultDto<BambuLabAccountDto>> GetListAsync(BambuLabAccountGetListInput input)
    {
        return _bambuLabAccountPublicAppService.GetListAsync(input);
    }

    [HttpPut]
    [Route("{id}/sync-user-name")]
    public Task<BambuLabAccountDto> SyncUserNameAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    [Route("{id}")]
    public Task<BambuLabAccountDto> UpdateAsync(Guid id, UpdateBambuLabAccountDto input)
    {
        throw new NotImplementedException();
    }
}
