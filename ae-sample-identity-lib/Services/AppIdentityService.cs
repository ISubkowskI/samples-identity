using System.Security.Claims;
using Ae.Sample.Identity.Authorization;
using Ae.Sample.Identity.Data;
using Microsoft.Extensions.Logging;

namespace Ae.Sample.Identity.Services
{
    /// <summary>
    /// Provides identity and authentication services for the application.
    /// </summary>
    public sealed class AppIdentityService : IAppIdentityService
    {
        private readonly ILogger<AppIdentityService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppIdentityService"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for logging service operations.</param>
        public AppIdentityService(ILogger<AppIdentityService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Attempts to verify user credentials and creates a ClaimsPrincipal if successful.
        /// </summary>
        /// <param name="userName">The username to verify (typically an email address).</param>
        /// <param name="password">The password to verify.</param>
        /// <param name="ct">Optional cancellation token to cancel the operation.</param>
        /// <returns>
        /// A tuple containing:
        /// - bool: Indicates whether the credentials were verified successfully
        /// - ClaimsPrincipal?: The claims principal containing user claims if verification was successful, null otherwise
        /// </returns>
        /// <remarks>
        /// Currently implements a basic hardcoded verification for demo purposes.
        /// Successfully authenticates with:
        /// - Username: info@softaren.com
        /// - Password: Demo
        /// </remarks>
        public async Task<(bool isVerified, ClaimsPrincipal? principal)> TryVerifyCredentialAsync(string userName, string password, CancellationToken ct = default)
        {
            _logger.LogDebug("Start {ServiceName} {MethodName}() ...", nameof(AppIdentityService), nameof(TryVerifyCredentialAsync));

            try
            {
                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                {
                    _logger.LogWarning("Incorrect arguments username or password. '{UserName}'. {ServiceName} {MethodName}()", userName, nameof(AppIdentityService), nameof(TryVerifyCredentialAsync));
                    return (false, default);
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
                            new (AppClaimTypes.EmploymentDate, "2024-03-01"),
                        };
                    var identity = new ClaimsIdentity(claims, ConstsWebApp.CookieName);

                    _logger.LogInformation("Verified '{UserName}'.", userName);
                    var principal = new ClaimsPrincipal(identity);
                    return await Task.FromResult((true, principal));
                }

                _logger.LogWarning("NOT VERIFIED '{UserName}'.", userName);
                return (false, default);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "{ServiceName} {MethodName}() ", nameof(AppIdentityService), nameof(TryVerifyCredentialAsync));
                return (false, default);
            }
        }
    }
}
