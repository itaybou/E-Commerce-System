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
        public int CancelPayCounter = 0;
        public int PayCounter = 0;

        public Task<bool> cancelPay(string transactionID)
        {
            CancelPayCounter++;
            Task<bool> task = new Task<bool>(() => true);
            task.RunSynchronously();
            return task; 
        }

        public Task<bool> cancelSupply(string transactionID)
        {
            Task<bool> task = new Task<bool>(() => true);
            task.RunSynchronously();
            return task;
        }

        public Task<bool> ConnectExternal(string url)
        {
            throw new NotImplementedException();
        }

        public Task<(bool, int)> pay(string cardNumber, string month, string year, string cardHolder, string cvv, string id)
        {
            if(cardNumber != null)
            {
                PayCounter++;
                Task<(bool, int)> task = new Task<(bool, int)>(() => (true, 1));
                task.RunSynchronously();
                return task;
            }
            else
            {
                Task<(bool, int)> task = new Task<(bool, int)>(() => (false, -1));
                task.RunSynchronously();
                return task;
            }
        }

        public Task<(bool, int)> supply(string name, string address, string city, string country, string zip)
        {
            if (!name.Equals(""))
            {
                Task<(bool, int)> task = new Task<(bool, int)>(() => (true, 1));
                task.RunSynchronously();
                return task;
            }
            else
            {
                Task<(bool, int)> task = new Task<(bool, int)>(() => (false, -1));
                task.RunSynchronously();
                return task;
            }
        }
    }
}
