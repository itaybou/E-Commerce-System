﻿using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement.logger;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Models;
using System;
using System.Collections.Generic;

namespace ECommerceSystem.ServiceLayer
{
    public class StoreService
    {
        private StoreManagement _storeManagement;

        public StoreService()
        {
            _storeManagement = StoreManagement.Instance;
        }

        public void removeAllStores()
        {
            _storeManagement.Stores.Clear();
        }

        [Trace("Info")]
        //Usecase - 2.4
        public Tuple<StoreModel, List<ProductModel>> getStoreInfo(string storeName)
        {
            return _storeManagement.getStoreProducts(storeName);
        }

        [Trace("Info")]
        public Dictionary<StoreModel, List<ProductModel>> getAllStoresInfo()
        {
            return _storeManagement.getAllStoresProducts();
        }

        [Trace("Info")]
        //Usecase - 3.2
        public bool openStore(string name, DiscountPolicy discountPolicy, PurchasePolicy purchasePolicy) // TODO: fix service types version 1
        {
            return _storeManagement.openStore(name, discountPolicy, purchasePolicy);
        }

        [Trace("Info")]
        //Usecase - 4.1.1
        public Guid addProductInv(string storeName, string description, string productInvName, Discount discount, PurchaseType purchaseType, double price, int quantity, string category, List<string> keywords) // TODO: fix service types version 1
        {
            return _storeManagement.addProductInv(storeName, description, productInvName, discount, purchaseType, price, quantity, category, keywords);
        }

        [Trace("Info")]
        //Usecase - 4.1.2
        public bool deleteProductInv(string storeName, string productInvName)
        {
            return _storeManagement.deleteProductInventory(storeName, productInvName);
        }

        [Trace("Info")]
        //Usecase - 4.1.3
        public Guid addProduct(string storeName, string productInvName, Discount discount, PurchaseType purchaseType, int quantity) // TODO: fix service types version 1
        {
            return _storeManagement.addProduct(storeName, productInvName, discount, purchaseType, quantity);
        }

        [Trace("Info")]
        public bool deleteProduct(string storeName, string productInvName, Guid productID)
        {
            return _storeManagement.deleteProduct(storeName, productInvName, productID);
        }

        [Trace("Info")]
        public bool modifyProductName(string storeName, string newProductName, string oldProductName)
        {
            return _storeManagement.modifyProductName(storeName, newProductName, oldProductName);
        }

        [Trace("Info")]
        public bool modifyProductPrice(string storeName, string productInvName, int newPrice)
        {
            return _storeManagement.modifyProductPrice(storeName, productInvName, newPrice);
        }

        [Trace("Info")]
        public bool modifyProductQuantity(string storeName, string productInvName, Guid productID, int newQuantity)
        {
            return _storeManagement.modifyProductQuantity(storeName, productInvName, productID, newQuantity);
        }

        [Trace("Info")]
        public bool modifyProductDiscountType(string storeName, string productInvName, Guid productID, Discount newDiscount) // TODO: fix service types version 1
        {
            return _storeManagement.modifyProductDiscountType(storeName, productInvName, productID, newDiscount);
        }

        [Trace("Info")]
        public bool modifyProductPurchaseType(string storeName, string productInvName, Guid productID, PurchaseType purchaseType) // TODO: fix service types version 1
        {
            return _storeManagement.modifyProductPurchaseType(storeName, productInvName, productID, purchaseType);
        }

        [Trace("Info")]
        //Usecase - 4.3
        public bool assignOwner(string newOwneruserName, string storeName) // TODO: fix service types version 1
        {
            return _storeManagement.assignOwner(newOwneruserName, storeName);
        }

        [Trace("Info")]
        //Usecase - 4.5
        public bool assignManager(string newManageruserName, string storeName) // TODO: fix service types version 1
        {
            return _storeManagement.assignManager(newManageruserName, storeName);
        }

        [Trace("Info")]
        //Usecase - 4.6
        public bool editPermissions(string storeName, string managerUserName, List<string> permissions)
        {
            return _storeManagement.editPermissions(storeName, managerUserName, permissions);
        }

        [Trace("Info")]
        //Usecase - 4.7
        public bool removeManager(string managerUserName, string storeName) // TODO: fix service types version 1
        {
            return _storeManagement.removeManager(managerUserName, storeName);
        }

        [Trace("Info")]
        //Usecase - 4.10
        //Usecase - 6.4.2
        public ICollection<StorePurchaseModel> purchaseHistory(string storeName)
        {
            return _storeManagement.purchaseHistory(storeName);
        }
    }
}