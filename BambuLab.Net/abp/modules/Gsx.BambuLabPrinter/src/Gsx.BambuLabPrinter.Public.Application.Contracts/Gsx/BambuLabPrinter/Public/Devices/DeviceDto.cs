using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Gsx.BambuLabPrinter.Public.Devices;

public class DeviceDto : ExtensibleAuditedEntityDto<Guid>
{
    public virtual Guid AccountId { get; set; }
    public virtual string Serial { get; set; }
    public virtual string Name { get; set; }
    public virtual string ModelName { get; set; }
    public virtual string ProductName { get; set; }
    public virtual string AccessCode { get; set; }
}
