using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.Discount
{
    public class OrDiscountPolicy : CompositeDicountPolicy
    {
        public OrDiscountPolicy(DiscountPolicy left, DiscountPolicy right, Guid ID) : base(left, right, ID)
        {
        }


        public override void calculateTotalPrice(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            _left.calculateTotalPrice(products);
            _right.calculateTotalPrice(products);
        }

        public override bool isSatisfied(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products)
        {
            return (_left.isSatisfied(products) || _right.isSatisfied(products));
        }
    }
}
