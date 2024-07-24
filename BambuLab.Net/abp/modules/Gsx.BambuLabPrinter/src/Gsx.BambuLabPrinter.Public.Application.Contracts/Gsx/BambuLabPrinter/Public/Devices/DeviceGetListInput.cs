using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Gsx.BambuLabPrinter.Public.Devices;

public class DeviceGetListInput : PagedAndSortedResultRequestDto
{
    public virtual string Filter { get; set; }
    public virtual Guid AccountId { get; set; }
}
