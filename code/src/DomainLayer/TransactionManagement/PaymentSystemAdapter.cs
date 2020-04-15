using ECommerceSystem.DomainLayer.StoresManagement;
using System;

namespace ECommerceSystem.DomainLayer.TransactionManagement
{
    internal class PaymentSystemAdapter
    {
        internal bool pay(double amount, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int cVV)
        {
            return true;
        }

        internal bool refund(double amount, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int cVV)
        {
            return true;
        }

        internal bool pay(Store store, double amount)
        {
            return true;
        }
    }
}