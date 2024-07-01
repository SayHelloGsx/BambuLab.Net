using System;
using System.Collections.Generic;
using System.Text;

namespace BambuLab.Sdk;

public class AMS
{
    /// <summary>
    /// Represents the Bambulab's AMS (Automated Material System) system.
    /// </summary>
    public Dictionary<int, FilamentTray> FilamentTrays { get; private set; }

    public string Humidity { get; private set; }
    public float Temperature { get; private set; }

    public AMS(string humidity, float temperature)
    {
        FilamentTrays = new Dictionary<int, FilamentTray>();
        Humidity = humidity;
        Temperature = temperature;
    }

    /// <summary>
    /// Set the filament tray at the given index. Will overwrite any existing tray at the given index.
    /// </summary>
    /// <param name="filamentTray">Description of the filament tray</param>
    /// <param name="trayIndex">Tray index</param>
    public void SetFilamentTray(FilamentTray filamentTray, int trayIndex)
    {
        FilamentTrays[trayIndex] = filamentTray;
    }

    /// <summary>
    /// Get the filament tray at the given index. If no tray exists at the index, return null.
    /// </summary>
    /// <param name="trayIndex">Tray index of the filament tray</param>
    /// <returns>Filament tray at the given index</returns>
    public FilamentTray GetFilamentTray(int trayIndex)
    {
        FilamentTrays.TryGetValue(trayIndex, out var filamentTray);
        return filamentTray;
    }
}
