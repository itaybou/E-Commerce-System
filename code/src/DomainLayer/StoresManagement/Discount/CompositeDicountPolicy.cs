using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Models.DiscountPolicyModels;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceSystem.DomainLayer.StoresManagement.Discount
{
    public abstract class CompositeDiscountPolicy : DiscountPolicy
    {
        [BsonId]
        public Guid ID { get; set; }
        public List<DiscountPolicy> Children { get; set; }

        protected CompositeDiscountPolicy(Guid ID)
        {
            this.ID = ID;
            Children = new List<DiscountPolicy>();
        }

        protected CompositeDiscountPolicy(Guid ID, List<DiscountPolicy> children)
        {
            this.ID = ID;
            Children = children;
        }

        public abstract void calculateTotalPrice(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products);

        public Guid getID()
        {
            return ID;
        }

        public abstract bool isSatisfied(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products);

        public void Remove(Guid discountPolicyID)
        {
            
            List<DiscountPolicy> newChildren = new List<DiscountPolicy>();
            foreach (DiscountPolicy d in Children)
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

            Children = newChildren;
        }

        public DiscountPolicy getByID(Guid id)
        {
            //check if this contains id
            foreach (DiscountPolicy d in Children)
            {
                if (d.getID().Equals(id))
                {
                    return d;
                }
            }

            //if this isn`t contains id then check recursively
            foreach (DiscountPolicy d in Children)
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

        public abstract DiscountPolicyModel CreateModel();
    }
}