using ECommerceSystem.DomainLayer.StoresManagement;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public class StoreShoppingCart
    {
        private Store _store { get; set; }
        private Dictionary<Product, int> _productQuantities; // Product => Quantity

        public Store store { get => _store; set => _store = value; }
        public Dictionary<Product, int> Products { get => _productQuantities; }

        public StoreShoppingCart(Store s)
        {
            _store = s;
            _productQuantities = new Dictionary<Product, int>();
        }

        public void AddToCart(Product p, int quantity)
        {
            if (_productQuantities.ContainsKey(p))
                _productQuantities[p] += quantity;
            else _productQuantities.Add(p, quantity);
        }

        public void ChangeProductQuantity(Product p, int quantity)
        {
            if (!_productQuantities.ContainsKey(p)) return;
            if (quantity == 0)
                _productQuantities.Remove(p);
            else _productQuantities[p] = quantity;
        }

        public void RemoveFromCart(Product p)
        {
            if (_productQuantities.ContainsKey(p))
                _productQuantities.Remove(p);
        }

        public double getTotalCartPrice()
        {
            return _productQuantities.Aggregate(0.0, (total, product) => total + (product.Key.CalculateDiscount() * product.Value));
        }
    }
}