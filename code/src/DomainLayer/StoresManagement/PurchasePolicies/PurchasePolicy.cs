using System;
using System.Collections.Generic;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies
{
    public interface PurchasePolicy
    {
        bool canBuy(IDictionary<Guid, int> products, double totalPrice, string address);

        Guid getID();
    }
}