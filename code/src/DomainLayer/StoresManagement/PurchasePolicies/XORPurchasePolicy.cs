using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Models.PurchasePolicyModels;
using ECommerceSystem.Models;

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

        public override PurchasePolicyModel CreateModel()
        {
            List<PurchasePolicyModel> childrenModels = new List<PurchasePolicyModel>();
            foreach(PurchasePolicy p in this.Children)
            {
                childrenModels.Add(p.CreateModel());
            }
            return new CompositePurchasePolicyModel(this.ID, childrenModels, CompositeType.Xor);
        }
    }
}