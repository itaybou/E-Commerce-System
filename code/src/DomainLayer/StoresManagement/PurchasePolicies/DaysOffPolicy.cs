using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Models;
using ECommerceSystem.Models.PurchasePolicyModels;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public class DaysOffPolicy : PurchasePolicy
    {
        [BsonId]
        public Guid ID { get; set; }
        public List<DayOfWeek> DaysOff { get; set; }

        public DaysOffPolicy(List<DayOfWeek> daysOff, Guid ID)
        {
            this.DaysOff = daysOff;
            this.ID = ID;
        }

        public bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address)
        {
            DayOfWeek today = DateTime.Today.DayOfWeek;
            return !DaysOff.Contains(today);
        }

        public PurchasePolicyModel CreateModel()
        {
            return new DaysOffPolicyModel(this.ID, this.DaysOff);
        }

        public Guid getID()
        {
            return ID;
        }
    }
}