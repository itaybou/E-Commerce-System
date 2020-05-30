using ECommerceSystem.DataAccessLayer.serializers;
using ECommerceSystem.DomainLayer.StoresManagement;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System;
using System.Collections.Generic;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public class StoreShoppingCart
    {
        [BsonSerializer(typeof(StoreReferenceSerializer))]
        public Store Store { get; set; }

        [BsonSerializer(typeof(ProductDictionarySerializer))]
        public Dictionary<Guid, Tuple<Product, int>> ProductQuantities; // Product => Quantity

        public StoreShoppingCart(Store s)
        {
            Store = s;
            ProductQuantities = new Dictionary<Guid, Tuple<Product, int>>();
        }

        public bool AddToCart(Product p, int quantity)
        {
            if (ProductQuantities.ContainsKey(p.Id))
            {
                if (ProductQuantities[p.Id].Item2 + quantity > p.Quantity)
                    return false;
                ProductQuantities[p.Id] = Tuple.Create(p, ProductQuantities[p.Id].Item2 + quantity);
            }
            else ProductQuantities.Add(p.Id, Tuple.Create(p, quantity));
            return true;
        }

        public void ChangeProductQuantity(Product p, int quantity)
        {
            if (!ProductQuantities.ContainsKey(p.Id)) return;
            if (quantity == 0)
                ProductQuantities.Remove(p.Id);
            else ProductQuantities[p.Id] = Tuple.Create(p, quantity);
        }

        public void RemoveFromCart(Product p)
        {
            if (ProductQuantities.ContainsKey(p.Id))
                ProductQuantities.Remove(p.Id);
        }

        public double getTotalCartPrice()
        {
            return Store.getTotalPrice(ProductQuantities);
        }
    }
}