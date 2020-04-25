using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models.Auth
{
    public class LoginModel
    {
        [Required]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}
