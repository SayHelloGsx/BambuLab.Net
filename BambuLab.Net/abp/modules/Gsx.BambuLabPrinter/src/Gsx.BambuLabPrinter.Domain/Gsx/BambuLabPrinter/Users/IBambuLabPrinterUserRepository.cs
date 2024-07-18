using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Users;

namespace Gsx.BambuLabPrinter.Users;

public interface IBambuLabPrinterUserRepository : IUserRepository<BambuLabPrinterUser>
{
}
