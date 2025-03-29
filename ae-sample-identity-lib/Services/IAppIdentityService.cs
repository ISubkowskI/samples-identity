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
        /// <param name="userName">The username to verify.</param>
        /// <param name="password">The password to verify.</param>
        /// <param name="ct">Optional cancellation token to cancel the operation.</param>
        /// <returns>
        /// A tuple containing:
        /// - isVerified: Boolean indicating if the credentials were valid
        /// - principal: The ClaimsPrincipal if verification was successful, null otherwise
        /// </returns>
        Task<(bool isVerified, ClaimsPrincipal? principal)> TryVerifyCredentialAsync(string userName, string password, CancellationToken ct = default);
    }
}
