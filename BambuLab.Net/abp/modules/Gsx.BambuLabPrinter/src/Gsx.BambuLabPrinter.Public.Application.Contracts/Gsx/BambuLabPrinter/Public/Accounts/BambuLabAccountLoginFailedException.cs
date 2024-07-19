using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Gsx.BambuLabPrinter.Public.Accounts;

public class BambuLabAccountLoginFailedException : BusinessException
{
    public BambuLabAccountLoginFailedException()
    {
        Code = BambuLabPrinterErrorCodes.Accounts.LoginFailed;
    }
}
