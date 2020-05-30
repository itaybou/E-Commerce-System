using System;
using System.Collections.Generic;

namespace ECommerceSystem.Models
{
    public class StorePurchaseModel
    {
        private string _username;
        private double _totalPrice;
        private ICollection<ProductModel> _productsPurchased;
        public DateTime PurchaseDate { get; set; }

        public StorePurchaseModel(string username, double totalPrice, List<ProductModel> productsPurchased, DateTime purchaseDate)
        {
            _username = username;
            _totalPrice = totalPrice;
            _productsPurchased = productsPurchased;
            PurchaseDate = purchaseDate;
        }

        public string Username { get => _username; set => _username = value; }
        public double TotalPrice { get => _totalPrice; set => _totalPrice = value; }
        public ICollection<ProductModel> ProductsPurchased { get => _productsPurchased; set => _productsPurchased = value; }
    }
}