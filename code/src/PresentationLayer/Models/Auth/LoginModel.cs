using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models.Auth
{
    public class LoginModel
    {
        [DisplayName("Username")]
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        public string Username { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Please Provide Password", AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
