using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BambuLab.Sdk;
public interface IPrinterOperator : IAsyncDisposable
{
    Task<bool> TurnLightOnAsync();

    Task<bool> TurnLightOffAsync();

    Task<bool> StartPrintAsync(string filename, int plateNumber, bool useAms = true, List<int> amsMapping = null);

    Task<bool> PausePrintAsync();

    Task<bool> ResumePrintAsync();

    Task<bool> StopPrintAsync();

    Task<bool> ResumeFilamentActionAsync();

    Task<bool> SetBedTemperatureAsync(int temperature);

    Task<bool> HomePrinterAsync();

    Task<bool> SetPrintSpeedAsync(PrintSpeedEnum speedLevel);
}
