using ECommerceSystem.DomainLayer.TransactionManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystemUnitTests.DomainLayer.SystemManagement
{
    class ExternalSystemsStub : IExternalSupplyPayment
    {
        public Task<bool> cancelPay(string transactionID)
        {
            return new Task<bool>(() => true);
        }

        public Task<bool> cancelSupply(string transactionID)
        {
            return new Task<bool>(() => true);
        }

        public Task<bool> ConnectExternal(string url)
        {
            throw new NotImplementedException();
        }

        public Task<(bool, int)> pay(string cardNumber, string month, string year, string cardHolder, string cvv, string id)
        {
            if(cardNumber == null)
            {
                return new Task<(bool, int)>(() => (false, -1));
            }
            else
            {
                return new Task<(bool, int)>(() => (true, 1));
            }
        }

        public Task<(bool, int)> supply(string name, string address, string city, string country, string zip)
        {
            if (address == null)
            {
                return new Task<(bool, int)>(() => (false, -1));
            }
            else
            {
                return new Task<(bool, int)>(() => (true, 1));
            }
        }
    }
}
