using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Models;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public class DaysOffPolicy : PurchasePolicy
    {
        List<DayOfWeek> daysOff;
        Guid _ID;

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
