using ECommerceSystem.DomainLayer.StoresManagement;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.TransactionManagement
{
    internal class SupplySystemAdapter : ISupplySystem
    {
        private IExternalSupplyPayment External;

        public SupplySystemAdapter(IExternalSupplyPayment external)
        {
            External = external;
        }

        public async Task<(bool, int)> supply(IDictionary<Product, int> products, IDictionary<string, string> shippment_details, string name)
        {
            return await External.supply(name, shippment_details["address"], shippment_details["city"], shippment_details["country"], shippment_details["zip"]);
        }

        public Task<bool> cancelSupply(int transactionID)
        {
            return External.cancelSupply(transactionID.ToString());
        }
    }
}