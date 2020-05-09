using ECommerceSystem.DomainLayer.StoresManagement;
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
        public bool openStore(Guid sessionID, string name, DiscountPolicy discountPolicy, PurchasePolicy purchasePolicy) // TODO: fix service types version 1
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.openStore(userID, name, discountPolicy, purchasePolicy);
        }

        [Trace("Info")]
        //Usecase - 4.1.1
        public Guid addProductInv(Guid sessionID, string storeName, string description, string productInvName, Discount discount, PurchaseType purchaseType, double price, int quantity, string category, List<string> keywords) // TODO: fix service types version 1
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.addProductInv(userID, storeName, description, productInvName, discount, purchaseType, price, quantity, category, keywords);
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
        public Guid addProduct(Guid sessionID, string storeName, string productInvName, Discount discount, PurchaseType purchaseType, int quantity) // TODO: fix service types version 1
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.addProduct(userID, storeName, productInvName, discount, purchaseType, quantity);
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
        public bool modifyProductDiscountType(Guid sessionID, string storeName, string productInvName, Guid productID, Discount newDiscount) // TODO: fix service types version 1
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
        public bool editPermissions(Guid sessionID, string storeName, string managerUserName, List<string> permissions)
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

        [Trace("Info")]
        public IDictionary<string, PermissionModel> getUserPermissions(Guid sessionID)
        {
            var userID = _sessions.ResolveSession(sessionID);
            return _storeManagement.getUserPermissions(userID);
        }
    }
}