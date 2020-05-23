using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Models.PurchasePolicyModels;

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
            foreach(PurchasePolicy p in base.Children)
            {
                output = output || p.canBuy(products, totalPrice, address);
            }
            return output;
        }

        public override PurchasePolicyModel CreateModel()
        {
            List<PurchasePolicyModel> childrenModels = new List<PurchasePolicyModel>();
            foreach (PurchasePolicy p in this.Children)
            {
                childrenModels.Add(p.CreateModel());
            }
            return new CompositePurchasePolicyModel(this._ID, childrenModels, CompositeType.Or);
        }
    }
}
