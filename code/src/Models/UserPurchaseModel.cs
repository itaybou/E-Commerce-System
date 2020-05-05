using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models
{
    public class UserPurchaseModel
    {
        private DateTime _purchaseDate;
        private double _totalPrice;
        private ICollection<ProductModel> _productsPurchased;
        private string _firstName;
        private string _lastName;
        private int _id;
        private string _creditCardNumber;
        private DateTime _expirationCreditCard;
        private int _CVV;
        private string _address;

        public UserPurchaseModel(DateTime purchaseDate, double totalPrice, List<ProductModel> productsPurchased, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int cVV, string address)
        {
            _purchaseDate = purchaseDate;
            _totalPrice = totalPrice;
            _productsPurchased = productsPurchased;
            _firstName = firstName;
            _lastName = lastName;
            _id = id;
            _creditCardNumber = creditCardNumber;
            _expirationCreditCard = expirationCreditCard;
            _CVV = cVV;
            _address = address;
        }

        public DateTime PurchaseDate { get => _purchaseDate; set => _purchaseDate = value; }
        public double TotalPrice { get => _totalPrice; set => _totalPrice = value; }
        public ICollection<ProductModel> ProductsPurchased { get => _productsPurchased; set => _productsPurchased = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public int Id { get => _id; set => _id = value; }
        public string CreditCardNumber { get => _creditCardNumber; set => _creditCardNumber = value; }
        public DateTime ExpirationCreditCard { get => _expirationCreditCard; set => _expirationCreditCard = value; }
        public int CVV { get => _CVV; set => _CVV = value; }
        public string Address { get => _address; set => _address = value; }
    }
}