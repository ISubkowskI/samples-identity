using System.Security.Claims;

namespace Ae.Sample.Identity.Services
{
    public interface IAppIdentityService
    {
        Task<bool> TryVerifyCredentialAsync(string userName, string password, out ClaimsPrincipal? principal, CancellationToken ct = default);
    }
}
