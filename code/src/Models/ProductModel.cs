using ECommerceSystem.Models.DiscountPolicyModels;
using ECommerceSystem.Models.PurchasePolicyModels;
using MongoDB.Bson.Serialization.Attributes;
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
        private DiscountPolicyModel _discount;
        private PurchasePolicyModel _purchasePolicy;
        public string ImageURL { get; set; }

        public ProductModel(Guid id, string name, string description, int quantity, double basePrice, DiscountPolicyModel discount, PurchasePolicyModel purchase)
        {
            _id = id;
            _name = name;
            _description = description;
            _quantity = quantity;
            _basePrice = basePrice;
            _discount = discount;
            _purchasePolicy = purchase;
        }

        public ProductModel()
        {

        }

        public ProductModel(Guid id, string name, string description, int quantity, double basePrice, DiscountPolicyModel discount, PurchasePolicyModel purchase, string imageURL)
        {
            _id = id;
            _name = name;
            _description = description;
            _quantity = quantity;
            _basePrice = basePrice;
            _discount = discount;
            _purchasePolicy = purchase;
            ImageURL = imageURL;
        }

        public Guid Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public double BasePrice { get => _basePrice; set => _basePrice = value; }
        [BsonIgnore]
        public DiscountPolicyModel Discount { get => _discount; set => _discount = value; }
        [BsonIgnore]
        public PurchasePolicyModel PurchasePolicy { get => _purchasePolicy; set => _purchasePolicy = value; }
    }
}