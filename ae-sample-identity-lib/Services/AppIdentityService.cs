using System.Security.Claims;
using Ae.Sample.Identity.Authentication;
using Ae.Sample.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Ae.Sample.Identity.Services
{
    /// <summary>
    /// Provides identity and authentication services for the application.
    /// </summary>
    public sealed class AppIdentityService : IAppIdentityService
    {
        private readonly ILogger<AppIdentityService> _logger;

        private readonly IAccountsService _accounts;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppIdentityService"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for logging service operations.</param>
        public AppIdentityService(ILogger<AppIdentityService> logger, IAccountsService accounts)
        {
            _logger = logger;
            _accounts = accounts;
        }

        /// <summary>
        /// Attempts to verify user credentials and creates a ClaimsPrincipal if successful.
        /// </summary>
        /// <param name="email">The username to verify (typically an email address).</param>
        /// <param name="password">The password to verify.</param>
        /// <param name="ct">Optional cancellation token to cancel the operation.</param>
        /// <returns>
        /// A VerifyCredentialResult containing:
        /// - bool: Indicates whether the credentials were verified successfully
        /// - infoMessage: A message providing additional information about the verification result
        /// - ClaimsPrincipal?: The claims principal containing user claims if verification was successful, null otherwise
        /// </returns>
        /// <remarks>
        /// Currently implements a basic hardcoded verification for demo purposes.
        /// Successfully authenticates with:
        /// - Username: info@softaren.com
        /// - Password: Demo
        /// </remarks>
        public async Task<VerifyCredentialResult> TryVerifyCredentialAsync(string email, string password, CancellationToken ct = default)
        {
            _logger.LogDebug("Start {ServiceName} {MethodName}() ...", nameof(AppIdentityService), nameof(TryVerifyCredentialAsync));

            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    _logger.LogWarning("Incorrect arguments username or password. '{Email}'. {ServiceName} {MethodName}()", email, nameof(AppIdentityService), nameof(TryVerifyCredentialAsync));
                    return new(isVerified: false, infoMessage: "Incorrect arguments username or password.", principal: default);
                }

                // Verify the credential
                (bool success, AccountIdentity? accountIdentity) = await _accounts.TryGetAccountIdentityByEmailAsync(email, ct);
                if (!success)
                {
                    _logger.LogWarning("User not found '{Email}'.", email);
                    return new(isVerified: false, infoMessage: $"User not found '{email}'.", principal: default);
                }

                // Check if the account is locked
                if (accountIdentity!.IsLocked)
                {
                    _logger.LogWarning("Account is locked '{Email}'.", email);
                    return new(isVerified: false, infoMessage: $"Account is locked '{email}'.", principal: default);
                }

                if (new PasswordHasher<AccountIdentity>().VerifyHashedPassword(accountIdentity!, accountIdentity!.PasswordHash, password) == PasswordVerificationResult.Failed)
                {
                    _logger.LogWarning("PASSWORD NOT VERIFIED '{Email}'.", email);
                    return new(isVerified: false, infoMessage: $"The password is incorrect '{email}'.", principal: default);
                }

                // Creating the security context
                List<Claim> claims = [.. (await _accounts.GetAccountClaimsByEmailAsync(email, ct))];

                var identity = new ClaimsIdentity(claims, ConstsWebApp.CookieName);

                _logger.LogInformation("Verified '{Email}'.", email);
                var claimsPrincipal = new ClaimsPrincipal(identity);
                return new(isVerified: true, infoMessage: "Ok.", principal: claimsPrincipal);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "{ServiceName} {MethodName}() ", nameof(AppIdentityService), nameof(TryVerifyCredentialAsync));
                return new(isVerified: false, infoMessage: $"Error '{exc.Message}'.", principal: default);
            }
        }
    }
}
