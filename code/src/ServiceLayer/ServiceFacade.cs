using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.Utilities;
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

        public Guid addProduct(string storeName, string productInvName, Discount discount, PurchaseType purchaseType, int quantity)
        {
            return _storeServices.addProduct(storeName, productInvName, discount, purchaseType, quantity);
        }

        public Guid addProductInv(string storeName, string description, string productInvName, Discount discount, PurchaseType purchaseType, double price, int quantity, string category, List<string> keywords)
        {
            throw new NotImplementedException();
        }

        public bool addProductToCart(Guid productId, string storeName, int quantity)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Dictionary<StoreModel, List<ProductModel>> getAllStoresInfo()
        {
            throw new NotImplementedException();
        }

        public Tuple<StoreModel, List<ProductModel>> getStoreInfo(string storeName)
        {
            throw new NotImplementedException();
        }

        public bool isUserLogged(string username)
        {
            throw new NotImplementedException();
        }

        public bool isUserSubscribed(string username)
        {
            throw new NotImplementedException();
        }

        public bool login(string uname, string pswd)
        {
            throw new NotImplementedException();
        }

        public bool logout()
        {
            throw new NotImplementedException();
        }

        public bool modifyProductDiscountType(string storeName, string productInvName, Guid productID, Discount newDiscount)
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

        public bool openStore(string name, DiscountPolicy discountPolicy, PurchasePolicy purchasePolicy)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StorePurchaseModel> purchaseHistory(string storeName)
        {
            throw new NotImplementedException();
        }

        public ICollection<ProductModel> purchaseUserShoppingCart(Guid userID, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address)
        {
            throw new NotImplementedException();
        }

        public bool register(string uname, string pswd, string fname, string lname, string email)
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
            throw new NotImplementedException();
        }

        public SearchResultModel searchProductsByKeyword(List<string> keywords, string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter)
        {
            throw new NotImplementedException();
        }

        public SearchResultModel searchProductsByName(string prodName, string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter)
        {
            throw new NotImplementedException();
        }

        public ShoppingCartModel ShoppingCartDetails()
        {
            throw new NotImplementedException();
        }

        public ICollection<UserPurchaseModel> userPurchaseHistory(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
