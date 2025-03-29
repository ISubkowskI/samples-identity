using System.Security.Claims;
using Ae.Sample.Identity.Data;
using Microsoft.Extensions.Logging;

namespace Ae.Sample.Identity.Services
{
    public sealed class AppIdentityService : IAppIdentityService
    {
        private readonly ILogger<AppIdentityService> _logger;

        public AppIdentityService(ILogger<AppIdentityService> logger)
        {
            _logger = logger;
        }

        public Task<bool> TryVerifyCredentialAsync(string userName, string password, out ClaimsPrincipal? principal, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                principal = default;
                return Task.FromResult(false);
            }

            // Verify the credential
            if (userName == "info@softaren.com" && password == "Demo")
            {
                // Creating the security context
                var claims = new List<Claim>
                {
                    new (ClaimTypes.Name, userName),
                    new (ClaimTypes.Email, password),
                    new (ClaimTypes.Role, "Demo"),
                    new ("Department", "HR"),
                    new ("Admin", "true"),
                    new ("Manager", "true"),
                    new ("EmploymentDate", "2024-03-01"),
                };
                var identity = new ClaimsIdentity(claims, ConstsWebApp.CookieName);
                principal = new ClaimsPrincipal(identity);
                return Task.FromResult(true);
            }

            principal = default;
            return Task.FromResult(false);
        }
    }
}
