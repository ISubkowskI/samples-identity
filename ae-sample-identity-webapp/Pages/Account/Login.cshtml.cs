using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ae.Sample.Identity.Authentication;
using Ae.Sample.Identity.Data;
using Ae.Sample.Identity.Services;

namespace Ae.Sample.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; } = new Credential();

        private readonly ILogger<LoginModel> _logger;

        private readonly IAppIdentityService _appIdentityService;

        public LoginModel(ILogger<LoginModel> logger, IAppIdentityService appIdentityService)
        {
            _logger = logger;
            _appIdentityService = appIdentityService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (await _appIdentityService.TryVerifyCredentialAsync(Credential.Username, Credential.Password, out var principal).ConfigureAwait(false))
            {
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = Credential.RememberMe,
                };

                await HttpContext.SignInAsync(ConstsWebApp.CookieName, principal!, authProperties);

                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
