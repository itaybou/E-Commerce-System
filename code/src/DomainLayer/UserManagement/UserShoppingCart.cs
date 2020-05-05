using ECommerceSystem.DomainLayer.StoresManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public class UserShoppingCart : IEnumerable<Product>
    {
        private List<StoreShoppingCart> _storeCarts;

        public List<StoreShoppingCart> StoreCarts { get => _storeCarts; set => _storeCarts = value; }

        public UserShoppingCart()
        {
            _storeCarts = new List<StoreShoppingCart>();
        }

        public double getTotalACartPrice()
        {
            return _storeCarts.Aggregate(0.0, (total, cart) => total + cart.getTotalCartPrice());
        }

        public ICollection<(Store, double, IDictionary<Product, int>)> getProductsStoreAndTotalPrices()
        {
            var result = new List<(Store, double, IDictionary<Product, int>)>();
            foreach(var storeCart in StoreCarts)
            {
                result.Add((storeCart.store, storeCart.getTotalCartPrice(), storeCart.Products));
            }
            return result;
        }

        public IDictionary<Product, int> getAllCartProductsAndQunatities()
        {
            return StoreCarts.SelectMany(storeCart => storeCart.Products).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public IEnumerator<Product> GetEnumerator()
        {
            foreach (var storeCart in _storeCarts)
            {
                foreach (var product in storeCart.Products.Keys.ToList())
                {
                    yield return product;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}