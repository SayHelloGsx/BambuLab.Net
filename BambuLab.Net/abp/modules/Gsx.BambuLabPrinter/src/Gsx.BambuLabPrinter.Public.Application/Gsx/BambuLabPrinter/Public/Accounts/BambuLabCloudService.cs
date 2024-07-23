using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BambuLab.Sdk.Http;
using Gsx.BambuLabPrinter.Accounts;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace Gsx.BambuLabPrinter.Public.Accounts;

public class BambuLabCloudService : ITransientDependency
{
    protected virtual IBambuLabAccountRepository BambuLabAccountRepository { get; }
    protected virtual IDistributedCache<AccessTokenData> TokenCache { get; }

    public BambuLabCloudService(
        IBambuLabAccountRepository bambuLabAccountRepository,
        IDistributedCache<AccessTokenData> tokenCache)
    {
        BambuLabAccountRepository = bambuLabAccountRepository;
        TokenCache = tokenCache;
    }

    public virtual async Task<BambuLabCloudRequester> CreateBambuLabCloudRequesterAndLoginAsync(Guid accountId, Guid? ownerId = null)
    {
        var bambuLabAccount = await BambuLabAccountRepository.GetAsync(accountId);
        return await CreateBambuLabCloudRequesterAndLoginAsync(bambuLabAccount);
    }

    public virtual async Task<BambuLabCloudRequester> CreateBambuLabCloudRequesterAndLoginAsync(string account, Guid? ownerId = null)
    {
        var bambuLabAccount = await BambuLabAccountRepository.GetAsync(account);
        return await CreateBambuLabCloudRequesterAndLoginAsync(bambuLabAccount);
    }

    protected virtual async Task<BambuLabCloudRequester> CreateBambuLabCloudRequesterAndLoginAsync(BambuLabAccount bambuLabAccount, Guid? ownerId = null)
    {
        if (ownerId.HasValue)
        {
            if (bambuLabAccount.OwnerId != ownerId.Value)
            {
                throw new BambuLabAccountNotBelongToOwnerException();
            }
        }

        BambuLabCloudRequester bambuLabCloudRequester = null;

        switch (bambuLabAccount.CloudType)
        {
            case BambuLabCloudTypeEnum.Public:
                bambuLabCloudRequester = new PublicBambuLabCloudRequester();
                break;

            case BambuLabCloudTypeEnum.China:
                bambuLabCloudRequester = new ChinaBambuLabCloudRequester();
                break;

            default:
                throw new BambuLabCloudTypeNotSupportException(bambuLabAccount.CloudType);
        }

        var token = await TokenCache.GetAsync(account);

        if (null == token)
        {
            await bambuLabCloudRequester.LoginAsync(bambuLabAccount.Account, bambuLabAccount.Password);
        }
        else
        {
            if (!await bambuLabCloudRequester.TryLoginAsync(token))
            {
                await bambuLabCloudRequester.LoginAsync(bambuLabAccount.Account, bambuLabAccount.Password);
            }
        }

        await TokenCache.SetAsync(account, bambuLabCloudRequester.AccessTokenData);

        return bambuLabCloudRequester;
    }
}
