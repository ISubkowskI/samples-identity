using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ae.Sample.Identity.Data;

namespace Ae.Sample.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(ILogger<LogoutModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (HttpContext.User?.Identity?.IsAuthenticated == true)
            {
                _logger.LogInformation("<-- User {UserName} is logging out. {Page} {MethodName}()",
                    HttpContext.User.Identity.Name, nameof(LogoutModel), nameof(OnPostAsync));

                await HttpContext.SignOutAsync(ConstsWebApp.CookieName);
            }
            else
            {
                _logger.LogInformation("No authenticated user to log out. {Page} {MethodName}()",
                    nameof(LogoutModel), nameof(OnPostAsync));
            }

            return RedirectToPage("/Index");
        }
    }
}
