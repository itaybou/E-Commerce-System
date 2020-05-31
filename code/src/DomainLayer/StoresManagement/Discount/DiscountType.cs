using System;
using System.Collections.Generic;
using ECommerceSystem.Models.DiscountPolicyModels;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceSystem.DomainLayer.StoresManagement.Discount
{
    public abstract class DiscountType : DiscountPolicy
    {
        [BsonId]
        public Guid ID { get; set; }
        public float Percentage { get; set; }
        public DateTime ExpirationDate { get; set; }

        protected DiscountType(float percentage, DateTime expDate, Guid id)
        {
            Percentage = percentage;
            ExpirationDate = expDate;
            ID = id;
        }


        public abstract void calculateTotalPrice(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products);
        public abstract DiscountPolicyModel CreateModel();

        public Guid getID()
        {
            return ID;
        }

        public abstract bool isSatisfied(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products);
    }
}