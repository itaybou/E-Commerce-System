using System;
using System.Collections.Generic;
using System.Text;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;

namespace ECommerceSystemAcceptanceTests.adapters
{
    class ProxyBridge : IBridgeAdapter
    {
        private IBridgeAdapter real;

        internal IBridgeAdapter RealBridge { get => real; set => real = value; }

        public long addProduct(string storeName, string productInvName, string discountType, int discountPercentage, string purchaseType, int quantity)
        {
            if (real != null)
            {
                return real.addProduct(storeName, productInvName, discountType, discountPercentage, purchaseType, quantity);
            }
            else return 1;
        }

        public long addProductInv(string storeName, string productName, string description, string discountType, int discountPercentage, string purchaseType, double price, int quantity, Category category, List<string> keys)
        {
            if (real != null)
            {
                return real.addProductInv(storeName, productName, description, discountType, discountPercentage, purchaseType, price, quantity, category, keys);
            }
            else return 1;
        }

        public bool assignManager(string newManageruserName, string storeName)
        {
            if (real != null)
            {
                return real.assignManager(newManageruserName, storeName);
            }
            else return false;
        }

        public bool assignOwner(string newOwneruserName, string storeName)
        {
            if (real != null)
            {
                return real.assignOwner(newOwneruserName, storeName);
            }
            else return false;
        }

        public bool deleteProduct(string storeName, string productInvName, long productID)
        {
            if (real != null)
            {
                return real.deleteProduct(storeName, productInvName, productID);
            }
            else return true;
        }

        public bool deleteProductInv(string storeName, string productName)
        {
            if (real != null)
            {
                return real.deleteProductInv(storeName, productName);
            }
            else return true;
        }

        public bool editPermissions(string storeName, string managerUserName, List<permissionType> permissions)
        {
            if (real != null)
            {
                return real.editPermissions(storeName, managerUserName, permissions);
            }
            else return false;
        }

        public bool modifyProductDiscountType(string storeName, string productInvName, long productID, string newDiscount, int discountPercentage)
        {
            if (real != null)
            {
                return real.modifyProductDiscountType(storeName, productInvName, productID, newDiscount, discountPercentage);
            }
            else return true;
        }

        public bool modifyProductName(string storeName, string newProductName, string oldProductName)
        {
            if (real != null)
            {
                return real.modifyProductName(storeName, newProductName, oldProductName);
            }
            else return true;
        }

        public bool modifyProductPrice(string storeName, string productInvName, int newPrice)
        {
            if (real != null)
            {
                return real.modifyProductPrice(storeName, productInvName, newPrice);
            }
            else return true;
        }

        public bool modifyProductPurchaseType(string storeName, string productInvName, long productID, string purchaseType)
        {
            if (real != null)
            {
                return real.modifyProductPurchaseType(storeName, productInvName, productID, purchaseType);
            }
            else return true;
        }

        public bool modifyProductQuantity(string storeName, string productInvName, long productID, int newQuantity)
        {
            if (real != null)
            {
                return real.modifyProductQuantity(storeName, productInvName, productID, newQuantity);
            }
            else return true;
        }

        public bool openStore(string name, string discountPolicy, string purchasePolicy)
        {
            if (real != null)
            {
                return real.openStore(name, discountPolicy, purchasePolicy);
            }
            else return true;
        }

        public List<StorePurchase> purchaseHistory(string storeName)
        {
            if (real != null)
            {
                return real.purchaseHistory(storeName);
            }
            else return null;
        }

        public bool register(string uname, string pswd, string fname, string lname, string email)
        {
            if (real != null)
            {
                return real.register(uname, pswd, fname, lname, email);
            }
            else return true;
        }

        public bool removeManager(string managerUserName, string storeName)
        {
            if (real != null)
            {
                return real.removeManager(managerUserName, storeName);
            }
            else return false;
        }
    }
}