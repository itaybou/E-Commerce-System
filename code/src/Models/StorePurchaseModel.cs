using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models
{
    public class StorePurchaseModel
    {
        private string _username;
        private double _totalPrice;
        private ICollection<ProductModel> _productsPurchased;

        public StorePurchaseModel(string username, double totalPrice, List<ProductModel> productsPurchased)
        {
            _username = username;
            _totalPrice = totalPrice;
            _productsPurchased = productsPurchased;
        }

        public string Username { get => _username; set => _username = value; }
        public double TotalPrice { get => _totalPrice; set => _totalPrice = value; }
        public ICollection<ProductModel> ProductsPurchased { get => _productsPurchased; set => _productsPurchased = value; }
    }
}