using ECommerceSystem.DataAccessLayer.serializers;
using ECommerceSystem.DomainLayer.StoresManagement;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.Collections.Generic;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public class StoreShoppingCart
    {
        private Store _store { get; set; }
        private Dictionary<Product, int> _productQuantities; // Product => Quantity

        [BsonSerializer(typeof(StoreReferenceSerializer))]
        public Store store { get => _store; set => _store = value; }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<Product, int> Products { get => _productQuantities; set => _productQuantities = value; }

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
            return _store.getTotalPrice(_productQuantities);
        }
    }
}