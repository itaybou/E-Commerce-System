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
        private Dictionary<Product, int> _productQuantities; // Product => Quantity

        public Store store { get => _store; set => _store = value; }
        public Dictionary<Product, int> Products { get => _productQuantities;}

        public StoreShoppingCart (Store s, Dictionary<Product, int> products)
        {
            _store = s;
            _productQuantities = products;
        }

        public void AddToCart(Product p, int quantity)
        {
            _productQuantities.Add(p, quantity);
        }

        public void ChangeProductQuantity(Product p, int quantity)
        {
            _productQuantities[p] = quantity;
        }

        public void RemoveFromCart(Product p)
        {
            _productQuantities.Remove(p);
        }

        public double getTotalCartPrice()
        {
            return _productQuantities.Aggregate(0.0, (total, product) => total + (product.Key.CalculateDiscount() * product.Value));
        }
    }
}