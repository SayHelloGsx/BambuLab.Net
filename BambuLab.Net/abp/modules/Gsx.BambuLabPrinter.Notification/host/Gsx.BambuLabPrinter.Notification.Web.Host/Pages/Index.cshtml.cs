using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Gsx.BambuLabPrinter.Notification.Pages;

public class IndexModel : NotificationPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
