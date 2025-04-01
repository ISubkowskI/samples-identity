using System.ComponentModel.DataAnnotations;

namespace Ae.Sample.Identity.Authentication
{
    public class Credential
    {
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [Display(Name = "Display name")]
        [DataType(DataType.Text)]
        public string DisplayName { get; set; } = string.Empty;
    }
}
