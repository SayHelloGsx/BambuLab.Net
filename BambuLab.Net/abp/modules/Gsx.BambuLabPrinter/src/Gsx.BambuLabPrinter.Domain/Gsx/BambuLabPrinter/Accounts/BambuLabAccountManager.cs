using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gsx.BambuLabPrinter.Users;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Gsx.BambuLabPrinter.Accounts;

public class BambuLabAccountManager : DomainService
{
    protected IBambuLabAccountRepository AccountRepository { get; }

    public BambuLabAccountManager(IBambuLabAccountRepository accountRepository)
    {
        AccountRepository = accountRepository;
    }

    public virtual async Task<BambuLabAccount> CreateBambuLabAccountAsnyc(
        [NotNull] BambuLabPrinterUser owner,
        [NotNull] string account,
        [NotNull] string password,
        [NotNull] string userName,
        BambuLabCloudTypeEnum cloudType)
    {
        Check.NotNull(owner, nameof(owner));
        Check.NotNullOrWhiteSpace(account, nameof(account));
        Check.NotNullOrWhiteSpace(password, nameof(password));
        Check.NotNullOrWhiteSpace(userName, nameof(userName));

        var bambuLabAccount = new BambuLabAccount(GuidGenerator.Create(), owner.Id, account, password, userName, cloudType);

        await CheckAccountExistenceAsync(bambuLabAccount);

        return bambuLabAccount;
    }

    protected virtual async Task CheckAccountExistenceAsync(BambuLabAccount bambuLabAccount)
    {
        if (await AccountRepository.ExistsAsync(bambuLabAccount.Account))
        {
            throw new BambuLabAccountAlreadyExistsException(bambuLabAccount.Account);
        }
    }
}
