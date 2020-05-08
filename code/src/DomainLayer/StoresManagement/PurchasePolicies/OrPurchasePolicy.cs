using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public class OrPurchasePolicy : CompositePurchasePolicy
    {
        public OrPurchasePolicy(Guid ID) : base(ID)
        {
        }

        public OrPurchasePolicy(List<PurchasePolicy> childrens, Guid ID) : base(childrens, ID)
        {
        }

        public override bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address)
        {
            bool output = false;
            foreach(PurchasePolicy p in _children)
            {
                output = output || p.canBuy(products, totalPrice, address);
            }
            return output;
        }
    }
}
