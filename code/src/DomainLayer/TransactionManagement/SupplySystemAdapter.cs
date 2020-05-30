using ECommerceSystem.DomainLayer.StoresManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.TransactionManagement
{
    internal class SupplySystemAdapter : ISupplySystem
    {
        public async Task<bool> supply(IDictionary<Product, int> products, string address)
        {
            return true;
        }
    }
}