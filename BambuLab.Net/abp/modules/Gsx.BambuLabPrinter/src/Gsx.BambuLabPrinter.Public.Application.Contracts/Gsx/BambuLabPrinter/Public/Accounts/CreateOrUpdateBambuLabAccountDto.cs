using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gsx.BambuLabPrinter.Public.Accounts;

public abstract class CreateOrUpdateBambuLabAccountDto
{
    public virtual string Password { get; set; }
}
