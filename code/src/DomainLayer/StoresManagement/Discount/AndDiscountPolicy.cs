using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.Discount
{
    public class AndDiscountPolicy : CompositeDicountPolicy
    {
        public AndDiscountPolicy(Guid ID) : base(ID)
        {
        }

        public AndDiscountPolicy(Guid ID, List<DiscountPolicy> children) : base(ID, children)
        {
        }

        public override void calculateTotalPrice(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            if (this.isSatisfied(products))
            {
                foreach (DiscountPolicy d in _children)
                {
                    d.calculateTotalPrice(products);
                }
            }
        }

        public override bool isSatisfied(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            foreach(DiscountPolicy d in _children)
            {
                if (!d.isSatisfied(products))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
