using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ae.Sample.Identity.Authorization;

namespace Ae.Sample.Identity.Pages
{
    [Authorize(AppPolicyNames.AdminOnly)]
    public class SettingsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
