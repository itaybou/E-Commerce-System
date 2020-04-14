using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public class VisibleDiscount : Discount
    {
        public override double CalculateDiscount(double price)
        {
            return ((100 - this._percentage)/100) * price;
        }
    }
}
