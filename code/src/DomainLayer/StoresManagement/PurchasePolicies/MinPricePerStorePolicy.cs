using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Models.PurchasePolicyModels;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public class MinPricePerStorePolicy : PurchasePolicy
    {
        [BsonId]
        public Guid ID { get; set; }
        public double MinPrice { get; set; }

        public MinPricePerStorePolicy(double min, Guid ID)
        {
            this.MinPrice = min;
            this.ID = ID;
        }

        public bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address)
        {
            return MinPrice <= totalPrice;
        }

        public PurchasePolicyModel CreateModel()
        {
            return new MinPricePerStorePolicyModel(this.ID, this.MinPrice);
        }

        public Guid getID()
        {
            return ID;
        }
    }
}