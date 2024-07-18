using Gsx.BambuLabPrinter.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Gsx.BambuLabPrinter.Public.Permissions;

public class BambuLabPrinterPublicPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BambuLabPrinterPublicPermissions.GroupName, L("Permission:BambuLabPrinter"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BambuLabPrinterResource>(name);
    }
}
