using System;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.TransactionManagement
{
    public class PaymentSystemAdapter : IPaymentSystem
    {
        public async Task<bool> pay(double amount, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int cVV)
        {
            if (cVV.ToString().Length != 3)
            {
                return false;
            }

            if (amount <= 0)
            {
                return false;
            }

            if (expirationCreditCard < DateTime.Now)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> refund(double amount, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int cVV)
        {
            return true;
        }

        public async Task<bool> sendPayment(string storeName, double amount)
        {
            return true;
        }
    }
}