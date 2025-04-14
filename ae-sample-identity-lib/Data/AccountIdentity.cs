namespace Ae.Sample.Identity.Data
{
    /// <summary>
    /// Represents a user account identity with authentication and profile information.
    /// </summary>
    public sealed class AccountIdentity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the account.
        /// </summary>
        public Guid Id { get; set; } = Guid.Empty;

        /// <summary>
        /// Gets or sets the email address associated with the account.
        /// </summary>
        public string EmailAddress { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the hashed password for the account.
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the timestamp when the account was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.MinValue;

        /// <summary>
        /// Gets or sets a value indicating whether the account is locked.
        /// </summary>
        public bool IsLocked { get; set; } = false;

        /// <summary>
        /// Gets or sets the employment date of the account holder.
        /// </summary>
        public DateTimeOffset EmploymentDate { get; set; } = DateTimeOffset.MinValue;

        /// <summary>
        /// Gets or sets the date when the employment is expected to expire.
        /// </summary>
        public DateTimeOffset EmploymentExpiredDate { get; set; } = DateTimeOffset.MinValue;

        /// <summary>
        /// Gets or sets the timestamp of the last login.
        /// </summary>
        public DateTimeOffset LastLogin { get; set; } = DateTimeOffset.MinValue;

        /// <summary>
        /// Gets or sets the timestamp of the last password change.
        /// </summary>
        public DateTimeOffset LastPasswordChange { get; set; } = DateTimeOffset.MinValue;

        /// <summary>
        /// Gets or sets the timestamp when the current password will expire.
        /// </summary>
        public DateTimeOffset PasswordExpiredOn { get; set; } = DateTimeOffset.MaxValue;

        /// <summary>
        /// Gets or sets the timestamp when the email was verified.
        /// </summary>
        public DateTimeOffset EmailVerifiedOn { get; set; } = DateTimeOffset.MinValue;

        /// <summary>
        /// Gets or sets the display name for the account.
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description for the account.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Formats the employment date as a string in "yyyy-MM-dd" format.
        /// </summary>
        /// <returns>The formatted employment date string.</returns>
        public string ToStringEmploymentDate() => EmploymentDate.ToString("yyyy-MM-dd");
    }
}
