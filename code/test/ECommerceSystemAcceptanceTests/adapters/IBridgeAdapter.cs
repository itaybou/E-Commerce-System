using System;
using System.Collections.Generic;
using ECommerceSystem.DomainLayer.UserManagement;

namespace ECommerceSystemAcceptanceTests.adapters
{
    interface IBridgeAdapter
    {
        // Utility methods
        bool IsUserSubscribed(string username);
        bool IsUserLogged(string username);
        void usersCleanUp();
        void storesCleanUp();
        void openStoreWithProducts(Guid userID, string storeName, string ownerName, List<string> products);
        void cancelSearchFilters();
        Dictionary<string, Dictionary<Guid, int>> getUserCartDetails();


        // Requirments
        bool register(string uname, string pswd, string fname, string lname, string email); // Requirment 2.2
        bool login(string uname, string pswd); // Requirment 2.3
        Dictionary<string, List<string>> ViewProdcutStoreInfo(); // Requirment 2.4
        List<string> SearchAndFilterProducts(string prodName, string catName, List<string> keywords, List<string> filters, double from, double to); // Requirment 2.5
        Dictionary<string, Dictionary<Guid, int>> AddTocart(Guid prodID, int quantity); // Requirment 2.6
        Dictionary<string, Dictionary<Guid, int>> ViewUserCart(); //Requirment 2.7
        bool RemoveFromCart(Guid prodID); //Requirment 2.7.1
        bool ChangeProductCartQuantity(Guid prodID, int quantity); //Requirment 2.7.2
        bool PurchaseProducts(Dictionary<Guid, int> products, string firstName, string lastName, string id, string creditCardNumber, string creditExpiration, string CVV, string address); // Requirment 2.8

        bool logout(); // Requirment 3.1
        List<Guid> UserPurchaseHistory(string uname); // Requirment 3.7

        bool openStore(Guid userID, string name, string discountPolicy, string purchasePolicy); // Requirment 3.2

        Guid addProductInv(Guid userID, string storeName, string productName, string description, string discountType, int discountPercentage, string purchaseType, double price, int quantity, string category, List <string> keys); // Requirment 4.1.1

        bool deleteProductInv(Guid userID, string storeName, string productName); // Requirment 4.1.2

        //Requirment 4.1.3 modify product:
        Guid addProduct(Guid userID, string storeName, string productInvName, string discountType, int discountPercentage, string purchaseType, int quantity);
        bool deleteProduct(Guid userID, string storeName, string productInvName, Guid productID);
        bool modifyProductName(Guid userID, string storeName, string newProductName, string oldProductName);
        bool modifyProductPrice(Guid userID, string storeName, string productInvName, int newPrice);
        bool modifyProductQuantity(Guid userID, string storeName, string productInvName, Guid productID, int newQuantity);
        bool modifyProductDiscountType(Guid userID, string storeName, string productInvName, Guid productID, string newDiscount, int discountPercentage);
        bool modifyProductPurchaseType(Guid userID, string storeName, string productInvName, Guid productID, string purchaseType);


        bool assignOwner(Guid userID, string newOwneruserName, string storeName); // Requirment 4.3

        bool assignManager(Guid userID, string newManageruserName, string storeName); // Requirment 4.5

        bool editPermissions(Guid userID, string storeName, string managerUserName, List<PermissionType> permissions); //Requirement 4.6

        bool removeManager(Guid userID, string managerUserName, string storeName); //Requirement 4.7

        List <Tuple<string, List<Tuple<Guid, int>>, double>> StorePurchaseHistory(Guid userID, string storeName); // Requirements 4.10 and 6.4.2


    }
}
