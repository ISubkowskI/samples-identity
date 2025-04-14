using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Sample.Identity.Authentication
{
    /// <summary>
    /// Represents authentication credentials used for user registration
    /// </summary>
    public sealed class CredentialRegistration
    {
        /// <summary>
        /// Gets or sets the user's email address which serves as their username
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's password for authentication
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's display name shown in the application
        /// </summary>
        [Display(Name = "Display name")]
        [DataType(DataType.Text)]
        public string DisplayName { get; set; } = string.Empty;
    }
}
