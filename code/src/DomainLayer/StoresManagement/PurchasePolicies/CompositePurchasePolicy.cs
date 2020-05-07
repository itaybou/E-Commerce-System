using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public abstract class CompositePurchasePolicy : PurchasePolicy
    {
        protected List<PurchasePolicy> _childrens;

        public CompositePurchasePolicy()
        {
            _childrens = new List<PurchasePolicy>();
        }

        public abstract bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address);
    }
}
