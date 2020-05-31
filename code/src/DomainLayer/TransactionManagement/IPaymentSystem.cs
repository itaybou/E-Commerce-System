using System;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.TransactionManagement
{
    public interface IPaymentSystem
    {
        Task<bool> pay(double amount, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV);

        Task<bool> refund(double amount, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV);

        Task<bool> sendPayment(string storeName, double amount);
    }
}