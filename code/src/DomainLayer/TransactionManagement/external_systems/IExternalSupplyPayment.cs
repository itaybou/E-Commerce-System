using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.TransactionManagement
{
    public interface IExternalSupplyPayment
    {
        Task<bool> ConnectExternal(string url);

        Task<(bool, int)> pay(string cardNumber, string month, string year, string cardHolder, string cvv, string id);

        Task<bool> cancelPay(string transactionID);

        Task<(bool, int)> supply(string name, string address, string city, string country, string zip);

        Task<bool> cancelSupply(string transactionID);
    }
}
