using System;
using System.Collections.Generic;

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
            if (base.Children.Count == 0)
            {
                return true;
            }

            bool output = false;
            foreach (PurchasePolicy p in base.Children)
            {
                output = output || p.canBuy(products, totalPrice, address);
            }
            return output;
        }
    }
}