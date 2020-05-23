using ECommerceSystem.DomainLayer.StoresManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public class UserShoppingCart
    {
        public List<StoreShoppingCart> StoreCarts { get; set; }

        public UserShoppingCart(Guid userID)
        {
            StoreCarts = new List<StoreShoppingCart>();
        }

        public double getTotalACartPrice()
        {
            return StoreCarts.Aggregate(0.0, (total, cart) => total + cart.getTotalCartPrice());
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
    }
}