using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.TransactionManagement
{
    public class TransactionManager
    {
        private IPaymentSystem _paymentSystem;
        private ISupplySystem _supplySystem;

        private static readonly Lazy<TransactionManager> lazy = new Lazy<TransactionManager>(() => new TransactionManager());

        public static TransactionManager Instance => lazy.Value;

        private TransactionManager()
        {
            _paymentSystem = new PaymentSystemAdapter();
            _supplySystem = new SupplySystemAdapter();
        }

        public async Task<bool> paymentTransaction(double amount, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV)
        {
            try
            {
                return await _paymentSystem.pay(amount, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV);
            }
            catch(Exception e)
            {
                SystemLogger.logger.Error(e.Message);
                throw new ExternalSystemExceptions("Faild : payment failure");
            }        
        }

        public async Task<bool> refundTransaction(double amount, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV)
        {
            try
            {
                return await _paymentSystem.refund(amount, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.Message);
                throw new ExternalSystemExceptions("Faild : refund failure");
            }
        }

        public async Task<bool> sendPayment(Store store, double amount)
        {
            try
            {
                return await _paymentSystem.sendPayment(store.Name, amount);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.Message);
                throw new ExternalSystemExceptions("Faild : send payment to store failure");
            }
        }

        public async Task<bool> supplyTransaction(IDictionary<Product, int> products, string address)
        {
            try
            {
                return await _supplySystem.supply(products, address);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.Message);
                throw new ExternalSystemExceptions("Faild : supply transaction");
            }
        }
    }
}