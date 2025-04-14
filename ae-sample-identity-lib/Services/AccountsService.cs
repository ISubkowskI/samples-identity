using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using Ae.Sample.Identity.Data;
using System.Security.Claims;
using Ae.Sample.Identity.Authentication;

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

            // Initialize thread-safe storage and add demo account
            _accountsStorage = new ConcurrentDictionary<string, AccountIdentity>();
            InitializeStorage(_accountsStorage);
        }

        private static void InitializeStorage(ConcurrentDictionary<string, AccountIdentity> storage)
        {
            // Create and initialize demo account with hashed password
            AccountIdentity accountInfo = new()
            {
                Id = Guid.NewGuid(),
                EmailAddress = "info@softaren.com",
                CreatedAt = DateTimeOffset.Now,
                EmploymentDate = DateTimeOffset.Parse("2024-03-01"),
                DisplayName = "Info",
                Description = "Demo User Info",
            };
            accountInfo.PasswordHash = new PasswordHasher<AccountIdentity>().HashPassword(accountInfo, "Demo");
            storage.TryAdd(accountInfo.EmailAddress, accountInfo);

            AccountIdentity accountNotifications = new()
            {
                Id = Guid.NewGuid(),
                EmailAddress = "notifications@softaren.com",
                CreatedAt = DateTimeOffset.Now,
                EmploymentDate = DateTimeOffset.Now,
                DisplayName = "Notifications",
                Description = "Demo User Notifications",
            };
            accountNotifications.PasswordHash = new PasswordHasher<AccountIdentity>().HashPassword(accountNotifications, "Demo");
            storage.TryAdd(accountNotifications.EmailAddress, accountNotifications);
        }

        /// <summary>
        /// Attempts to retrieve an account identity by username
        /// </summary>
        /// <param name="email">The username (email) to look up</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>
        /// A tuple containing:
        /// - success: true if account was found, false otherwise
        /// - accountIdentity: the found account or null if not found
        /// </returns>
        public async Task<(bool success, AccountIdentity? accountIdentity)> TryGetAccountIdentityByEmailAsync(string email, CancellationToken ct = default)
        {
            if (!_accountsStorage.TryGetValue(email, out var accountIdentity))
            {
                _logger.LogWarning("User not found '{UserName}'.", email);
                return (false, default);
            }
            return await Task.FromResult((true, accountIdentity));
        }

        /// <summary>
        /// Retrieves the claims associated with a specific account by email.
        /// </summary>
        /// <param name="email">The email address of the account.</param>
        /// <param name="ct">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a collection of claims
        /// associated with the account, or an empty collection if the account is not found.
        /// </returns>
        public async Task<IEnumerable<Claim>> GetAccountClaimsByEmailAsync(string email, CancellationToken ct = default)
        {

            if (!_accountsStorage.TryGetValue(email, out var accountIdentity))

            {

                _logger.LogWarning("User not found '{UserName}'. {MethodName}", email, nameof(GetAccountClaimsByEmailAsync));

                return [];

            }



            // Create claims based on account identity

            switch (accountIdentity.EmailAddress)

            {

                case "info@softaren.com":

                    { // Info account

                        var claims = new List<Claim>
                                {
                                    new (ClaimTypes.Name, string.IsNullOrWhiteSpace(accountIdentity.DisplayName) ? accountIdentity.EmailAddress : accountIdentity.DisplayName),
                                    new (ClaimTypes.NameIdentifier, $"{accountIdentity.Id}"),
                                    new (ClaimTypes.Email, accountIdentity.EmailAddress),
                                    new (ClaimTypes.Role, "Demo"),
                                    new (AppClaimTypes.Department, "HR"),
                                    new (AppClaimTypes.Admin, "true"),
                                    new (AppClaimTypes.Manager, "true"),
                                    new (AppClaimTypes.EmploymentDate, accountIdentity.ToStringEmploymentDate()),
                                };

                        return await Task.FromResult(claims);

                    }



                case "notifications@softaren.com":

                    { // Notifications account

                        var claims = new List<Claim>
                                {
                                    new (ClaimTypes.Name, string.IsNullOrWhiteSpace(accountIdentity.DisplayName) ? accountIdentity.EmailAddress : accountIdentity.DisplayName),
                                    new (ClaimTypes.NameIdentifier, $"{accountIdentity.Id}"),
                                    new (ClaimTypes.Email, accountIdentity.EmailAddress),
                                    new (ClaimTypes.Role, "Demo"),
                                    new (AppClaimTypes.Department, "HR"),
                                    //new (AppClaimTypes.Admin, "false"),
                                    new (AppClaimTypes.Manager, "true"),
                                    new (AppClaimTypes.EmploymentDate, accountIdentity.ToStringEmploymentDate()),
                                };

                        return await Task.FromResult(claims);

                    }



                default:

                    return [];

            }



        }
    }
}
