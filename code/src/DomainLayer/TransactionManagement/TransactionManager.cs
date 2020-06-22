using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.TransactionManagement
{
    public class TransactionManager
    {
        private IExternalSupplyPayment _external;
        private IPaymentSystem _paymentSystem;
        private ISupplySystem _supplySystem;

        private static readonly Lazy<TransactionManager> lazy = new Lazy<TransactionManager>(() => new TransactionManager());

        public static TransactionManager Instance => lazy.Value;

        private readonly HttpClient _client;

        private TransactionManager()
        {
            _client = new HttpClient();
            _external = new ExternalSupplyPayment(_client);
            _paymentSystem = new PaymentSystemAdapter(_external);
            _supplySystem = new SupplySystemAdapter(_external);
        }

        public async Task<bool> ConnectExternal(string url)
        {
            try
            {
                return await _external.ConnectExternal(url);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.Message);
                throw new ExternalSystemException("Faild : payment failure");
            }
        }

        public async Task<(bool, int)> paymentTransaction(double amount, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV)
        {
            try
            {
                return await _paymentSystem.pay(amount, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV);
            }
            catch(Exception e)
            {
                SystemLogger.logger.Error(e.Message);
                throw new ExternalSystemException("Faild : payment failure");
            }        
        }

        public async Task<bool> refundTransaction(int transactionID)
        {
            try
            {
                return await _paymentSystem.refund(transactionID);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.Message);
                throw new ExternalSystemException("Faild : refund failure");
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
                throw new ExternalSystemException("Faild : send payment to store failure");
            }
        }

        public async Task<(bool, int)> supplyTransaction(IDictionary<Product, int> products, IDictionary<string, string> shippment_details, string name)
        {
            try
            {
                return await _supplySystem.supply(products, shippment_details, name);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.Message);
                throw new ExternalSystemException("Faild : supply transaction");
            }
        }

        public void setTestExternalSystems(IExternalSupplyPayment External)
        {
            _paymentSystem.SetExternal(External);
            _supplySystem.SetExternal(External);
        }

        public void setRealExternalSystems()
        {
            _paymentSystem.SetExternal(_external);
            _supplySystem.SetExternal(_external);
        }
    }
}