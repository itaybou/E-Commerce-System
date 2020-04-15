using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public abstract class Discount
    {
        private float percentage;
        private DiscountPolicy policy;

        public float Percentage { get => percentage; set => percentage = value; }
        public DiscountPolicy Policy { get => policy; set => policy = value; }

        public abstract double CalculateDiscount(double price); // returns discount percentage
    }
}
