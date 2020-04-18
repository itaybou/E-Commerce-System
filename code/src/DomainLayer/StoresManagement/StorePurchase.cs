using ECommerceSystem.DomainLayer.UserManagement;
using System.Collections.Generic;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public class StorePurchase
    {
        private User _user;
        private double _totalPrice;
        private List <Product> _productsPurchased;

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