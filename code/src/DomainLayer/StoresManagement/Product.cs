using System;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public class Product
    {
        private Guid _id;
        private int _quantity;
        private double _basePrice;
        private Discount _discount;
        private PurchaseType _purchaseType;
        private string _name;
        private string _description;

        public Product(Discount discount, PurchaseType purchaseType, int quantity, double price, Guid id, string name, string description)
        {
            this._quantity = quantity;
            this._discount = discount;
            this._purchaseType = purchaseType;
            this._basePrice = price;
            this._id = id;
            this._name = name;
            this._description = description;
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
        public Discount Discount { get => _discount; set => _discount = value; }
        public PurchaseType PurchaseType { get => _purchaseType; set => _purchaseType = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
    }
}