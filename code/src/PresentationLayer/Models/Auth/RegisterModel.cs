using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models.Auth
{
    public class RegisterModel
    {
        [Required]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DisplayName("Password Validation")]
        [Compare("Password", ErrorMessage = "Passwords entered does not match.")]
        public string PasswordValidation { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }
    }
}
