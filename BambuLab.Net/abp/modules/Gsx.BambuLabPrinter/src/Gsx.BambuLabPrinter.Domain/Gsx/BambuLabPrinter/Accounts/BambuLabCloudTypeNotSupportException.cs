using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Gsx.BambuLabPrinter.Accounts;

public class BambuLabCloudTypeNotSupportException : BusinessException
{
    public virtual BambuLabCloudTypeEnum CloudType { get; }

    public BambuLabCloudTypeNotSupportException(BambuLabCloudTypeEnum cloudType)
    {
        CloudType = cloudType;
        Code = BambuLabPrinterErrorCodes.Accounts.CloudTypeNotSupport;
        WithData(nameof(BambuLabAccount.CloudType), cloudType);
    }
}
