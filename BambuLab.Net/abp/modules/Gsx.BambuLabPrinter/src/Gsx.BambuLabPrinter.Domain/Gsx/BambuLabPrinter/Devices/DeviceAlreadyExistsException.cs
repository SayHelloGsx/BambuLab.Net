using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Gsx.BambuLabPrinter.Devices;

public class DeviceAlreadyExistsException : BusinessException
{
    public virtual string Serial { get; }

    public DeviceAlreadyExistsException(string serial)
    {
        Serial = serial;
        Code = BambuLabPrinterErrorCodes.Accounts.AccountAlreadyExist;
        WithData(nameof(serial), serial);
    }
}
