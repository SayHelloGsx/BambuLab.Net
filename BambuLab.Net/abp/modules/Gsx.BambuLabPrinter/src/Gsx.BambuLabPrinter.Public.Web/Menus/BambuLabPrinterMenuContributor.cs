using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Gsx.BambuLabPrinter.Web.Menus;

public class BambuLabPrinterMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        context.Menu.AddItem(new ApplicationMenuItem(BambuLabPrinterMenus.Prefix, displayName: "BambuLabPrinter", "~/BambuLabPrinter", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}
