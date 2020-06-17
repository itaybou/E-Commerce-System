using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PresentationLayer.Models.User
{
    public class CheckoutModel
    {
        public ShoppingCartModel Cart { get; set; } = null;

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

        [Required(ErrorMessage = "Missing shipping country", AllowEmptyStrings = false)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Missing shipping postal code", AllowEmptyStrings = false)]
        public int PostCode { get; set; }

        public double Total { get; set; } 

        public IEnumerable<(ProductModel, int)> Products { get; set; } = null;

        public CheckoutModel(string fname, string lname, int id, int expMonth, int expYear, int cvv, string address, string city, string country, int postcode)
        {
            FirstName = fname;
            LastName = lname;
            ID = id;
            CreditCardExpirationMonth = expMonth;
            CreditCardExpirationYear = expYear;
            CVV = cvv;
            Address = address;
            City = city;
            Country = country;
            PostCode = postcode;
            Cart = null;
            Products = null;
        }

        public CheckoutModel(ShoppingCartModel cart)
        {
            Cart = cart;
            Products = cart.Cart.Values.SelectMany(s => s);
            Total = Products.Select(p => p.Item1.BasePrice * p.Item2).Aggregate(0.0, (total, current) => total += current);

        }
    }
}