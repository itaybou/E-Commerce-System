using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.Discount
{
    public class OrDiscountPolicy : CompositeDiscountPolicy
    {
        public OrDiscountPolicy(Guid ID) : base(ID)
        {
        }

        public OrDiscountPolicy(Guid ID, List<DiscountPolicy> children) : base(ID, children)
        {
        }

        public override void calculateTotalPrice(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            foreach(DiscountPolicy d in base.Children)
            {
                d.calculateTotalPrice(products);
            }
        }

        public override bool isSatisfied(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            foreach (DiscountPolicy d in base.Children)
            {
                if (d.isSatisfied(products))
                    return true;
            }

            return false;
        }
    }
}
