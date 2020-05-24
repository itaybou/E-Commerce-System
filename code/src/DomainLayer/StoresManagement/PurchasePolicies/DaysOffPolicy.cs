using System;
using System.Collections.Generic;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    internal class DaysOffPolicy : PurchasePolicy
    {
        private List<DayOfWeek> daysOff;
        private Guid _ID;

        public DaysOffPolicy(List<DayOfWeek> daysOff, Guid ID)
        {
            this.daysOff = daysOff;
            this._ID = ID;
        }

        public bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address)
        {
            DayOfWeek today = DateTime.Today.DayOfWeek;
            return !daysOff.Contains(today);
        }

        public Guid getID()
        {
            return _ID;
        }
    }
}