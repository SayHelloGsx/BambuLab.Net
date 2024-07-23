using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gsx.BambuLabPrinter.Public.Devices;

public class CreateDeviceListDto
{
    public virtual Guid AccountId { get; set; }
    public virtual string[] Serials { get; set; }
}
