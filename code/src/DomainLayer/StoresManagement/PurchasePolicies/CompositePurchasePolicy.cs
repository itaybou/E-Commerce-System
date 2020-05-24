using System;
using System.Collections.Generic;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public abstract class CompositePurchasePolicy : PurchasePolicy
    {
        private List<PurchasePolicy> _children;
        protected Guid _ID;

        public List<PurchasePolicy> Children { get => _children; set => _children = value; }

        public CompositePurchasePolicy(Guid ID)
        {
            _children = new List<PurchasePolicy>();
            _ID = ID;
        }

        public CompositePurchasePolicy(List<PurchasePolicy> childrens, Guid ID)
        {
            _children = childrens;
            _ID = ID;
        }

        public abstract bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address);

        public Guid getID()
        {
            return _ID;
        }

        public void Remove(Guid purchasePolicyID)
        {
            List<PurchasePolicy> newChildren = new List<PurchasePolicy>();
            foreach (PurchasePolicy p in _children)
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
            _children = newChildren;
        }

        public PurchasePolicy getByID(Guid ID)
        {
            //check if this contains id
            foreach (PurchasePolicy p in _children)
            {
                if (p.getID().Equals(ID))
                {
                    return p;
                }
            }

            //if this isn`t contains id then check recursively
            foreach (PurchasePolicy p in _children)
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
    }
}