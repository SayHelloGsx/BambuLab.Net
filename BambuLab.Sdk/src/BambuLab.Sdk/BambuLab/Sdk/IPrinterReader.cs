using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BambuLab.Sdk;

public interface IPrinterReader : IAsyncDisposable
{
    bool IsMqttConnected { get; }

    Task<int?> GetRemainingTimeAsync();

    Task<int?> GetPercentageAsync();

    Task<GcodeStateEnum> GetPrinterStateAsync();

    Task<int?> GetPrintSpeedAsync();

    Task<float> GetBedTemperature();

    Task<float> GetNozzleTemperatureAsync();

    Task<float?> GetNozzleTemperatureTargetAsync();

    Task<int> CurrentLayerNumAsync();

    Task<int> TotalLayerNumAsync();

    Task<int?> GetGcodeFilePreparePercentageAsync();

    Task<float> GetNozzleDiameterAsync();

    Task<NozzleTypeEnum> GetNozzleTypeAsync();

    Task<string> GetFileNameAsync();

    Task<LightStateEnum> GetLightStateAsync();

    Task<PrintStatusEnum> GetCurrentStateAsync();
}
