using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Models;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public class AndPurchasePolicy : CompositePurchasePolicy
    {
        public AndPurchasePolicy(Guid ID) : base(ID)
        {
        }

        public AndPurchasePolicy(List<PurchasePolicy> childrens, Guid ID) : base(childrens, ID)
        {
        }

        public override bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address)
        {
            bool output = true;
            foreach(PurchasePolicy p in base.Children)
            {
                output = output && p.canBuy(products, totalPrice, address);
            }
            return output;
        }

        public void Add(PurchasePolicy purchasePolicy)
        {
            base.Children.Add(purchasePolicy);
        }
    }
}
