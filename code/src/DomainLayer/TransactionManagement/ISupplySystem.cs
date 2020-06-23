using ECommerceSystem.DomainLayer.StoresManagement;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.TransactionManagement
{
    internal interface ISupplySystem
    {
        Task<(bool, int)> supply(IDictionary<Product, int> products, IDictionary<string, string> shippment_details, string name);

        Task<bool> cancelSupply(int transactionID);

        public void SetExternal(IExternalSupplyPayment External);
    }
}