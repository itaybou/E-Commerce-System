using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.Models;
using ECommerceSystem.Utilities;
using System;
using System.Collections.Generic;

namespace ECommerceSystem.ServiceLayer
{
    public class ServiceFacade : IService
    {
        private UserServices _userServices;
        private SystemServices _systemServices;
        private StoreService _storeServices;

        public ServiceFacade()
        {
            _userServices = new UserServices();
            _systemServices = new SystemServices();
            _storeServices = new StoreService();
        }

        public Guid addProduct(Guid sessionID, string storeName, string productInvName, PurchaseType purchaseType, int quantity, int minQuantity, int maxQuantity)
        {
            return _storeServices.addProduct(sessionID, storeName, productInvName, quantity, minQuantity, maxQuantity);
        }

        public Guid addProductInv(Guid sessionID, string storeName, string description, string productInvName, double price, int quantity, Category category, List<string> keywords, int minQuantity, int maxQuantity)
        {
            return _storeServices.addProductInv(sessionID, storeName, description, productInvName, price, quantity, category, keywords, minQuantity, maxQuantity);
        }

        public bool addProductToCart(Guid sessionID, Guid productId, string storeName, int quantity)
        {
            return _userServices.addProductToCart(sessionID, productId, storeName, quantity);
        }

        public bool assignManager(Guid sessionID, string newManageruserName, string storeName)
        {
            return _storeServices.assignManager(sessionID, newManageruserName, storeName);
        }

        public Guid createOwnerAssignAgreement(Guid sessionID, string newOwneruserName, string storeName)
        {
            return _storeServices.createOwnerAssignAgreement(sessionID, newOwneruserName, storeName);
        }

        public bool approveAssignOwnerRequest(Guid sessionID, Guid agreementID, string storeName)
        {
            return _storeServices.approveAssignOwnerRequest(sessionID, agreementID, storeName);
        }

        public bool disApproveAssignOwnerRequest(Guid sessionID, Guid agreementID, string storeName)
        {
            return _storeServices.disApproveAssignOwnerRequest(sessionID, agreementID, storeName);

        }

        public bool removeOwner(Guid sessionID, string ownerToRemoveUserName, string storeName)
        {
            return _storeServices.removeOwner(sessionID, ownerToRemoveUserName, storeName);
        }

