using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class AddProductDiscountModel
    {
        [DisplayName("Minimum Purchase Price For Discount")]
        [Required(ErrorMessage = "Please minimum price to recieve discount", AllowEmptyStrings = false)]
        public double RequiredQuantity { get; set; }

        [DisplayName("Percentage")]
        [Required(ErrorMessage = "Please Provide discount percentage", AllowEmptyStrings = false)]
        public float Percentage { get; set; }

        [DisplayName("Discount Expiration Date"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Please Provide discount expiration date", AllowEmptyStrings = false)]
        public DateTime ExpDate { get; set; }
    }
}