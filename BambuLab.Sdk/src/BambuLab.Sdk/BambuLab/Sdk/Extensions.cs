using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BambuLab.Sdk;

public static class Extensions
{
    public static T GetEnumValueOrDefault<T>(this string value, T defaultValue) where T : struct, Enum
    {
        if (Enum.TryParse<T>(value, true, out var result))
        {
            return result;
        }

        return defaultValue;
    }

    public static T GetAttribute<T>(this Enum enumValue) where T : Attribute
    {
        return enumValue.GetType()
                        .GetMember(enumValue.ToString())
                        .First()
                        .GetCustomAttribute<T>();
    }

    public static IEnumerable<T> GetEnumValues<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }

    public static string GetDescription(this NozzleTypeEnum nozzleType)
    {
        switch (nozzleType)
        {
            case NozzleTypeEnum.StainlessSteel:
                return "stainless_steel";
            case NozzleTypeEnum.HardenedSteel:
                return "hardened_steel";
            default:
                return string.Empty;
        }
    }
}
