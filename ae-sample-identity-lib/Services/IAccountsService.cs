using Ae.Sample.Identity.Data;

namespace Ae.Sample.Identity.Services
{
    public interface IAccountsService
    {
        Task<(bool success, AccountIdentity? accountIdentity)> TryGetAccountIdentityAsync(string userName, CancellationToken ct = default);
    }
}
