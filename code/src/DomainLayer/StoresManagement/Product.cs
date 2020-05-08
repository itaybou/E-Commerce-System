using System;
using ECommerceSystem.DomainLayer.StoresManagement.Discount;
using ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public class Product
    {
        private Guid _id;
        private string _name;
        private string _description;
        private int _quantity;
        private double _basePrice;
        private DiscountType _discount;
        private PurchaseType _purchaseType;
        private ProductQuantityPolicy purchasePolicy;

        public Product(string name, string description, PurchaseType purchaseType, int quantity, double price, Guid guid)
        {
            this._name = name;
            this._description = description;
            this._quantity = quantity;
            this._discount = null;
            this._purchaseType = purchaseType;
            this._basePrice = price;
            this._id = guid;
        }

        /// <summary>
        /// Calculate price including discount
        /// </summary>
        /// <returns>Price after discount</returns>
        public double CalculateDiscount()
        {
            return _discount == null ? _basePrice : _discount.CalculateDiscount(_basePrice);
        }

        public Guid Id { get => _id;}
        public double BasePrice { get => _basePrice; set => _basePrice = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public DiscountType Discount { get => _discount; set => _discount = value; }
        public PurchaseType PurchaseType { get => _purchaseType; set => _purchaseType = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public ProductQuantityPolicy PurchasePolicy { get => purchasePolicy; set => purchasePolicy = value; }
    }
}