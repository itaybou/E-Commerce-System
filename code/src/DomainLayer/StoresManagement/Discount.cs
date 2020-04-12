﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public abstract class Discount
    {
        private float _percentage;
        private DiscountPolicy _policy;

        public abstract double CalculateDiscount(double price); // returns discount percentage
    }
}
