using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using Ae.Sample.Identity.Data;

namespace Ae.Sample.Identity.Services
{
    /// <summary>
    /// Service that manages user account identities and authentication.
    /// Implements in-memory storage using ConcurrentDictionary for thread-safe operations.
    /// </summary>
    public sealed class AccountsService : IAccountsService
    {
        private readonly ILogger<AccountsService> _logger;

        /// <summary>
        /// Thread-safe dictionary storing user accounts with email address as key
        /// </summary>
        private readonly ConcurrentDictionary<string, AccountIdentity> _accountsStorage;

        /// <summary>
        /// Initializes a new instance of the AccountsService
        /// </summary>
        /// <param name="logger">Logger instance for diagnostic information</param>
        public AccountsService(ILogger<AccountsService> logger)
        {
            _logger = logger;

            // Create and initialize demo account with hashed password
            AccountIdentity account = new()
            {
                Name = "info@softaren.com",
                EmailAddress = "info@softaren.com",
                Description = "Demo User",
            };
            account.PasswordHash = new PasswordHasher<AccountIdentity>().HashPassword(account, "Demo");

            // Initialize thread-safe storage and add demo account
            _accountsStorage = new ConcurrentDictionary<string, AccountIdentity>();
            _accountsStorage.TryAdd(account.EmailAddress, account);
        }

        /// <summary>
        /// Attempts to retrieve an account identity by username
        /// </summary>
        /// <param name="userName">The username (email) to look up</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>
        /// A tuple containing:
        /// - success: true if account was found, false otherwise
        /// - accountIdentity: the found account or null if not found
        /// </returns>
        public async Task<(bool success, AccountIdentity? accountIdentity)> TryGetAccountIdentityAsync(string userName, CancellationToken ct = default)
        {
            if (!_accountsStorage.TryGetValue(userName, out var accountIdentity))
            {
                _logger.LogWarning("User not found '{UserName}'.", userName);
                return (false, default);
            }
            return await Task.FromResult((true, accountIdentity));
        }
    }
}
