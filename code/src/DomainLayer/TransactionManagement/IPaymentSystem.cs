using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.TransactionManagement
{
    public interface IPaymentSystem
    {
        Task<(bool, int)> pay(double amount, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV);

        Task<bool> refund(int transactionID);

        Task<bool> sendPayment(string storeName, double amount);

        void SetExternal(IExternalSupplyPayment External);
    }
}