using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.Discount
{
    public abstract class CompositeDicountPolicy : DiscountPolicy
    {
        protected DiscountPolicy _left;
        protected DiscountPolicy _right;
        protected Guid _ID;

        protected CompositeDicountPolicy(DiscountPolicy left, DiscountPolicy right, Guid ID)
        {
            _left = left;
            _right = right;
            _ID = ID;
        }

        public abstract void calculateTotalPrice(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products);
        public abstract bool isSatisfied(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products);
    }
}
