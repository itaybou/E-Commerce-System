using ECommerceSystem.Exceptions;
using ECommerceSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.TransactionManagement
{
    public class ExternalSupplyPayment : IExternalSupplyPayment
    {
        private readonly HttpClient Client;
        private string ExternalURL;
        private bool Active;
        private Range<int> TransacionIDRange;
        private static TimeSpan TimeoutSeconds = TimeSpan.FromSeconds(30);

        public ExternalSupplyPayment(HttpClient client)
        {
            Client = client;
            Client.Timeout = TimeoutSeconds;
            TransacionIDRange = new Range<int>(10000, 100000);
        }

        public async Task<bool> ConnectExternal(string url)
        {
            var uri = new Uri(url);
            var postValues = new Dictionary<string, string>
            {
                { "action_type", "handshake"}
            };
            var content = new FormUrlEncodedContent(postValues);
            try
            {
                var response = await Client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    bool ok = responseString.ToLower().Equals("ok");
                    ExternalURL = url;
                    if (ok)
                        Active = true;
                    else Active = false;
                    return ok;
                } return false;
            }
            catch (Exception)
            {
                Active = false;
                throw new ExternalSystemException("Failed to create handshake with external systems " + DateTime.Now.ToString());
            }
        }

        public async Task<(bool, int)> pay(string cardNumber, string month, string year, string cardHolder, string cvv, string id)
        {
            if (!Active)
            {
                if(!(await ConnectExternal(ExternalURL)))
                    throw new ExternalSystemException("Failed to create handshake with external systems");
                else await pay(cardNumber, month, year, cardHolder, cvv, id);
            }
            var uri = new Uri(ExternalURL);
            var postValues = new Dictionary<string, string> 
            { 
                { "action_type", "pay" },
                { "card_number", cardNumber },
                { "month", month },
                { "year", year },
                { "holder", cardHolder },
                { "ccv", cvv },
                { "id", id }
            };
            var content = new FormUrlEncodedContent(postValues);
            try
            {
                var response = await Client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var transactionID = Int32.Parse(responseString);
                    if (TransacionIDRange.inRange(transactionID))
                        return (true, transactionID);
                    else return (false, -1);
                }
                else return (false, -1);
            }
            catch (Exception)
            {
                Active = false;
                throw new ExternalSystemException("Failed to pay using external system. payments details: " + cardNumber + "," + cvv + "," + month + "/" + year);
            }
        }

        public async Task<bool> cancelPay(string transactionID)
        {
            if (!Active)
            {
                if (!(await ConnectExternal(ExternalURL)))
                    throw new ExternalSystemException("Failed to create handshake with external systems");
                else await cancelPay(transactionID);
            }
            var uri = new Uri(ExternalURL);
            var postValues = new Dictionary<string, string>
            {
                { "action_type", "cancel_pay" },
                { "transaction_id", transactionID },
            };
            var content = new FormUrlEncodedContent(postValues);
            try
            {
                var response = await Client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var result = Int32.Parse(responseString);
                    return result > 0;
                }
                return false;

            }
            catch (Exception)
            {
                Active = false;
                throw new ExternalSystemException("Failed to cancel payment by external system. transaction id: " + transactionID);
            }
        }

        public async Task<(bool, int)> supply(string name, string address, string city, string country, string zip)
        {
            if (!Active)
            {
                if (!(await ConnectExternal(ExternalURL)))
                    throw new ExternalSystemException("Failed to create handshake with external systems");
                else await supply(name, address, city, country, zip);
            }
            var uri = new Uri(ExternalURL);
            var postValues = new Dictionary<string, string>
            {
                { "action_type", "supply" },
                { "name", name },
                { "address", address },
                { "city", city },
                { "country", country },
                { "zip", zip }
            };
            var content = new FormUrlEncodedContent(postValues);
            try
            {
                var response = await Client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var transactionID = Int32.Parse(responseString);
                    if (TransacionIDRange.inRange(transactionID))
                        return (true, transactionID);
                    else return (false, -1);
                }
                return (false, -1);
            }
            catch (Exception)
            {
                Active = false;
                throw new ExternalSystemException("Failed to supply using external system. " + address + "," + city + "," + country + "," + zip);
            }
        }

        public async Task<bool> cancelSupply(string transactionID)
        {
            if (!Active)
            {
                if (!(await ConnectExternal(ExternalURL)))
                    throw new ExternalSystemException("Failed to create handshake with external systems");
                else await cancelPay(transactionID);
            }
            var uri = new Uri(ExternalURL);
            var postValues = new Dictionary<string, string>
            {
                { "action_type", "cancel_supply" },
                { "transaction_id", transactionID },
            };
            var content = new FormUrlEncodedContent(postValues);
            try
            {
                var response = await Client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var result = Int32.Parse(responseString);
                    return result > 0;
                }
                return false;

            }
            catch (Exception)
            {
                Active = false;
                throw new ExternalSystemException("Failed to cancel supply request by external system. transaction id: " + transactionID);
            }
        }
    }
}
