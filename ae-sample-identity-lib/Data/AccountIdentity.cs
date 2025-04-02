namespace Ae.Sample.Identity.Data
{
    public sealed class AccountIdentity
    {
        public string EmailAddress { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public Guid Guid { get; set; } = Guid.Empty;

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.MinValue;

        public DateTimeOffset EmploymentDate { get; set; } = DateTimeOffset.MinValue;

        public string DisplayName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;


        public string ToStringEmploymentDate() => EmploymentDate.ToString("yyyy-MM-dd");
    }
}
