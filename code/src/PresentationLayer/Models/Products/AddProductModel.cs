using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class AddProductModel
    {
        public Guid Id { get ; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        public string Name { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        public string Category { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        public string Description { get ; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        public int Quantity { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        public double BasePrice { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        public double PriceWithDiscount { get; set; }

        public AddProductModel(Guid id, string name, string category, string description, int quantity, double basePrice, double priceWithDiscount)
        {
            Id = id;
            Name = name;
            Description = description;
            Quantity = quantity;
            BasePrice = basePrice;
            PriceWithDiscount = priceWithDiscount;
        }
    }
}