using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Models;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    class DaysOffPolicy : PurchasePolicy
    {
        List<DayOfWeek> daysOff;

        public DaysOffPolicy(List<DayOfWeek> daysOff)
        {
            this.daysOff = daysOff;
        }

        public bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address)
        {
            DayOfWeek today = DateTime.Today.DayOfWeek;
            return !daysOff.Contains(today);
        }
    }
}
