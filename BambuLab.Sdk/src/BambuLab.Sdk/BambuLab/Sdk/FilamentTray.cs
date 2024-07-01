using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BambuLab.Sdk;

public class FilamentTray
{
    public float K { get; set; }
    public int N { get; set; }
    public string TagUid { get; set; }
    public string TrayIdName { get; set; }
    public string TrayInfoIdx { get; set; }
    public string TrayType { get; set; }
    public string TraySubBrands { get; set; }
    public string TrayColor { get; set; }
    public string TrayWeight { get; set; }
    public string TrayDiameter { get; set; }
    public string TrayTemp { get; set; }
    public string TrayTime { get; set; }
    public string BedTempType { get; set; }
    public string BedTemp { get; set; }
    public int NozzleTempMax { get; set; }
    public int NozzleTempMin { get; set; }
    public string XCamInfo { get; set; }
    public string TrayUuid { get; set; }

    public static HashSet<string> Keys()
    {
        return new HashSet<string>(typeof(FilamentTray).GetProperties().Select(p => p.Name));
    }

    public static FilamentTray FromDictionary(Dictionary<string, object> dict)
    {
        var tray = new FilamentTray();
        var properties = typeof(FilamentTray).GetProperties();

        foreach (var prop in properties)
        {
            if (dict.ContainsKey(prop.Name))
            {
                prop.SetValue(tray, Convert.ChangeType(dict[prop.Name], prop.PropertyType));
            }
        }

        return tray;
    }

    public FilamentEnum Filament
    {
        get
        {
            return Extensions.GetEnumValues<FilamentEnum>().First(p =>
            {
                var attr = p.GetAttribute<FilamentInfoAttribute>();

                return attr.TrayInfoIdx == TrayInfoIdx &&
                       attr.NozzleTempMin == NozzleTempMin &&
                       attr.NozzleTempMax == NozzleTempMax &&
                       attr.TrayType == TrayType;
            });
        }
    }
}
