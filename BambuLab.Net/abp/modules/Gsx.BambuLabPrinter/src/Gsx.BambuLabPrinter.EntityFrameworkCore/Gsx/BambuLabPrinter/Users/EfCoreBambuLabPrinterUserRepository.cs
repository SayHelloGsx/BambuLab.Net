using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gsx.BambuLabPrinter.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Users.EntityFrameworkCore;

namespace Gsx.BambuLabPrinter.Users;

public class EfCoreBambuLabPrinterUserRepository : EfCoreUserRepositoryBase<IBambuLabPrinterDbContext, BambuLabPrinterUser>, IBambuLabPrinterUserRepository
{
    public EfCoreBambuLabPrinterUserRepository(IDbContextProvider<IBambuLabPrinterDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}

