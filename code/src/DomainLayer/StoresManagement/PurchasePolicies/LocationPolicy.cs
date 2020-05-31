using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Models.PurchasePolicyModels;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public class LocationPolicy : PurchasePolicy
    {
        [BsonId]
        public Guid ID { get; set; }
        public List<string> BannedLocations { get; set; }

        public LocationPolicy(List<string> bannedLocation, Guid ID)
        {
            this.BannedLocations = bannedLocation;
            this.ID = ID;
        }

        public bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address)
        {
            if(address == null)
            {
                return false;
            }

            foreach(string s in BannedLocations)
            {
                if (address.Contains(s))
                {
                    return false;
                }
            }
            return true;
        }

        public PurchasePolicyModel CreateModel()
        {
            return new LocationPolicyModel(this.ID, this.BannedLocations);
        }

        public Guid getID()
        {
            return ID;
        }
    }
}