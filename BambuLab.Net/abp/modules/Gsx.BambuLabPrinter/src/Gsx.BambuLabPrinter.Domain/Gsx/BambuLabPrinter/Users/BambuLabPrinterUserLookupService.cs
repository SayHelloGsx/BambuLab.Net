using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace Gsx.BambuLabPrinter.Users;

public class BambuLabPrinterUserLookupService : UserLookupService<BambuLabPrinterUser, IBambuLabPrinterUserRepository>, IBambuLabPrinterUserLookupService
{
    public BambuLabPrinterUserLookupService(
        IBambuLabPrinterUserRepository userRepository,
        IUnitOfWorkManager unitOfWorkManager)
        : base(
            userRepository,
            unitOfWorkManager)
    {

    }

    protected override BambuLabPrinterUser CreateUser(IUserData externalUser)
    {
        return new BambuLabPrinterUser(externalUser);
    }
}
