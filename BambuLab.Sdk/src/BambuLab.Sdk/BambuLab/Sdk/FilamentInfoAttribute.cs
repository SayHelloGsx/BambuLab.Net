using System;
using System.Collections.Generic;
using System.Text;

namespace BambuLab.Sdk;

[AttributeUsage(AttributeTargets.Field)]
public class FilamentInfoAttribute : Attribute
{
    public string TrayInfoIdx { get; }
    public int NozzleTempMin { get; }
    public int NozzleTempMax { get; }
    public string TrayType { get; }

    public FilamentInfoAttribute(string trayInfoIdx, int nozzleTempMin, int nozzleTempMax, string trayType)
    {
        TrayInfoIdx = trayInfoIdx;
        NozzleTempMin = nozzleTempMin;
        NozzleTempMax = nozzleTempMax;
        TrayType = trayType;
    }
}
