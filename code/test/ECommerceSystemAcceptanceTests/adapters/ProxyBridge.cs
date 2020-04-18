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

        public void storesCleanUp()
        {
            if (_real != null)
            {
                _real.storesCleanUp();
            }
        }

        public void cancelSearchFilters()
        {
            if (_real != null)
            {
                _real.cancelSearchFilters();
            }
        }

        public void openStoreWithProducts(string storeName, string ownerName, List<string> products)
        {
            if (_real != null)
            {
                _real.openStoreWithProducts(storeName, ownerName, products);
            }

        }

        public Dictionary<string, Dictionary<long, int>> getUserCartDetails()
        {
            if (_real != null)
            {
                return _real.getUserCartDetails();
            }
            else return new Dictionary<string, Dictionary<long, int>>();
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

        public Dictionary<string, Dictionary<long, int>> AddTocart(long prodID, int quantity) //2.6
        {
            if (_real != null)
            {
                return _real.AddTocart(prodID, quantity);
            }
            else return new Dictionary<string, Dictionary<long, int>>();
        }

        public Dictionary<string, Dictionary<long, int>> ViewUserCart() //2.7
        {
            if (_real != null)
            {
                return _real.ViewUserCart();
            }
            else return new Dictionary<string, Dictionary<long, int>>();
        }

        public bool RemoveFromCart(long prodID)
        {
            if (_real != null)
            {
                return _real.RemoveFromCart(prodID);
            }
            else return false;
        }

        public bool ChangeProductCartQuantity(long prodID, int quantity)
        {
            if (_real != null)
            {
                return _real.ChangeProductCartQuantity(prodID, quantity);
            }
            else return false;
        }

        public bool logout() //3.1
        {
            if (_real != null)
            {
                return _real.logout();
            }
            else return true;
        }

        public bool PurchaseProducts(Dictionary<long, int> products, string firstName, string lastName, string id, string creditCardNumber, string creditExpiration, string CVV, string address)
        {
            if (_real != null)
            {
                return _real.PurchaseProducts(products, firstName, lastName, id, creditCardNumber, creditExpiration, CVV, address);
            }
            else return true;
        }

        public List<long> PurchaseHistory()
        {
            if (_real != null)
            {
                return _real.PurchaseHistory();
            }
            else return new List<long>();
        }
    }
}