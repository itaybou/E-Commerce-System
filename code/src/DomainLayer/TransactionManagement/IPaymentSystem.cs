using System;

namespace ECommerceSystem.DomainLayer.TransactionManagement
{
    public interface IPaymentSystem
    {
        bool pay(double amount, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV);

        bool refund(double amount, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV);

        bool sendPayment(string storeName, double amount);
    }
}