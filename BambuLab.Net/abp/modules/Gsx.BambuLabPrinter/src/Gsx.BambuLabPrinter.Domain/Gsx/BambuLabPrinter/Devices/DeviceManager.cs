using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gsx.BambuLabPrinter.Accounts;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Gsx.BambuLabPrinter.Devices;

public class DeviceManager : DomainService
{
    protected virtual IDeviceRepository DeviceRepository { get; }

    public DeviceManager(IDeviceRepository deviceRepository)
    {
        DeviceRepository = deviceRepository;
    }

    public virtual async Task<Device> CreateDeviceAsync(
        [NotNull] BambuLabAccount bambuLabAccount,
        [NotNull] string serial,
        [NotNull] string name,
        [NotNull] string modelName,
        [NotNull] string productName,
        [NotNull] string accessCode)
    {
        Check.NotNull(bambuLabAccount, nameof(bambuLabAccount));
        Check.NotNullOrWhiteSpace(serial, nameof(serial));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.NotNullOrWhiteSpace(modelName, nameof(modelName));
        Check.NotNullOrWhiteSpace(productName, nameof(productName));

        var device = new Device(GuidGenerator.Create(), bambuLabAccount.Id, serial, name, modelName, productName, accessCode);

        await CheckDeviceExistenceAsync(device);

        return device;
    }

    protected virtual async Task CheckDeviceExistenceAsync(Device device)
    {
        if (await DeviceRepository.ExistsAsync(device.Serial))
        {
            throw new DeviceAlreadyExistsException(device.Serial);
        }
    }
}
