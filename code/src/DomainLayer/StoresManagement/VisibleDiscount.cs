using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public class VisibleDiscount : Discount
    {

        public VisibleDiscount(float percentage, DiscountPolicy policy)
        {
            this.Percentage = percentage;
            this.Policy = policy;
        }
        
        public override double CalculateDiscount(double price)
        {
            return ((100 - this.Percentage)/100) * price;
        }
    }
}
