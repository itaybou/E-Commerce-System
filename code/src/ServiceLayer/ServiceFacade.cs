using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.Exceptions;
using ECommerceSystem.Models;
using ECommerceSystem.Models.DiscountPolicyModels;
using ECommerceSystem.Models.PurchasePolicyModels;
using ECommerceSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public Guid addProduct(Guid sessionID, string storeName, string productInvName, int quantity, int minQuantity, int maxQuantity)
        {
            return _storeServices.addProduct(sessionID, storeName, productInvName, quantity, minQuantity, maxQuantity);
        }

        public Guid addProductInv(Guid sessionID, string storeName, string description, string productInvName, double price, int quantity, Category category, List<string> keywords, int minQuantity, int maxQuantity, string imageUrl)
        {
            return _storeServices.addProductInv(sessionID, storeName, description, productInvName, price, quantity, category, keywords, minQuantity, maxQuantity, imageUrl);
        }

        public IEnumerable<INotificationRequest> GetAwaitingRequests(Guid sessionID)
        {
            return _userServices.GetAwaitingRequests(sessionID);
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

        public bool ChangeProductQunatity(Guid sessionID, Guid productId, int quantity)
        {
            return _userServices.ChangeProductQunatity(sessionID, productId, quantity);
        }

        public bool deleteProduct(Guid sessionID, string storeName, string productInvName, Guid productID)
        {
            return _storeServices.deleteProductInv(sessionID, storeName, productInvName);
        }

        public bool deleteProductInv(Guid sessionID, string storeName, string productInvName)
        {
            return _storeServices.deleteProductInv(sessionID, storeName, productInvName);
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

        public bool modifyProductName(Guid sessionID, string storeName, string newProductName, string oldProductName)
        {
            return _storeServices.modifyProductName(sessionID, storeName, newProductName, oldProductName);
        }

        public bool modifyProductPrice(Guid sessionID, string storeName, string productInvName, int newPrice)
        {
            return _storeServices.modifyProductPrice(sessionID, storeName, productInvName, newPrice);
        }

        public bool modifyProductQuantity(Guid sessionID, string storeName, string productInvName, Guid productID, int newQuantity)
        {
            return _storeServices.modifyProductQuantity(sessionID, storeName, productInvName, productID, newQuantity);
        }

        public bool openStore(Guid sessionID, string name)
        {
            return _storeServices.openStore(sessionID, name);
        }

        public IEnumerable<StorePurchaseModel> purchaseHistory(Guid sessionID, string storeName)
        {
            return _storeServices.purchaseHistory(sessionID, storeName);
        }

        public async Task<ICollection<ProductModel>> purchaseUserShoppingCart(Guid sessionID, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            return await _systemServices.purchaseUserShoppingCart(sessionID, firstName, lastName, id, creditCardNumber, expirationCreditCard, CVV, address);
        }

        public (bool, string) register(string uname, string pswd, string fname, string lname, string email)
        {
            return _userServices.register(uname, pswd, fname, lname, email);
        }

        public void removeAllUsers()
        {
            throw new NotImplementedException();
        }

        public bool RemoveFromCart(Guid sessionID, Guid productId)
        {
            return _userServices.RemoveFromCart(sessionID, productId);
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

        public ICollection<UserPurchaseModel> userPurchaseHistory(Guid sessionID, string userName)
        {
            return _userServices.userPurchaseHistory(sessionID, userName);
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

        public (IEnumerable<(UserModel, PermissionModel)>, string) getStoreOwners(Guid sessionID, string storeName)
        {
            return _storeServices.getStoreOwners(sessionID, storeName);
        }

        public (IEnumerable<(UserModel, PermissionModel)>, string) getStoreManagers(Guid sessionID, string storeName)
        {
            return _storeServices.getStoreManagers(sessionID, storeName);
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

        public List<DiscountPolicyModel> getAllStoreLevelDiscounts(Guid sessionID, string storeName)
        {
            return _storeServices.getAllStoreLevelDiscounts(sessionID, storeName);
        }

        public Guid addConditionalStoreDiscount(Guid sessionID, string storeName, float percentage, DateTime expDate, int minPriceForDiscount)
        {
            return _storeServices.addConditionalStoreDiscount(sessionID, storeName, percentage, expDate, minPriceForDiscount);
        }

        public Guid addCondiotionalProcuctDiscount(Guid sessionID, string storeName, Guid productID, float percentage, DateTime expDate, int minQuantityForDiscount)
        {
            return _storeServices.addCondiotionalProcuctDiscount(sessionID, storeName, productID, percentage, expDate, minQuantityForDiscount);
        }

        public Guid addVisibleDiscount(Guid sessionID, string storeName, Guid productID, float percentage, DateTime expDate)
        {
            return _storeServices.addVisibleDiscount(sessionID, storeName, productID, percentage, expDate);
        }

        public List<DiscountPolicyModel> getAllDiscountsForCompose(Guid sessionID, string storeName)
        {
            return _storeServices.getAllDiscountsForCompose(sessionID, storeName);
        }

        public Guid addAndDiscountPolicy(Guid sessionID, string storeName, List<Guid> IDs)
        {
            return _storeServices.addAndDiscountPolicy(sessionID, storeName, IDs);
        }

        public Guid addOrDiscountPolicy(Guid sessionID, string storeName, List<Guid> IDs)
        {
            return _storeServices.addOrDiscountPolicy(sessionID, storeName, IDs);
        }

        public Guid addXorDiscountPolicy(Guid sessionID, string storeName, List<Guid> IDs)
        {
            return _storeServices.addXorDiscountPolicy(sessionID, storeName, IDs);
        }

        public List<PurchasePolicyModel> getAllPurchasePolicyByStoreName(Guid sessionID, string storeName)
        {
            return _storeServices.getAllPurchasePolicyByStoreName(sessionID, storeName);
        }

        public Guid addDayOffPolicy(Guid sessionID, string storeName, List<DayOfWeek> daysOff)
        {
            return _storeServices.addDayOffPolicy(sessionID, storeName, daysOff);
        }

        public Guid addLocationPolicy(Guid sessionID, string storeName, List<string> banLocations)
        {
            return _storeServices.addLocationPolicy(sessionID, storeName, banLocations);
        }

        public Guid addMinPriceStorePolicy(Guid sessionID, string storeName, double minPrice)
        {
            return _storeServices.addMinPriceStorePolicy(sessionID, storeName, minPrice);
        }

        public Guid addAndPurchasePolicy(Guid sessionID, string storeName, Guid ID1, Guid ID2)
        {
            return _storeServices.addAndPurchasePolicy(sessionID, storeName, ID1, ID2);
        }

        public Guid addOrPurchasePolicy(Guid sessionID, string storeName, Guid ID1, Guid ID2)
        {
            return _storeServices.addOrPurchasePolicy(sessionID, storeName, ID1, ID2);
        }
        public Guid addXorPurchasePolicy(Guid sessionID, string storeName, Guid ID1, Guid ID2)
        {
            return _storeServices.addXorPurchasePolicy(sessionID, storeName, ID1, ID2);
        }
    }
}