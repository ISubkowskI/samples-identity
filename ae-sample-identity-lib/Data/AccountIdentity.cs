namespace Ae.Sample.Identity.Data
{
    public sealed class AccountIdentity
    {
        public string Name { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
