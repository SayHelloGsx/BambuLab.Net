using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Gsx.BambuLabPrinter.Pages;

public class IndexModel : BambuLabPrinterPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
