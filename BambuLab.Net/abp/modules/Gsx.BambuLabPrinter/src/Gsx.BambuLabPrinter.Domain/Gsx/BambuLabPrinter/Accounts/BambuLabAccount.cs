using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Gsx.BambuLabPrinter.Accounts;

public class BambuLabAccount : FullAuditedAggregateRoot<Guid>
{
    public virtual Guid OwnerId { get; protected set; }
    public virtual string Account { get; protected set; }
    public virtual string Password { get; protected set; }
    public virtual string UserName { get; protected set; }
    public virtual BambuLabCloudTypeEnum CloudType { get; protected set; }

    protected BambuLabAccount()
    {
    }

    internal BambuLabAccount(
        Guid id,
        Guid ownerId,
        [NotNull] string account,
        [NotNull] string password,
        [NotNull] string userName,
        BambuLabCloudTypeEnum cloudType
        ) : base(id)
    {
        Check.NotNullOrWhiteSpace(account, nameof(account));

        Account = account;
        SetPassword(password);
        SetUserName(userName);
        CloudType = cloudType;
        OwnerId = ownerId;
    }

    public virtual void SetPassword(string password)
    {
        Password = Check.NotNullOrWhiteSpace(password, nameof(password));
    }

    public virtual void SetUserName(string userName)
    {
        UserName = Check.NotNullOrWhiteSpace(userName, nameof(userName));
    }
}
