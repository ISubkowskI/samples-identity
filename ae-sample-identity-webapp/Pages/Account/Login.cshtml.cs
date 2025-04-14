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

            var vcResult = await _appIdentityService.TryVerifyCredentialAsync(Credential.Email, Credential.Password).ConfigureAwait(false);
            if (vcResult.IsVerified)
            {
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = Credential.RememberMe,
                };

                _logger.LogInformation("<-- User {Email} is logging in. {Page} {MethodName}().",
                    Credential.Email, nameof(LoginModel), nameof(OnPost));

                await HttpContext.SignInAsync(ConstsWebApp.CookieName, vcResult.Principal!, authProperties);
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
