using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.Discount
{
    public abstract class CompositeDiscountPolicy : DiscountPolicy
    {
        private List<DiscountPolicy> _children;
        protected Guid _ID;

        public List<DiscountPolicy> Children { get => _children; set => _children = value; }

        protected CompositeDiscountPolicy(Guid ID)
        {
            _ID = ID;
            _children = new List<DiscountPolicy>();
        }

        protected CompositeDiscountPolicy(Guid ID, List<DiscountPolicy> children)
        {
            _ID = ID;
            _children = children;
        }

        public abstract void calculateTotalPrice(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products);

        public Guid getID()
        {
            return _ID;
        }

        public abstract bool isSatisfied(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products);


        public void Remove(Guid discountPolicyID)
        {
            List<DiscountPolicy> newChildren = new List<DiscountPolicy>();
            foreach (DiscountPolicy d in _children)
            {
                if (!d.getID().Equals(discountPolicyID))
                {
                    newChildren.Add(d);

                    //remove from the children
                    if (d is CompositeDiscountPolicy)
                    {
                        ((CompositeDiscountPolicy)d).Remove(discountPolicyID);
                    }
                }
            }
            _children = newChildren;
        }

        public DiscountPolicy getByID(Guid id)
        {
            //check if this contains id
            foreach (DiscountPolicy d in _children)
            {
                if (d.getID().Equals(id))
                {
                    return d;
                }
            }

            //if this isn`t contains id then check recursively
            foreach (DiscountPolicy d in _children)
            {
                if (d is CompositeDiscountPolicy)
                {
                    DiscountPolicy wanted = ((CompositeDiscountPolicy)d).getByID(id);
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
