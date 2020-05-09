﻿using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.StoresManagement.Discount;
using ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies;
using ECommerceSystem.DomainLayer.SystemManagement.logger;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Models;
using ECommerceSystem.CommunicationLayer.sessions;
using System;
using System.Collections.Generic;

namespace ECommerceSystem.ServiceLayer
{
    public class StoreService
    {
        private StoreManagement _storeManagement;
        private ISessionController _sessions;

        public StoreService()
        {
            _storeManagement = StoreManagement.Instance;
            _sessions = SessionController.Instance;
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
        public bool openStore(Guid sessionID, string name) 
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.openStore(userID, name);
        }

        [Trace("Info")]
        //Usecase - 4.1.1
        public Guid addProductInv(Guid sessionID, string storeName, string description, string productInvName, PurchaseType purchaseType, double price, int quantity, Category category, List<string> keywords, int minQuantity, int maxQuantity) // -1 if not needed in both
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.addProductInv(userID, storeName, description, productInvName, purchaseType, price, quantity, category, keywords, minQuantity, maxQuantity);
        }

        [Trace("Info")]
        //Usecase - 4.1.2
        public bool deleteProductInv(Guid sessionID, string storeName, string productInvName)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.deleteProductInventory(userID, storeName, productInvName);
        }

        [Trace("Info")]
        //Usecase - 4.1.3
        public Guid addProduct(Guid sessionID, string storeName, string productInvName, PurchaseType purchaseType, int quantity, int minQuantity, int maxQuantity)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.addProduct(userID, storeName, productInvName, purchaseType, quantity, minQuantity, maxQuantity);
        }

        [Trace("Info")]
        public bool deleteProduct(Guid sessionID, string storeName, string productInvName, Guid productID)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.deleteProduct(userID, storeName, productInvName, productID);
        }

        [Trace("Info")]
        public bool modifyProductName(Guid sessionID, string storeName, string newProductName, string oldProductName)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.modifyProductName(userID, storeName, newProductName, oldProductName);
        }

        [Trace("Info")]
        public bool modifyProductPrice(Guid sessionID, string storeName, string productInvName, int newPrice)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.modifyProductPrice(userID, storeName, productInvName, newPrice);
        }

        [Trace("Info")]
        public bool modifyProductQuantity(Guid sessionID, string storeName, string productInvName, Guid productID, int newQuantity)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.modifyProductQuantity(userID, storeName, productInvName, productID, newQuantity);
        }

        [Trace("Info")]
        public bool modifyProductDiscountType(Guid sessionID, string storeName, string productInvName, Guid productID, DiscountType newDiscount) // TODO: fix service types version 1
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.modifyProductDiscountType(userID, storeName, productInvName, productID, newDiscount);
        }

        [Trace("Info")]
        public bool modifyProductPurchaseType(Guid sessionID, string storeName, string productInvName, Guid productID, PurchaseType purchaseType) // TODO: fix service types version 1
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.modifyProductPurchaseType(userID, storeName, productInvName, productID, purchaseType);
        }

        [Trace("Info")]
        //Usecase - 4.3
        public bool assignOwner(Guid sessionID, string newOwneruserName, string storeName) // TODO: fix service types version 1
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.assignOwner(userID, newOwneruserName, storeName);
        }

        [Trace("Info")]
        //Usecase - 4.5
        public bool assignManager(Guid sessionID, string newManageruserName, string storeName) // TODO: fix service types version 1
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.assignManager(userID, newManageruserName, storeName);
        }

        [Trace("Info")]
        //Usecase - 4.6
        public bool editPermissions(Guid sessionID, string storeName, string managerUserName, List<PermissionType> permissions)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.editPermissions(userID, storeName, managerUserName, permissions);
        }

        [Trace("Info")]
        //Usecase - 4.7
        public bool removeManager(Guid sessionID, string managerUserName, string storeName) // TODO: fix service types version 1
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.removeManager(userID, managerUserName, storeName);
        }

        [Trace("Info")]
        //Usecase - 4.10
        //Usecase - 6.4.2
        public IEnumerable<StorePurchaseModel> purchaseHistory(Guid sessionID, string storeName)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.purchaseHistory(userID, storeName);
        }

        //*********Manage Purchase Policy  --   REQUIREMENT 4.2*********

        //*********ADD*********

        public Guid addDayOffPolicy(Guid userID, string storeName, List <DayOfWeek> daysOff)
        {
            return _storeManagement.AddDayOffPolicy(userID, storeName, daysOff);
        }

        public Guid addLocationPolicy(Guid userID, string storeName, List<string> banLocations)
        {
            return _storeManagement.addLocationPolicy(userID, storeName, banLocations);
        }

        public Guid addMinPriceStorePolicy(Guid userID, string storeName, double minPrice)
        {
            return _storeManagement.addMinPriceStorePolicy(userID, storeName, minPrice);
        }

        public Guid addAndPurchasePolicy(Guid userID, string storeName, Guid ID1, Guid ID2)
        {
            return _storeManagement.addAndPurchasePolicy(userID, storeName, ID1, ID2);
        }

        public Guid addOrPurchasePolicy(Guid userID, string storeName, Guid ID1, Guid ID2)
        {
            return _storeManagement.addOrPurchasePolicy(userID, storeName, ID1, ID2);
        }

        public Guid addXorPurchasePolicy(Guid userID, string storeName, Guid ID1, Guid ID2)
        {
            return _storeManagement.addXorPurchasePolicy(userID, storeName, ID1, ID2);
        }

        //*********REMOVE*********

        public bool removePurchasePolicy(Guid userID, string storeName, Guid policyID)
        {
            return _storeManagement.removePurchasePolicy(userID, storeName, policyID);
        }


        //*********Manage Dicsount Policy  --   REQUIREMENT 4.2*********


        //*********ADD*********

        //if the product already have discount, the new discount override the old
        public Guid addVisibleDiscount(Guid userID, string storeName, Guid productID, float percentage, DateTime expDate)
        {
            return _storeManagement.addVisibleDiscount(userID, storeName, productID, percentage, expDate);
        }

        public Guid addCondiotionalProcuctDiscount(Guid userID, string storeName, Guid productID, float percentage, DateTime expDate, int minQuantityForDiscount)
        {
            return _storeManagement.addCondiotionalProcuctDiscount(userID, storeName, productID, percentage, expDate, minQuantityForDiscount);
        }

        public Guid addConditionalStoreDiscount(Guid userID, string storeName, Guid productID, float percentage, DateTime expDate, int minPriceForDiscount)
        {
            return _storeManagement.addConditionalStoreDiscount(userID, storeName, percentage, expDate, minPriceForDiscount);
        }

        //cant compose store level discount
        //@pre - IDs doesn`t contain store level discount id
        public Guid addAndDiscountPolicy(Guid userID, string storeName, List<Guid> IDs)
        {
            return _storeManagement.addAndDiscountPolicy(userID, storeName, IDs);
        }

        //cant compose store level discount
        //@pre - IDs doesn`t contain store level discount id
        public Guid addOrDiscountPolicy(Guid userID, string storeName, List<Guid> IDs)
        {
            return _storeManagement.addOrDiscountPolicy(userID, storeName, IDs);
        }

        //cant compose store level discount
        //@pre - IDs doesn`t contain store level discount id
        public Guid addXorDiscountPolicy(Guid userID, string storeName, List<Guid> IDs)
        {
            return _storeManagement.addXorDiscountPolicy(userID, storeName, IDs);
        }

        //*********REMOVE*********
        public bool removeProductDiscount(Guid userID, string storeName, Guid discountID, Guid productID)
        {
            return _storeManagement.removeProductDiscount(userID, storeName, discountID, productID);
        }

        public bool removeCompositeDiscount(Guid userID, string storeName, Guid discountID)
        {
            return _storeManagement.removeCompositeDiscount(userID, storeName, discountID);
        }

        public bool removeStoreLevelDiscount(Guid userID, string storeName, Guid discountID)
        {
            return _storeManagement.removeStoreLevelDiscount(userID, storeName, discountID);
        }


        [Trace("Info")]
        public IDictionary<string, PermissionModel> getUserPermissions(Guid sessionID)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.getUserPermissions(userID);
        }
    }
}