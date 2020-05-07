using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public class OrPurchasePolicy : CompositePurchasePolicy
    {
        public override bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address)
        {
            bool output = false;
            foreach(PurchasePolicy p in _childrens)
            {
                output = output || p.canBuy(products, totalPrice, address);
            }
            return output;
        }
    }
}
