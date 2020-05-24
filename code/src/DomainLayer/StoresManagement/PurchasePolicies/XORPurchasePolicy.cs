using System;
using System.Collections.Generic;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public class XORPurchasePolicy : CompositePurchasePolicy
    {
        public XORPurchasePolicy(Guid ID) : base(ID)
        {
        }

        public XORPurchasePolicy(List<PurchasePolicy> childrens, Guid ID) : base(childrens, ID)
        {
        }

        public override bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address)
        {
            if (base.Children.Count == 0)
            {
                return true;
            }

            int counter = 0;
            foreach (PurchasePolicy p in base.Children)
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