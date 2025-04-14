using Ae.Sample.Identity.Authentication;
using System.Security.Claims;

namespace Ae.Sample.Identity.Services
{
    /// <summary>
    /// Provides authentication services for application identity management.
    /// </summary>
    public interface IAppIdentityService
    {
        /// <summary>
        /// Attempts to verify user credentials and returns the associated claims principal if successful.
        /// </summary>
        /// <param name="email">The username to verify.</param>
        /// <param name="password">The password to verify.</param>
        /// <param name="ct">Optional cancellation token to cancel the operation.</param>
        /// <returns>
        /// A VerifyCredentialResult containing:
        /// - isVerified: Boolean indicating if the credentials were valid
        /// - infoMessage: A message providing additional information about the verification result
        /// - principal: The ClaimsPrincipal if verification was successful, null otherwise
        /// </returns>
        Task<VerifyCredentialResult> TryVerifyCredentialAsync(string email, string password, CancellationToken ct = default);
    }
}
