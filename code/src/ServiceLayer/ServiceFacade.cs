using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.Utilities;
using ECommerceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Guid addProduct(Guid sessionID, string storeName, string productInvName, PurchaseType purchaseType, int quantity, int minQuantity, int maxQuantity)
        {
            return _storeServices.addProduct(sessionID, storeName, productInvName, purchaseType, quantity, minQuantity, maxQuantity);
        }

        public Guid addProductInv(Guid sessionID, string storeName, string description, string productInvName, PurchaseType purchaseType, double price, int quantity, string category, List<string> keywords)
        {
            throw new NotImplementedException();
        }

        public bool addProductToCart(Guid sessionID, Guid productId, string storeName, int quantity)
        {
            return _userServices.addProductToCart(sessionID, productId, storeName, quantity);
        }

        public bool assignManager(string newManageruserName, string storeName)
        {
            throw new NotImplementedException();
        }

        public bool assignOwner(string newOwneruserName, string storeName)
        {
            throw new NotImplementedException();
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

        public bool editPermissions(string storeName, string managerUserName, List<string> permissions)
        {
            throw new NotImplementedException();
        }

        public SearchResultModel getAllProducts(string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter)
        {
            return _systemServices.getAllProducts(category, priceFilter, storeRatingFilter, productRatingFilter);
        }

        public Dictionary<StoreModel, List<ProductModel>> getAllStoresInfo()
        {
            return _storeServices.getAllStoresInfo();
        }

        public Tuple<StoreModel, List<ProductModel>> getStoreInfo(string storeName)
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

        public bool modifyProductDiscountType(string storeName, string productInvName, Guid productID)
        {
            throw new NotImplementedException();
        }

        public bool modifyProductName(string storeName, string newProductName, string oldProductName)
        {
            throw new NotImplementedException();
        }

        public bool modifyProductPrice(string storeName, string productInvName, int newPrice)
        {
            throw new NotImplementedException();
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

        public bool removeManager(string managerUserName, string storeName)
        {
            throw new NotImplementedException();
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
    }
}
