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
    public interface IService
    {
        Tuple<StoreModel, List<ProductModel>> getStoreInfo(string storeName);
        Dictionary<StoreModel, List<ProductModel>> getAllStoresInfo();

        // User Services
        bool isUserSubscribed(string username);
        bool isUserLogged(string username);
        void removeAllUsers();
        bool register(string uname, string pswd, string fname, string lname, string email);
        bool login(string uname, string pswd);
        bool logout();
        bool addProductToCart(Guid productId, string storeName, int quantity);
        ShoppingCartModel ShoppingCartDetails();
        bool RemoveFromCart(Guid productId);
        bool ChangeProductQunatity(Guid productId, int quantity);
        ICollection<UserPurchaseModel> userPurchaseHistory(string userName);

        // Store Services
        bool openStore(string name, DiscountPolicy discountPolicy, PurchasePolicy purchasePolicy);

        Guid addProductInv(string storeName, string description, string productInvName, Discount discount, PurchaseType purchaseType, double price, int quantity, string category, List<string> keywords);

        bool deleteProductInv(string storeName, string productInvName);

        Guid addProduct(string storeName, string productInvName, Discount discount, PurchaseType purchaseType, int quantity);

        bool deleteProduct(string storeName, string productInvName, Guid productID);

        bool modifyProductName(string storeName, string newProductName, string oldProductName);

        bool modifyProductPrice(string storeName, string productInvName, int newPrice);

        bool modifyProductQuantity(string storeName, string productInvName, Guid productID, int newQuantity);

        bool modifyProductDiscountType(string storeName, string productInvName, Guid productID, Discount newDiscount);

        bool modifyProductPurchaseType(string storeName, string productInvName, Guid productID, PurchaseType purchaseType);

        bool assignOwner(string newOwneruserName, string storeName);

        bool assignManager(string newManageruserName, string storeName);

        bool editPermissions(string storeName, string managerUserName, List<string> permissions);

        bool removeManager(string managerUserName, string storeName);

        IEnumerable<StorePurchaseModel> purchaseHistory(string storeName);

        // System Services
        SearchResultModel getAllProducts(string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter);
        SearchResultModel searchProductsByCategory(string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter);
        SearchResultModel searchProductsByName(string prodName, string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter);
        SearchResultModel searchProductsByKeyword(List<string> keywords, string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter);
        ICollection<ProductModel> purchaseUserShoppingCart(Guid userID, string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address);
    }
}
