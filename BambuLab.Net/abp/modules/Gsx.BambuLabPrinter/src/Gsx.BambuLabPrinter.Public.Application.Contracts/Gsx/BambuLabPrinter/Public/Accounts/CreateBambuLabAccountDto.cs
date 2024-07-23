using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gsx.BambuLabPrinter.Public.Accounts;

public class CreateBambuLabAccountDto : CreateOrUpdateBambuLabAccountDto
{
    public virtual string Account { get; set; }
    public virtual BambuLabCloudTypeEnum CloudType { get; set; }
}
