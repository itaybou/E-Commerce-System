using ECommerceSystem.DomainLayer.UserManagement;
using System;
using System.Collections.Generic;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public class StorePurchase
    {
        private User _user;
        private double _totalPrice;
        private List<Product> _productsPurchased;
        public DateTime PurchaseDate { get; set; }

        public StorePurchase(User user, double totalPrice, List<Product> productsPurchased, DateTime purchaseDate)
        {
            _user = user;
            _totalPrice = totalPrice;
            _productsPurchased = productsPurchased;
            PurchaseDate = purchaseDate;
        }

        public User User { get => _user; }
        public double TotalPrice { get => _totalPrice; }
        public List<Product> ProductsPurchased { get => _productsPurchased; }
    }
}