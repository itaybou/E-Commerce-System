using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Models.PurchasePolicyModels;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public abstract class CompositePurchasePolicy : PurchasePolicy
    {
        [BsonId]
        public Guid ID { get; set; }
        public List<PurchasePolicy> Children { get; set; }

        public CompositePurchasePolicy(Guid ID)
        {
            Children = new List<PurchasePolicy>();
            this.ID = ID;
        }

        public CompositePurchasePolicy(List<PurchasePolicy> childrens, Guid ID)
        {
            Children = childrens;
            this.ID = ID;
        }

        public abstract bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address);

        public Guid getID()
        {
            return ID;
        }

        public void Remove(Guid purchasePolicyID)
        {
            List<PurchasePolicy> newChildren = new List<PurchasePolicy>();
            foreach (PurchasePolicy p in Children)
            {
                if (!p.getID().Equals(purchasePolicyID))
                {
                    newChildren.Add(p);

                    //remove from the children
                    if (p is CompositePurchasePolicy)
                    {
                        ((CompositePurchasePolicy)p).Remove(purchasePolicyID);
                    }
                }
            }
            Children = newChildren;
        }

        public PurchasePolicy getByID(Guid ID)
        {
            //check if this contains id
            foreach (PurchasePolicy p in Children)
            {
                if (p.getID().Equals(ID))
                {
                    return p;
                }
            }

            //if this isn`t contains id then check recursively
            foreach (PurchasePolicy p in Children)
            {
                if (p is CompositePurchasePolicy)
                {
                    PurchasePolicy wanted = ((CompositePurchasePolicy)p).getByID(ID);
                    if (wanted != null)
                    {
                        return wanted;
                    }
                }
            }

            return null;
        }

        public abstract PurchasePolicyModel CreateModel();
    }
}