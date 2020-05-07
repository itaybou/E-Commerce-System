using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    class LocationPolicy : PurchasePolicy
    {
        List<string> _bannedLocation;

        public LocationPolicy(List<string> bannedLocation)
        {
            this._bannedLocation = bannedLocation;
        }

        public bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address)
        {
            foreach(string s in _bannedLocation)
            {
                if (address.Contains(s))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
