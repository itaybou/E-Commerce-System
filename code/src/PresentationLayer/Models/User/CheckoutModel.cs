using ECommerceSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Models.User
{
    public class CheckoutModel
    {
        private IEnumerable<(ProductModel, int)> _products;
        
        [Required(ErrorMessage = "Missing First name of card holder", AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Missing Last name of card holder", AllowEmptyStrings = false)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Missing ID of card holder", AllowEmptyStrings = false)]
        public int ID { get; set; }
        [Required(ErrorMessage = "Missing Credit Card number", AllowEmptyStrings = false)]
        public string CreditCardNumber { get; set; }
        [Required(ErrorMessage = "Missing Credit Card expiration year", AllowEmptyStrings = false)]
        public int CreditCardExpirationYear { get; set; }
        [Required(ErrorMessage = "Missing Credit Card expiration month", AllowEmptyStrings = false)]
        public int CreditCardExpirationMonth { get; set; }
        [Required(ErrorMessage = "Missing Credit Card CVC", AllowEmptyStrings = false)]
        public int CVV { get; set; }
        [Required(ErrorMessage = "Missing shipping address", AllowEmptyStrings = false)]
        public string Address { get; set; }
        [Required(ErrorMessage = "Missing shipping city", AllowEmptyStrings = false)]
        public string City { get; set; }
        [Required(ErrorMessage = "Missing shipping postal code", AllowEmptyStrings = false)]
        public int PostCode { get; set; }
        public IEnumerable<(ProductModel, int)> Products 
        { get => _products;
            set {
                _products = value;
                Total = _products.Aggregate(0.0, (sum, prod) => sum += prod.Item1.PriceWithDiscount * prod.Item2);
            }
        }
        public double Total { get; set; }

        public CheckoutModel()
        {
        }

        public CheckoutModel(CartModel cartModel)
        {
            Products = cartModel.UserCart.Cart.Select(s => s.Value).SelectMany(p => p);
        }
    }
}
