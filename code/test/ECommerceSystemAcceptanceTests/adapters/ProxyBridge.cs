﻿using System;
using System.Collections.Generic;

namespace ECommerceSystemAcceptanceTests.adapters
{
    internal class ProxyBridge : IBridgeAdapter
    {
        private IBridgeAdapter _real;

        internal IBridgeAdapter RealBridge { get => _real; set => _real = value; }

        public Guid addProduct(Guid userID, string storeName, string productInvName, string discountType, int discountPercentage, string purchaseType, int quantity)
        {
            if (_real != null)
            {
                return _real.addProduct(userID, storeName, productInvName, discountType, discountPercentage, purchaseType, quantity);
            }
            else return Guid.NewGuid();
        }

        public Guid addProductInv(Guid userID, string storeName, string productName, string description, string discountType, int discountPercentage, string purchaseType, double price, int quantity, string category, List<string> keys)
        {
            if (_real != null)
            {
                return _real.addProductInv(userID, storeName, productName, description, discountType, discountPercentage, purchaseType, price, quantity, category, keys);
            }
            else return Guid.NewGuid();
        }

        public bool assignManager(Guid userID, string newManageruserName, string storeName)
        {
            if (_real != null)
            {
                return _real.assignManager(userID, newManageruserName, storeName);
            }
            else return false;
        }

        public bool assignOwner(Guid userID, string newOwneruserName, string storeName)
        {
            if (_real != null)
            {
                return _real.assignOwner(userID, newOwneruserName, storeName);
            }
            else return false;
        }

        public bool deleteProduct(Guid userID, string storeName, string productInvName, Guid productID)
        {
            if (_real != null)
            {
                return _real.deleteProduct(userID, storeName, productInvName, productID);
            }
            else return true;
        }

        public bool deleteProductInv(Guid userID, string storeName, string productName)
        {
            if (_real != null)
            {
                return _real.deleteProductInv(userID, storeName, productName);
            }
            else return true;
        }

        public bool editPermissions(Guid userID, string storeName, string managerUserName, List<PermissionType> permissions)
        {
            if (_real != null)
            {
                return _real.editPermissions(userID, storeName, managerUserName, permissions);
            }
            else return false;
        }

        public bool modifyProductDiscountType(Guid userID, string storeName, string productInvName, Guid productID, string newDiscount, int discountPercentage)
        {
            if (_real != null)
            {
                return _real.modifyProductDiscountType(userID, storeName, productInvName, productID, newDiscount, discountPercentage);
            }
            else return true;
        }

        public bool modifyProductName(Guid userID, string storeName, string newProductName, string oldProductName)
        {
            if (_real != null)
            {
                return _real.modifyProductName(userID, storeName, newProductName, oldProductName);
            }
            else return true;
        }

        public bool modifyProductPrice(Guid userID, string storeName, string productInvName, int newPrice)
        {
            if (_real != null)
            {
                return _real.modifyProductPrice(userID, storeName, productInvName, newPrice);
            }
            else return true;
        }

        public bool modifyProductPurchaseType(Guid userID, string storeName, string productInvName, Guid productID, string purchaseType)
        {
            if (_real != null)
            {
                return _real.modifyProductPurchaseType(userID, storeName, productInvName, productID, purchaseType);
            }
            else return true;
        }

        public bool modifyProductQuantity(Guid userID, string storeName, string productInvName, Guid productID, int newQuantity)
        {
            if (_real != null)
            {
                return _real.modifyProductQuantity(userID, storeName, productInvName, productID, newQuantity);
            }
            else return true;
        }

        public bool openStore(Guid userID, string name, string discountPolicy, string purchasePolicy)
        {
            if (_real != null)
            {
                return _real.openStore(userID, name, discountPolicy, purchasePolicy);
            }
            else return true;
        }

        public List<Tuple<string, List<Tuple<Guid, int>>, double>> StorePurchaseHistory(Guid userID, string storeName)
        {
            if (_real != null)
            {
                return _real.StorePurchaseHistory(userID, storeName);
            }
            else return null;
        }

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

        public void openStoreWithProducts(Guid userID, string storeName, string ownerName, List<string> products)
        {
            if (_real != null)
            {
                _real.openStoreWithProducts(userID, storeName, ownerName, products);
            }
        }

        public Dictionary<string, Dictionary<Guid, int>> getUserCartDetails()
        {
            if (_real != null)
            {
                return _real.getUserCartDetails();
            }
            else return new Dictionary<string, Dictionary<Guid, int>>();
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

        public bool removeManager(Guid userID, string managerUserName, string storeName)
        {
            if (_real != null)
            {
                return _real.removeManager(userID, managerUserName, storeName);
            }
            else return false;
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

        public Dictionary<string, Dictionary<Guid, int>> AddTocart(Guid prodID, int quantity) //2.6
        {
            if (_real != null)
            {
                return _real.AddTocart(prodID, quantity);
            }
            else return new Dictionary<string, Dictionary<Guid, int>>();
        }

        public Dictionary<string, Dictionary<Guid, int>> ViewUserCart() //2.7
        {
            if (_real != null)
            {
                return _real.ViewUserCart();
            }
            else return new Dictionary<string, Dictionary<Guid, int>>();
        }

        public bool RemoveFromCart(Guid prodID)
        {
            if (_real != null)
            {
                return _real.RemoveFromCart(prodID);
            }
            else return false;
        }

        public bool ChangeProductCartQuantity(Guid prodID, int quantity)
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

        public bool PurchaseProducts(Dictionary<Guid, int> products, string firstName, string lastName, string id, string creditCardNumber, string creditExpiration, string CVV, string address)
        {
            if (_real != null)
            {
                return _real.PurchaseProducts(products, firstName, lastName, id, creditCardNumber, creditExpiration, CVV, address);
            }
            else return true;
        }

        public List<Guid> UserPurchaseHistory(string uname)
        {
            if (_real != null)
            {
                return _real.UserPurchaseHistory(uname);
            }
            else return new List<Guid>();
        }
    }
}