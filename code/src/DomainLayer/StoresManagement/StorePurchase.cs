using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.UserManagement;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public class StorePurchase
    {
        private User _user;
        private double _totalPrice;
        private List<Product> _productsPurchased;

        public StorePurchase(User user, double totalPrice, List<Product> productsPurchased)
        {
            _user = user;
            _totalPrice = totalPrice;
            _productsPurchased = productsPurchased;
        }

        public User User { get => _user; }
        public double TotalPrice { get => _totalPrice; }
        public List<Product> ProductsPurchased { get => _productsPurchased; }
    }
}
