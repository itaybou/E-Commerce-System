using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.StoresManagement;


namespace ECommerceSystem.DomainLayer.TransactionManagement
{
    class TransactionManager
    {
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
            return _paymentSystem.pay(store, amount);
        }

        // Params - producs is product id to quantity
        public bool supplyTransaction(Dictionary<Product, int> products, string address)
        {
            return _supplySystem.supply(products, address);
        }

    }
}
