using System;
using System.Collections.Generic;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public class MinPricePerStorePolicy : PurchasePolicy
    {
        private double _minPrice;
        private Guid _ID;

        public MinPricePerStorePolicy(double min, Guid ID)
        {
            this._minPrice = min;
            this._ID = ID;
        }

        public bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address)
        {
            return _minPrice <= totalPrice;
        }

        public Guid getID()
        {
            return _ID;
        }
    }
}