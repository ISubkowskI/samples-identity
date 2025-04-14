using Ae.Sample.Identity.Data;
using System.Security.Claims;

namespace Ae.Sample.Identity.Services
{
    public interface IAccountsService
    {
        Task<(bool success, AccountIdentity? accountIdentity)> TryGetAccountIdentityByEmailAsync(string email, CancellationToken ct = default);

        Task<IEnumerable<Claim>> GetAccountClaimsByEmailAsync(string email, CancellationToken ct = default);
    }
}
