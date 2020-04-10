using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ECommerceSystem.DomainLayer.TransactionManagement
{
    class TransactionManagement
    {
        public bool paymentTransaction(int amount, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV)
        {
            return _paymentSystem.pay(amount, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV);
        }

        // Params - producs is product id to quantity
        public bool supplyTransaction(Dictionary<long, int> products, string address)
        {
            return _supplySystem.supply(products, address);
        }

    }
}
