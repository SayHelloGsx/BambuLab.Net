using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Gsx.BambuLabPrinter.Accounts;

public class BambuLabAccountAlreadyExistsException : BusinessException
{
    public virtual string Account { get; }

    public BambuLabAccountAlreadyExistsException(string account)
    {
        Account = account;
        Code = BambuLabPrinterErrorCodes.Accounts.AccountAlreadyExist;
        WithData(nameof(BambuLabAccount.Account), account);
    }
}
