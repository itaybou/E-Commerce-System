using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class AddProductModel
    {
        [DisplayName("Name")]
        [Required(ErrorMessage = "Please Provide Name", AllowEmptyStrings = false)]
        public string Name { get; set; }

        [DisplayName("Category")]
        [Required(ErrorMessage = "Please Provide Category", AllowEmptyStrings = false)]
        public string Category { get; set; }

        [DisplayName("Descrption")]
        public string Description { get; set; }

        [DisplayName("Quantity")]
        [Required(ErrorMessage = "Please Provide Quantity", AllowEmptyStrings = false)]
        public int Quantity { get; set; }

        [DisplayName("Price")]
        [Required(ErrorMessage = "Please Provide Price", AllowEmptyStrings = false)]
        public double Price { get; set; }

        [DisplayName("Keywords")]
        public string Keywords { get; set; }

        [DisplayName("Image URL")]
        public string ImageURL { get; set; }

        [DisplayName("Minimum Purchase Quantity")]
        public int? MinQuantity { get; set; }

        [DisplayName("Maximum Purchase Quantity")]
        public int? MaxQuantity { get; set; }
    }
}