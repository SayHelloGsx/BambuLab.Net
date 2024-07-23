using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using BambuLab.Sdk.Http;
using Gsx.BambuLabPrinter.Accounts;
using Gsx.BambuLabPrinter.Public;
using Gsx.BambuLabPrinter.Users;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Gsx.BambuLabPrinter.Public.Accounts;

[Authorize]
public class BambuLabAccountPublicAppService : BambuLabPrinterPublicAppServiceBase, IBambuLabAccountPublicAppService
{
    protected virtual IBambuLabAccountRepository BambuLabAccountRepository { get; }
    protected virtual IBambuLabPrinterUserRepository BambuLabPrinterUserRepository { get; }
    protected virtual BambuLabAccountManager BambuLabAccountManager { get; }

    public BambuLabAccountPublicAppService(
        IBambuLabAccountRepository bambuLabAccountRepository,
        IBambuLabPrinterUserRepository bambuLabPrinterUserRepository,
        BambuLabAccountManager bambuLabAccountManager)
    {
        BambuLabAccountRepository = bambuLabAccountRepository;
        BambuLabPrinterUserRepository = bambuLabPrinterUserRepository;
        BambuLabAccountManager = bambuLabAccountManager;
    }

    public virtual async Task<PagedResultDto<BambuLabAccountDto>> GetListAsync(BambuLabAccountGetListInput input)
    {
        var totalCount = await BambuLabAccountRepository.GetCountAsync(CurrentUser.Id, input.Filter);

        var accounts = await BambuLabAccountRepository.GetListAsync(
            CurrentUser.Id,
            input.Filter,
            input.Sorting,
            input.MaxResultCount,
            input.SkipCount);

        return new PagedResultDto<BambuLabAccountDto>(totalCount, ObjectMapper.Map<List<BambuLabAccount>, List<BambuLabAccountDto>>(accounts));
    }

    public virtual async Task<BambuLabAccountDto> GetAsync(Guid id)
    {
        var account = await BambuLabAccountRepository.GetAsync(id);
        return ObjectMapper.Map<BambuLabAccount, BambuLabAccountDto>(account);
    }

    public virtual async Task<BambuLabAccountDto> CreateAsync(CreateBambuLabAccountDto input)
    {
        var bambuLabCloudRequester = CreateBambuLabCloudRequester(input.CloudType);
        var loginSuccessed = await bambuLabCloudRequester.TryLoginAsync(input.Account, input.Password);

        if (!loginSuccessed)
        {
            throw new BambuLabAccountLoginFailedException();
        }

        var user = await BambuLabPrinterUserRepository.GetAsync(CurrentUser.Id.Value);
        var account = await BambuLabAccountManager.CreateBambuLabAccountAsnyc(user, input.Account, input.Password, bambuLabCloudRequester.UserName, input.CloudType);

        await BambuLabAccountRepository.InsertAsync(account);

        return ObjectMapper.Map<BambuLabAccount, BambuLabAccountDto>(account);
    }

    public virtual async Task<BambuLabAccountDto> UpdateAsync(Guid id, UpdateBambuLabAccountDto input)
    {
        var account = await GetAccountAsync(id);

        var bambuLabCloudRequester = CreateBambuLabCloudRequester(account.CloudType);

        var loginSuccessed = await bambuLabCloudRequester.TryLoginAsync(account.Account, input.Password);

        if (!loginSuccessed)
        {
            throw new BambuLabAccountLoginFailedException();
        }

        account.SetPassword(input.Password);
        account.SetUserName(bambuLabCloudRequester.UserName);

        account = await BambuLabAccountRepository.UpdateAsync(account);

        return ObjectMapper.Map<BambuLabAccount, BambuLabAccountDto>(account);
    }

    public virtual async Task<BambuLabAccountDto> SyncUserNameAsync(Guid id)
    {
        var account = await GetAccountAsync(id);
        var bambuLabCloudRequester = CreateBambuLabCloudRequester(account.CloudType);

        var loginSuccessed = await bambuLabCloudRequester.TryLoginAsync(account.Account, account.Password);

        if (!loginSuccessed)
        {
            throw new BambuLabAccountLoginFailedException();
        }

        account.SetUserName(bambuLabCloudRequester.UserName);
        account = await BambuLabAccountRepository.UpdateAsync(account);
        return ObjectMapper.Map<BambuLabAccount, BambuLabAccountDto>(account);
    }

    protected virtual BambuLabCloudRequester CreateBambuLabCloudRequester(BambuLabCloudTypeEnum cloudType)
    {
        switch (cloudType)
        {
            case BambuLabCloudTypeEnum.Public:
                return new PublicBambuLabCloudRequester();

            case BambuLabCloudTypeEnum.China:
                return new ChinaBambuLabCloudRequester();

            default:
                throw new BambuLabCloudTypeNotSupportException(cloudType);
        }
    }

    private async Task<BambuLabAccount> GetAccountAsync(Guid id) 
    {
        var account = await BambuLabAccountRepository.GetAsync(id);

        if (account.OwnerId != CurrentUser.Id.Value)
        {
            throw new BambuLabAccountNotBelongToOwnerException();
        }

        return account;
    }
}
