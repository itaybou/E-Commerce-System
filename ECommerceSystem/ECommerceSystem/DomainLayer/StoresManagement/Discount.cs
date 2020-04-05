using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    abstract class Discount
    {
        private float _percentage;
        private DiscountPolicy _policy;
    }
}
