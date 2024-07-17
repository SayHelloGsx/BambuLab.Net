using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Gsx.BambuLabPrinter.Notification.Devices;

public class Device : FullAuditedAggregateRoot<Guid>
{
    public virtual Guid AccountId { get; protected set; }
    public virtual string Serial { get; protected set; }
    public virtual string ModelName { get; protected set; }
    public virtual string ProductName { get; protected set; }
    public virtual string AccessCode { get; protected set; }

    protected Device()
    {
    }

    public Device(
        Guid id,
        Guid accountId,
        [NotNull] string serial,
        [NotNull] string modelName,
        [NotNull] string productName,
        [NotNull] string accessCode
        ) : base(id)
    {
        Check.NotNullOrWhiteSpace(serial, nameof(serial));
        Check.NotNullOrWhiteSpace(modelName, nameof(modelName));
        Check.NotNullOrWhiteSpace(productName, nameof(productName));
        Check.NotNullOrWhiteSpace(accessCode, nameof(accessCode));

        Serial = serial;
        ModelName = modelName;
        ProductName = productName;
        AccessCode = accessCode;
        AccountId = accountId;
    }
}
