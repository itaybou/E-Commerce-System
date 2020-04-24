using ECommerceSystem.DomainLayer.StoresManagement;
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
            return _paymentSystem.pay(amount, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV);
        }

        public bool refundTransaction(double amount, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV)
        {
            return _paymentSystem.refund(amount, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV);
        }

        public bool sendPayment(Store store, double amount)
        {
            return _paymentSystem.sendPayment(store.Name, amount);
        }

        public bool supplyTransaction(IDictionary<Product, int> products, string address)
        {
            return _supplySystem.supply(products, address);
        }
    }
}