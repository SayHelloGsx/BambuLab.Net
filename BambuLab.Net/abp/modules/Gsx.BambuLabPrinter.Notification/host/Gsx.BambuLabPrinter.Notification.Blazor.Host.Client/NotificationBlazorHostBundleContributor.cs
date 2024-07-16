using Volo.Abp.Bundling;

namespace Gsx.BambuLabPrinter.Notification.Blazor.Host.Client;

public class NotificationBlazorHostBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}
