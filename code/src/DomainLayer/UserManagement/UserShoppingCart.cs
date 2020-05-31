using ECommerceSystem.DomainLayer.StoresManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public class UserShoppingCart
    {
        public Guid UserID { get; set; }
        public List<StoreShoppingCart> StoreCarts { get; set; }

        public UserShoppingCart(Guid userID)
        {
            StoreCarts = new List<StoreShoppingCart>();
            UserID = userID;
        }

        public double getTotalACartPrice()
        {
            return StoreCarts.Aggregate(0.0, (total, cart) => total + cart.getTotalCartPrice());
        }

        public ICollection<(Store, double, IDictionary<Product, int>)> getProductsStoreAndTotalPrices()
        {
            var result = new List<(Store, double, IDictionary<Product, int>)>();
            foreach (var storeCart in StoreCarts)
            {
                var dict = storeCart.ProductQuantities.ToDictionary(k => k.Value.Item1, v => v.Value.Item2);
                result.Add((storeCart.Store, storeCart.getTotalCartPrice(), dict));
            }
            return result;
        }

        public IDictionary<Product, int> getAllCartProductsAndQunatities()
        {
            return StoreCarts.SelectMany(storeCart => storeCart.ProductQuantities).ToDictionary(pair => pair.Value.Item1, pair => pair.Value.Item2);
        }
    }
}