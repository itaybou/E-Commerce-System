using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class AddProductModel
    {
        public Guid Id { get ; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get ; set; }
        public int Quantity { get; set; }
        public double BasePrice { get; set; }
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