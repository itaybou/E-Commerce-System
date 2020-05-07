using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public class XORPurchasePolicy : CompositePurchasePolicy
    {
        public override bool canBuy(IDictionary <Guid, int> products, double totalPrice, string address)
        {
            int counter = 0;
            foreach(PurchasePolicy p in _childrens)
            {
                if (p.canBuy(products, totalPrice, address))
                {
                    counter++;
                }
            }
            return counter == 1;
        }
    }
}
