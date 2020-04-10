using ECommerceSystem.DomainLayer.StoresManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    class StoreShoppingCart
    {
        private Store _store { get; set; }
        private Dictionary<Product, int> _productQuantities;
        private float _totalPrice;

        public Store store { get => _store; set => _store = value; }
        public Dictionary<Product, int> Products { get => _productQuantities;}

        public StoreShoppingCart (Store s, Dictionary<Product, int> products)
        {
            _store = s;
            _productQuantities = products;
            _totalPrice = 0;
        }

        public void AddToCart(Product p, int quantity)
        {
            _productQuantities.Add(p, quantity);
            _totalPrice += p.Price * quantity;
        }

        public void ChangeProductQuantity(Product p, int quantity)
        {
            _totalPrice -= p.Price * _productQuantities[p];
            _productQuantities[p] = quantity;
            _totalPrice += p.Price * quantity;
        }

        public void RemoveFromCart(Product p)
        {
            _totalPrice -= p.Price * _productQuantities[p];
            _productQuantities.Remove(p);
        }

    }
}