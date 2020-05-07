using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public class AndPurchasePolicy : CompositePurchasePolicy
    {
        public override bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address)
        {
            bool output = true;
            foreach(PurchasePolicy p in _childrens)
            {
                output = output && p.canBuy(products, totalPrice, address);
            }
            return output;
        }
    }
}
