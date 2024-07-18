using Volo.Abp.Reflection;

namespace Gsx.BambuLabPrinter.Public.Permissions;

public class BambuLabPrinterPublicPermissions
{
    public const string GroupName = "BambuLabPrinter";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(BambuLabPrinterPublicPermissions));
    }
}
