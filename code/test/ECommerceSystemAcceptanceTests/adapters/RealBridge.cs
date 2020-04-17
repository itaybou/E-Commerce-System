using System;
using System.Collections.Generic;
using System.Text;
using ECommerceSystem.ServiceLayer;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;

namespace ECommerceSystemAcceptanceTests.adapters
{
    class RealBridge : IBridgeAdapter
    {
        UserServices _userServices;
        StoreService _storeService;
        
        public RealBridge()
        {
            _userServices = new UserServices();
            _storeService = new StoreService();
        }

        public long addProduct(string storeName, string productInvName, string discountType, int discountPercentage, string purchaseType, int quantity)
        {
            Discount discount = null;
            PurchaseType purchase = null;
            if (discountType.Equals("visible"))
            {
                discount = new VisibleDiscount(discountPercentage, new DiscountPolicy());
            }
            else
            {
                return -1;
            }
            if (purchaseType.Equals("immediate"))
            {
                purchase = new ImmediatePurchase();
            }
            else
            {
                return -1;
            }

            return _storeService.addProduct(storeName, productInvName, discount, purchase, quantity);

        }

        public long addProductInv(string storeName, string description, string productName, string discountType, int discountPercentage, string purchaseType, double price, int quantity, Category category, List <string> keys)
        {
            Discount discount = null;
            PurchaseType purchase = null;
            if (discountType.Equals("visible"))
            {
                discount = new VisibleDiscount(discountPercentage, new DiscountPolicy());
            }
            else
            {
                return -1;
            }
            if (purchaseType.Equals("immediate"))
            {
                purchase = new ImmediatePurchase();
            }
            else
            {
                return -1;
            }

            return _storeService.addProductInv(storeName, description, productName, discount, purchase, price, quantity, category, keywords);
        }

        public bool assignManager(string newManageruserName, string storeName)
        {
            return _storeService.assignManager(newManageruserName, storeName);
        }

        public bool assignOwner(string newOwneruserName, string storeName)
        {
            return _storeService.assignOwner(newOwneruserName, storeName);
        }

        public bool deleteProduct(string storeName, string productInvName, long productID)
        {
            return _storeService.deleteProduct(storeName, productInvName, productID);
        }

        public bool deleteProductInv(string storeName, string productName)
        {
            return _storeService.deleteProductInv(storeName, productName);
        }

        public bool editPermissions(string storeName, string managerUserName, List<permissionType> permissions)
        {
            return _storeService.editPermissions(storeName, managerUserName, permissions);
        }

        public bool modifyProductDiscountType(string storeName, string productInvName, long productID, string newDiscount, int discountPercentage)
        {
            Discount discount = null;
            if (newDiscount.Equals("visible"))
            {
                discount = new VisibleDiscount(discountPercentage, new DiscountPolicy());
            }
            else
            {
                return false;
            }

            return _storeService.modifyProductDiscountType(storeName, productInvName, productID, discount);
        }

        public bool modifyProductName(string storeName, string newProductName, string oldProductName)
        {
            return _storeService.modifyProductName(storeName, newProductName, oldProductName);
        }

        public bool modifyProductPrice(string storeName, string productInvName, int newPrice)
        {
            return _storeService.modifyProductPrice(storeName, productInvName, newPrice);
        }

        public bool modifyProductPurchaseType(string storeName, string productInvName, long productID, string purchaseType)
        {
            PurchaseType newPurchase = null;
            if (purchaseType.Equals("immediate"))
            {
                newPurchase = new ImmediatePurchase();
            }
            else
            {
                return false;
            }

            return _storeService.modifyProductPurchaseType(storeName, productInvName, productID, newPurchase);
        }

        public bool modifyProductQuantity(string storeName, string productInvName, long productID, int newQuantity)
        {
            return _storeService.modifyProductQuantity(storeName, productInvName, productID, newQuantity);

        }

        public bool openStore(string name, string discountPolicy, string purchasePolicy)
        {
            DiscountPolicy discount = new DiscountPolicy();
            PurchasePolicy purchase = new PurchasePolicy();

            return _storeService.openStore(name, discount, purchase);
        }

        public List<StorePurchase> purchaseHistory(string storeName)
        {
            return _storeService.purchaseHistory(storeName);
        }

        public bool register(string uname, string pswd, string fname, string lname, string email)
        {
            return _userServices.register(uname, pswd, fname, lname, email);
        }

        public bool removeManager(string managerUserName, string storeName)
        {
            return _storeService.removeManager(managerUserName, storeName);
        }
    }
}