﻿using Gsx.BambuLabPrinter.Notification.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Gsx.BambuLabPrinter.Notification.Permissions;

public class NotificationPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(NotificationPermissions.GroupName, L("Permission:Notification"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BambuLabPrinterNotificationResource>(name);
    }
}
