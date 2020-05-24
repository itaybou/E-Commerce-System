using ECommerceSystem.DomainLayer.StoresManagement;
using System.Collections.Generic;

namespace ECommerceSystem.DomainLayer.TransactionManagement
{
    internal interface ISupplySystem
    {
        bool supply(IDictionary<Product, int> products, string address);
    }
}