using ECommerceSystem.Models;
using ECommerceSystem.Models.PurchasePolicyModels;
using ECommerceSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceSystemAcceptanceTests.adapters
{
    internal interface IBridgeAdapter
    {
        void initSessions();

        //Dictionary<string, Dictionary<Guid, int>> getUserCartDetails();

        // Requirments
        bool  register( string uname, string pswd, string fname, string lname, string email); // Requirment 2.2

        bool login(string uname, string pswd); // Requirment 2.3

        Dictionary<StoreModel, List<ProductInventoryModel>> ViewProdcutStoreInfo(); // Requirment 2.4

        //List<string> SearchAndFilterProducts(string prodName, string catName, List<string> keywords, List<string> filters, double from, double to); // Requirment 2.5
        // Requirment 2.5
        SearchResultModel getAllProducts(string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter);
        SearchResultModel searchProductsByCategory(string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter);
        SearchResultModel searchProductsByName(string prodName, string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter);
        SearchResultModel searchProductsByKeyword(List<string> keywords, string category, Range<double> priceFilter, Range<double> storeRatingFilter, Range<double> productRatingFilter);
        
        bool AddTocart( Guid productId, string storeName, int quantity); // Requirment 2.6

        ShoppingCartModel ViewUserCart(); //Requirment 2.7

        bool RemoveFromCart(Guid prodID); //Requirment 2.7.1

        bool ChangeProductCartQuantity(Guid prodID, int quantity); //Requirment 2.7.2

        Task<ICollection<ProductModel>> purchaseUserShoppingCart( string firstName, string lastName, int id, string creditCardNumber, DateTime expirationCreditCard, int CVV, string address); // Requirment 2.8

        bool logout(); // Requirment 3.1

        List<Guid> UserPurchaseHistory(string uname); // Requirment 3.7

        bool openStore(string name); // Requirment 3.2

        Guid addProductInv(string storeName, string description, string productInvName, double price, int quantity, Category category, List<string> keywords, int minQuantity, int maxQuantity, string imageUrl); // Requirment 4.1.1

        bool deleteProductInv(string storeName, string productInvName); // Requirment 4.1.2


        //Requirment 4.1.3 modify product:
        Guid addProduct(string storeName, string productInvName, int quantity, int minQuantity, int maxQuantity);

        bool deleteProduct( string storeName, string productInvName, Guid productID);

        bool modifyProductName( string storeName, string newProductName, string oldProductName);

        bool modifyProductPrice(string storeName, string productInvName, int newPrice);

        bool modifyProductQuantity(string storeName, string productInvName, Guid productID, int newQuantity);

        //4.2 Purchase Policy 
        Guid addDayOffPolicy( string storeName, List<DayOfWeek> daysOff);
        Guid addLocationPolicy( string storeName, List<string> banLocations);
        Guid addMinPriceStorePolicy( string storeName, double minPrice);
        Guid addAndPurchasePolicy( string storeName, Guid ID1, Guid ID2);
        Guid addOrPurchasePolicy( string storeName, Guid ID1, Guid ID2);
        Guid addXorPurchasePolicy( string storeName, Guid ID1, Guid ID2);
        bool removePurchasePolicy( string storeName, Guid policyID);
        List<PurchasePolicyModel> getAllPurchasePolicyByStoreName( string storeName);

        //4.2 Dicsount Policy 




        Guid createOwnerAssignAgreement( string newOwneruserName, string storeName); // Requirment 4.3

        bool approveAssignOwnerRequest( Guid agreementID, string storeName); // Requirment 4.3

        bool disApproveAssignOwnerRequest( Guid agreementID, string storeName); // Requirment 4.3


        bool assignManager(string newManageruserName, string storeName); // Requirment 4.5

        bool editPermissions(string storeName, string managerUserName, List<PermissionType> permissions); //Requirement 4.6

        bool removeManager( string managerUserName, string storeName); //Requirement 4.7

        IEnumerable<StorePurchaseModel> storePurchaseHistory( string storeName); // Requirements 4.10 and 6.4.2

        bool removeOwner( string ownerToRemoveUserName, string storeName); 
    }
}