        public bool ChangeProductQunatity(Guid productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public bool deleteProduct(string storeName, string productInvName, Guid productID)
        {
            throw new NotImplementedException();
        }

        public bool deleteProductInv(string storeName, string productInvName)
        {
            throw new NotImplementedException();
        }

        public bool editPermissions(Guid sessionID, string storeName, string managerUserName, List<PermissionType> permissions)
        {
            return _storeServices.editPermissions(sessionID, storeName, managerUserName, permissions);
        }

        public SearchResultModel getAllProducts(string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter)
        {
            return _systemServices.getAllProducts(category, priceFilter, storeRatingFilter, productRatingFilter);
        }

        public Dictionary<StoreModel, List<ProductInventoryModel>> getAllStoresInfo()
        {
            return _storeServices.getAllStoresInfo();
        }

        public Tuple<StoreModel, List<ProductInventoryModel>> getStoreInfo(string storeName)
        {
            return _storeServices.getStoreInfo(storeName);
        }

        public IDictionary<string, PermissionModel> getUserPermissions(Guid sessionId)
        {
            return _storeServices.getUserPermissions(sessionId);
        }

        public bool isUserLogged(string username)
        {
            throw new NotImplementedException();
        }

        public bool isUserSubscribed(string username)
        {
            throw new NotImplementedException();
        }

        public (bool, Guid) login(Guid sessionID, string uname, string pswd)
        {
            return _userServices.login(sessionID, uname, pswd);
        }

        public bool logout(Guid userId)
        {
            return _userServices.logout(userId);
        }

        public bool modifyProductDiscountType(Guid sessionID, string storeName, string productInvName, Guid productID)
        {
            throw new NotImplementedException();
        }

        public bool modifyProductName(Guid sessionID, string storeName, string newProductName, string oldProductName)
        {
            return _storeServices.modifyProductName(sessionID, storeName, newProductName, oldProductName);
        }

        public bool modifyProductPrice(Guid sessionID, string storeName, string productInvName, int newPrice)
        {
            return _storeServices.modifyProductPrice(sessionID, storeName, productInvName, newPrice);
        }

        public bool modifyProductPurchaseType(string storeName, string productInvName, Guid productID, PurchaseType purchaseType)
        {
            throw new NotImplementedException();
        }

        public bool modifyProductQuantity(string storeName, string productInvName, Guid productID, int newQuantity)
        {
            throw new NotImplementedException();
        }

        public bool openStore(Guid sessionID, string name)
        {
            return _storeServices.openStore(sessionID, name);
        }

        public IEnumerable<StorePurchaseModel> purchaseHistory(string storeName)
        {
            throw new NotImplementedException();
        }

        public ICollection<ProductModel> purchaseUserShoppingCart(Guid sessionID, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            throw new NotImplementedException();
        }

        public (bool, string) register(string uname, string pswd, string fname, string lname, string email)
        {
            return _userServices.register(uname, pswd, fname, lname, email);
        }

        public void removeAllUsers()
        {
            throw new NotImplementedException();
        }

        public bool RemoveFromCart(Guid productId)
        {
            throw new NotImplementedException();
        }

        public bool removeManager(Guid sessionID, string managerUserName, string storeName)
        {
            return _storeServices.removeManager(sessionID, managerUserName, storeName);
        }

        public SearchResultModel searchProductsByCategory(string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter)
        {
            return _systemServices.searchProductsByCategory(category, priceFilter, storeRatingFilter, productRatingFilter);
        }

        public SearchResultModel searchProductsByKeyword(List<string> keywords, string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter)
        {
            return _systemServices.searchProductsByKeyword(keywords, category, priceFilter, storeRatingFilter, productRatingFilter);
        }

        public SearchResultModel searchProductsByName(string prodName, string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter)
        {
            return _systemServices.searchProductsByName(prodName, category, priceFilter, storeRatingFilter, productRatingFilter);
        }

        public ShoppingCartModel ShoppingCartDetails(Guid sessionID)
        {
            return _userServices.ShoppingCartDetails(sessionID);
        }

        public ICollection<UserPurchaseModel> userPurchaseHistory(string userName)
        {
            throw new NotImplementedException();
        }

        public UserModel userDetails(Guid sessionID)
        {
            return _userServices.userDetails(sessionID);
        }

        public bool isUserAdmin(Guid sessionID)
        {
            return _userServices.isUserAdmin(sessionID);
        }

        public IEnumerable<UserModel> allUsers(Guid sessionID)
        {
            return _userServices.allUsers(sessionID);
        }

        public (IEnumerable<(UserModel, PermissionModel)>, string) getStoreOwners(string storeName)
        {
            return _storeServices.getStoreOwners(storeName);
        }

        public (IEnumerable<(UserModel, PermissionModel)>, string) getStoreManagers(string storeName)
        {
            return _storeServices.getStoreManagers(storeName);
        }

        public IEnumerable<UserModel> searchUsers(string username)
        {
            return _userServices.searchUsers(username);
        }

        public IDictionary<PermissionType, bool> getUsernamePermissionTypes(string storeName, string username)
        {
            return _storeServices.getUsernamePermissionTypes(storeName, username);
        }

        public (ProductInventoryModel, string) getProductInventory(Guid prodID)
        {
            return _storeServices.getProductInventory(prodID);
        }

        public Tuple<StoreModel, List<ProductModel>> getStoreProductGroup(Guid sessionID, Guid productInvID, string storeName)
        {
            return _storeServices.getStoreProductGroup(sessionID, productInvID, storeName);
        }

        public void rateProduct(Guid prodID, int rating)
        {
            _storeServices.rateProduct(prodID, rating);
        }

        public void rateStore(string storeName, int rating)
        {
            _storeServices.rateStore(storeName, rating);
        }
    }
}