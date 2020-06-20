using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.Models;
using ECommerceSystem.Models.DiscountPolicyModels;
using ECommerceSystem.Models.PurchasePolicyModels;
using ECommerceSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceSystem.ServiceLayer
{
    public interface IService
    {
        // User Services
        bool isUserAdmin(Guid sessionID);

        bool isUserSubscribed(string username);

        bool isUserLogged(string username);

        void removeAllUsers();

        IEnumerable<INotificationRequest> GetAwaitingRequests(Guid sessionID);

        (bool, string) register(string uname, string pswd, string fname, string lname, string email);

        (bool, Guid) login(Guid sessionID, string uname, string pswd);

        bool logout(Guid guid);

        bool addProductToCart(Guid sessionID, Guid productId, string storeName, int quantity);

        ShoppingCartModel ShoppingCartDetails(Guid sessionID);

        bool RemoveFromCart(Guid sessionID, Guid productId);

        bool ChangeProductQunatity(Guid sessionID, Guid productId, int quantity);

        ICollection<UserPurchaseModel> userPurchaseHistory(Guid sessionID, string userName);

        UserModel userDetails(Guid sessionID);

        IEnumerable<UserModel> allUsers(Guid sessionID);

        IEnumerable<UserModel> searchUsers(string username);

        IDictionary<string, PermissionModel> getUserPermissions(Guid sessionID);

        IDictionary<PermissionType, bool> getUsernamePermissionTypes(string storeName, string username);

        void rateProduct(Guid prodID, int rating);

        void rateStore(string storeName, int rating);

        void GuestStatistics(Guid sessionID);

        UserStatistics GetUserStatistics(Guid sessionID, DateTime from, DateTime to);

        // Store Services
        Tuple<StoreModel, List<ProductInventoryModel>> getStoreInfo(string storeName);

        List<DiscountPolicyModel> getAllDiscountsForCompose(Guid sessionID, string storeName);

        Guid addAndDiscountPolicy(Guid sessionID, string storeName, List<Guid> IDs);
        Guid addOrDiscountPolicy(Guid sessionID, string storeName, List<Guid> IDs);
        Guid addXorDiscountPolicy(Guid sessionID, string storeName, List<Guid> IDs);

        bool removeProductDiscount(Guid sessionID, string storeName, Guid discountID, Guid productID);
        bool removeCompositeDiscount(Guid sessionID, string storeName, Guid discountID);
        bool removeStoreLevelDiscount(Guid sessionID, string storeName, Guid discountID);

        Guid addDayOffPolicy(Guid sessionID, string storeName, List<DayOfWeek> daysOff);
        Guid addLocationPolicy(Guid sessionID, string storeName, List<string> banLocations);
        Guid addMinPriceStorePolicy(Guid sessionID, string storeName, double minPrice);
        Guid addAndPurchasePolicy(Guid sessionID, string storeName, Guid ID1, Guid ID2);
        Guid addOrPurchasePolicy(Guid sessionID, string storeName, Guid ID1, Guid ID2);
        Guid addXorPurchasePolicy(Guid sessionID, string storeName, Guid ID1, Guid ID2);

        bool removePurchasePolicy(Guid sessionID, string storeName, Guid policyID);

        Tuple<StoreModel, List<ProductModel>> getStoreProductGroup(Guid sessionID, Guid productInvID, string storeName);

        Dictionary<StoreModel, List<ProductInventoryModel>> getAllStoresInfo();

        bool openStore(Guid sessionID, string name);

        List<DiscountPolicyModel> getAllStoreLevelDiscounts(Guid sessionID, string storeName);

        Guid addConditionalStoreDiscount(Guid sessionID, string storeName, float percentage, DateTime expDate, int minPriceForDiscount);

        Guid addCondiotionalProcuctDiscount(Guid sessionID, string storeName, Guid productID, float percentage, DateTime expDate, int minQuantityForDiscount);

        Guid addConditionalCompositeProcuctDiscount(Guid sessionID, string storeName, Guid productID, float percentage, DateTime expDate, CompositeDiscountPolicyModel conditionalTree);

        Guid addVisibleDiscount(Guid sessionID, string storeName, Guid productID, float percentage, DateTime expDate);

        Guid addProductInv(Guid sessionID, string storeName, string description, string productInvName, double price, int quantity, Category category, List<string> keywords, int minQuantity, int maxQuantity, string imageUrl);

        bool deleteProductInv(Guid sessionID, string storeName, string productInvName);

        Guid addProduct(Guid sessionID, string storeName, string productInvName, int quantity, int minQuantity, int maxQuantity);

        bool deleteProduct(Guid sessionID, string storeName, string productInvName, Guid productID);

        bool modifyProductName(Guid sessionID, string storeName, string newProductName, string oldProductName);

        bool modifyProductPrice(Guid sessionID, string storeName, string productInvName, int newPrice);

        bool modifyProductQuantity(Guid sessionID, string storeName, string productInvName, Guid productID, int newQuantity);

        List<PurchasePolicyModel> getAllPurchasePolicyByStoreName(Guid sessionID, string storeName);

        Guid createOwnerAssignAgreement(Guid sessionID, string newOwneruserName, string storeName);
        bool approveAssignOwnerRequest(Guid sessionID, Guid agreementID, string storeName);
        bool disApproveAssignOwnerRequest(Guid sessionID, Guid agreementID, string storeName);
        bool removeOwner(Guid sessionID, string ownerToRemoveUserName, string storeName);
        bool assignManager(Guid sessionID, string newManageruserName, string storeName);

        bool editPermissions(Guid sessionID, string storeName, string managerUserName, List<PermissionType> permissions);

        bool removeManager(Guid sessionID, string managerUserName, string storeName);

        IEnumerable<StorePurchaseModel> purchaseHistory(Guid sessionID, string storeName);

        (IEnumerable<(UserModel, PermissionModel)>, string) getStoreOwners(Guid sessionID, string storeName);

        (IEnumerable<(UserModel, PermissionModel)>, string) getStoreManagers(Guid sessionID, string storeName);

        (ProductInventoryModel, string) getProductInventory(Guid prodID);

        // System Services
        SearchResultModel getAllProducts(string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter);

        SearchResultModel searchProductsByCategory(string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter);

        SearchResultModel searchProductsByName(string prodName, string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter);

        SearchResultModel searchProductsByKeyword(List<string> keywords, string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter);

        Task<ICollection<ProductModel>> purchaseUserShoppingCart(Guid sessionID, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address);
    }
}