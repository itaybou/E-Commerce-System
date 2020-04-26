using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models.Auth
{
    public class RegisterModel
    {
        [DisplayName("Username")]
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        public string Username { get; set; }

        [DisplayName("Password")]
        
        public string Password { get; set; }

        [DisplayName("Password Validation")]
        [Required(ErrorMessage = "Please Provide Password Validation", AllowEmptyStrings = false)]
        [Compare("Password", ErrorMessage = "Passwords entered does not match.")]
        public string PasswordValidation { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please Provide First Name", AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please Provide Last Name", AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Please Provide Email", AllowEmptyStrings = false)]
        public string Email { get; set; }
    }
}
