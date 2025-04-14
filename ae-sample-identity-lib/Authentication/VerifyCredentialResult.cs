using System.Security.Claims;

namespace Ae.Sample.Identity.Authentication
{
    /// <summary>
    /// Represents the result of a credential verification operation.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="VerifyCredentialResult"/> class.
    /// </remarks>
    /// <param name="isVerified">Indicates whether the credentials were successfully verified.</param>
    /// <param name="infoMessage">An informational message about the verification result.</param>
    /// <param name="principal">The ClaimsPrincipal associated with the verified credentials.</param>
    public sealed class VerifyCredentialResult(
        bool isVerified,
        string infoMessage,
        ClaimsPrincipal? principal)
    {
        /// <summary>
        /// Gets a value indicating whether the credentials were successfully verified.
        /// </summary>
        public bool IsVerified { get; init; } = isVerified;

        /// <summary>
        /// Gets an informational message about the verification result.
        /// </summary>
        public string InfoMessage { get; init; } = infoMessage;

        /// <summary>
        /// Gets the ClaimsPrincipal associated with the verified credentials, if any.
        /// </summary>
        public ClaimsPrincipal? Principal { get; init; } = principal;
    }
}
