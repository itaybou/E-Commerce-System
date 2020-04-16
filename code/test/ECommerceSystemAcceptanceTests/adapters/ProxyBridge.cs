using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceSystemAcceptanceTests.adapters
{
    class ProxyBridge : IBridgeAdapter
    {
        private IBridgeAdapter _real;

        internal IBridgeAdapter RealBridge { get => _real; set => _real = value; }

        // Utility methods
        public bool IsUserLogged(string username)
        {
            if (_real != null)
            {
                return _real.IsUserLogged(username);
            }
            else return false;
        }

        public bool IsUserSubscribed(string username)
        {
            if (_real != null)
            {
                return _real.IsUserSubscribed(username);
            }
            else return false;
        }

        public void usersCleanUp()
        {
            if (_real != null)
            {
                _real.usersCleanUp();
            }
        }

        public void openStoreWithProducts(string storeName, string ownerName, List<string> products)
        {
            if (_real != null)
            {
                _real.openStoreWithProducts(storeName, ownerName, products);
            }
        }

        // Requirments
        public bool register(string uname, string pswd, string fname, string lname, string email) // 2.2
        {
            if (_real != null)
            {
                return _real.register(uname, pswd, fname, lname, email);
            }
            else return true;
        }

        public bool login(string uname, string pswd) // 2.3
        {
            if (_real != null)
            {
                return _real.login(uname, pswd);
            }
            else return true;
        }

        public Dictionary<string, List<string>> ViewProdcutStoreInfo() // 2.4
        {
            if (_real != null)
            {
                return _real.ViewProdcutStoreInfo();
            }
            else return new Dictionary<string, List<string>>();
        }

        public List<string> SearchAndFilterProducts(string prodName, string catName, List<string> keywords, List<string> filters, double from, double to)
        {
            if (_real != null)
            {
                return _real.SearchAndFilterProducts(prodName, catName, keywords, filters, from, to);
            }
            else return new List<string>();
        }

        public void cancelSearchFilters()
        {
            if (_real != null)
            {
                _real.cancelSearchFilters();
            }
        }
    }
}