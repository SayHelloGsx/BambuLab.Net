using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Gsx.BambuLabPrinter.Public.Accounts;

public class BambuLabAccountNotBelongToOwnerException : BusinessException
{
    public BambuLabAccountNotBelongToOwnerException()
    {
        Code = BambuLabPrinterErrorCodes.Accounts.NotOwner;
    }
}
