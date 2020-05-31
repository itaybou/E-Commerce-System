using ECommerceSystem.DomainLayer.StoresManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.TransactionManagement
{
    internal interface ISupplySystem
    {
        Task<bool> supply(IDictionary<Product, int> products, string address);
    }
}