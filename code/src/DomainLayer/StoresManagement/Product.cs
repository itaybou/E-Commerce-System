using ECommerceSystem.DomainLayer.StoresManagement.Discount;
using ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public class Product
    {
        [BsonId]
        public Guid Id { get; set; }
        [Required]
        [BsonElement("name")]
        public string Name { get; set; }
        [Required]
        [BsonElement("description")]
        public string Description { get; set; }
        [Required]
        [BsonElement("quantity")]
        public int Quantity { get; set; }
        [Required]
        [BsonElement("price")]
        public double BasePrice { get; set; }
        [Required]
        [BsonElement("discount")]
        public DiscountType Discount { get; set; }
        [Required]
        [BsonElement("purchase")]
        public PurchaseType PurchaseType { get; set; }
        [Required]
        [BsonElement("purchase_policy")]
        public ProductQuantityPolicy PurchasePolicy { get; set; }

        public Product(string name, string description, int quantity, double price, Guid guid)
        {
            this.Name = name;
            this.Description = description;
            this.Quantity = quantity;
            this.Discount = null;
            this.PurchaseType = null;
            this.BasePrice = price;
            this.Id = guid;
        }

        /// <summary>
        /// Calculate price including discount
        /// </summary>
        /// <returns>Price after discount</returns>
        public double CalculateDiscount()
        {
            return Discount == null ? BasePrice : BasePrice; // TODO: update return value of the calaulated discount or percentage
            //_discount.CalculateDiscount(_basePrice);
        }
    }
}