using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class OpenStoreModel
    {
        [Required(ErrorMessage = "Please Provide Store Name", AllowEmptyStrings = false)]
        public string StoreName { get; set; }
    }
}