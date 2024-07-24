using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Gsx.BambuLabPrinter.Public.Devices;

public class DeviceNotFoundFromCloudException : BusinessException
{
    public DeviceNotFoundFromCloudException()
    {
        Code = BambuLabPrinterErrorCodes.Devices.NotFoundFromCloud;
    }
}
