﻿using System;
using System.Collections.Generic;
using ECommerceSystem.Models.DiscountPolicyModels;

namespace ECommerceSystem.DomainLayer.StoresManagement.Discount
{
    public interface DiscountPolicy
    {
        void calculateTotalPrice(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products);

        bool isSatisfied(Dictionary<Guid, (double basePrice, int quantity, double totalPrice)> products);

        Guid getID();
        DiscountPolicyModel CreateModel();
    }
}