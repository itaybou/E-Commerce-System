using ECommerceSystem.DomainLayer.StoresManagement;
using System.Collections.Generic;

namespace ECommerceSystem.DomainLayer.TransactionManagement
{
    internal class SupplySystemAdapter : ISupplySystem
    {
        public bool supply(IDictionary<Product, int> products, string address)
        {
            return true;
        }
    }
}