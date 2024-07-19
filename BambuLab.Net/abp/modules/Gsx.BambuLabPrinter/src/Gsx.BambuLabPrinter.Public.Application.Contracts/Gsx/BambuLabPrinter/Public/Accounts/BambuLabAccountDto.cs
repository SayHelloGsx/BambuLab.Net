using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Gsx.BambuLabPrinter.Public.Accounts;

public class BambuLabAccountDto : ExtensibleAuditedEntityDto<Guid>
{
    public virtual Guid OwnerId { get; set; }
    public virtual string Account { get; set; }
    public virtual string Password { get; set; }
    public virtual BambuLabCloudTypeEnum CloudType { get; set; }
}
