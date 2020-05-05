using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.TransactionManagement
{
    public interface IPaymentSystem
    {
        bool pay(double amount, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV);
        bool refund(double amount, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV);
        bool sendPayment(string storeName, double amount);
    }
}
