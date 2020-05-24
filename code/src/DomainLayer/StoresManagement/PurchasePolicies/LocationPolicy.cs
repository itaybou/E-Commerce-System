using System;
using System.Collections.Generic;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    internal class LocationPolicy : PurchasePolicy
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
            foreach (string s in _bannedLocation)
            {
                if (address.Contains(s))
                {
                    return false;
                }
            }
            return true;
        }

        public Guid getID()
        {
            return _ID;
        }
    }
}