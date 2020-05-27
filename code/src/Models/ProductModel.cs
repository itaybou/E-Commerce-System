using ECommerceSystem.Models.DiscountPolicyModels;
using ECommerceSystem.Models.PurchasePolicyModels;
using System;

namespace ECommerceSystem.Models
{
    public class ProductModel
    {
        private Guid _id;
        private string _name;
        private string _description;
        private int _quantity;
        private double _basePrice;
        private double _priceWithDiscount;
        private DiscountPolicyModel _discount;
        private PurchasePolicyModel _purchasePolicy;

        public ProductModel(Guid id, string name, string description, int quantity, double basePrice, double priceWithDiscount, DiscountPolicyModel discount, PurchasePolicyModel purchase)
        {
            _id = id;
            _name = name;
            _description = description;
            _quantity = quantity;
            _basePrice = basePrice;
            _priceWithDiscount = priceWithDiscount;
            _discount = discount;
            _purchasePolicy = purchase;
        }

        public Guid Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public double BasePrice { get => _basePrice; set => _basePrice = value; }
        public double PriceWithDiscount { get => _priceWithDiscount; set => _priceWithDiscount = value; }
        public DiscountPolicyModel Discount { get => _discount; set => _discount = value; }

        public PurchasePolicyModel PurchasePolicy { get => _purchasePolicy; set => _purchasePolicy = value; }
    }
}