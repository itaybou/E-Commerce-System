using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.Exceptions;
using System;
using System.Collections.Generic;

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

        public bool paymentTransaction(double amount, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV)
        {
            try
            {
                return _paymentSystem.pay(amount, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV);
            }
            catch(Exception e)
            {
                SystemLogger.logger.Error(e.Message);
                throw new ExternalSystemExceptions("Faild : payment failure");
            }        
        }

        public bool refundTransaction(double amount, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV)
        {
            try
            {
                return _paymentSystem.refund(amount, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.Message);
                throw new ExternalSystemExceptions("Faild : refund failure");
            }
        }

        public bool sendPayment(Store store, double amount)
        {
            try
            {
                return _paymentSystem.sendPayment(store.Name, amount);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.Message);
                throw new ExternalSystemExceptions("Faild : send payment to store failure");
            }
        }

        public bool supplyTransaction(IDictionary<Product, int> products, string address)
        {
            try
            {
                return _supplySystem.supply(products, address);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.Message);
                throw new ExternalSystemExceptions("Faild : supply transaction");
            }
        }
    }
}