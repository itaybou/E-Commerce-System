using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Models.Products
{
    public class AddConcreteProductModel
    {
        [DisplayName("Username")]
        public string Name { get; set; }

        [DisplayName("Quantity")]
        [Required(ErrorMessage = "Please Provide Category", AllowEmptyStrings = false)]
        public int Quantity { get; set; }

        [DisplayName("Minimum Purchase Quantity")]
        public int MinQuantity { get; set; }

        [DisplayName("Maximum Purchase Quantity")]
        public int MaxQuantity { get; set; }

        [DisplayName("Minimum Purchase Price For Discount")]
        public double RequiredQuantity { get; set; }

        [DisplayName("Percentage")]
        public float PercentageVis { get; set; }

        [DisplayName("Percentage")]
        public float PercentageCond { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Discount Expiration Date"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ExpDateVis { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Discount Expiration Date"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ExpDateCond { get; set; }
    }
}
