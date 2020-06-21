using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.TransactionManagement
{
    public class PaymentSystemAdapter : IPaymentSystem
    {
        private IExternalSupplyPayment External;
        
        public PaymentSystemAdapter(IExternalSupplyPayment external)
        {
            External = external;
        }

        public async Task<(bool, int)> pay(double amount, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int cVV)
        {
            var month = expirationCreditCard.Month.ToString();
            var year = expirationCreditCard.Year.ToString();
            var holderName = firstName + lastName;
            return await External.pay(creditCardNumber, month, year, holderName, cVV.ToString(), id.ToString());
        }

        public async Task<bool> refund(int transactionID)
        {
            return await External.cancelPay(transactionID.ToString());
        }

        public async Task<bool> sendPayment(string storeName, double amount)
        {
            return true;
        }

        public void SetExternal(IExternalSupplyPayment External)
        {
            this.External = External;
        }
    }
}