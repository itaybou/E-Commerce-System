using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Models.PurchasePolicyModels;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public class LocationPolicy : PurchasePolicy
    {
        private List<string> _bannedLocation;
        private Guid _ID;

        public LocationPolicy(List<string> bannedLocation, Guid ID)
        {
            this._bannedLocation = bannedLocation;
            this._ID = ID;
        }

        public bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address)
        {
            if(address == null)
            {
                return false;
            }

            foreach(string s in _bannedLocation)
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
            return new LocationPolicyModel(this._ID, this._bannedLocation);
        }

        public Guid getID()
        {
            return _ID;
        }
    }
}