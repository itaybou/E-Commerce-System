using System.Collections.Generic;

namespace ECommerceSystemAcceptanceTests.adapters
{
    interface IBridgeAdapter
    {
        // Utility methods
        bool IsUserSubscribed(string username);
        bool IsUserLogged(string username);
        void usersCleanUp();
        void storesCleanUp();
        void openStoreWithProducts(string storeName, string ownerName, List<string> products);
        void cancelSearchFilters();
        Dictionary<string, Dictionary<long, int>> getUserCartDetails();


        // Requirments
        bool register(string uname, string pswd, string fname, string lname, string email); // Requirment 2.2
        bool login(string uname, string pswd); // Requirment 2.3
        Dictionary<string, List<string>> ViewProdcutStoreInfo(); // Requirment 2.4
        List<string> SearchAndFilterProducts(string prodName, string catName, List<string> keywords, List<string> filters, double from, double to); // Requirment 2.5
        Dictionary<string, Dictionary<long, int>> AddTocart(long prodID, int quantity); // Requirment 2.6
        Dictionary<string, Dictionary<long, int>> ViewUserCart(); //Requirment 2.7
        bool RemoveFromCart(long prodID); //Requirment 2.7.1
        bool ChangeProductCartQuantity(long prodID, int quantity); //Requirment 2.7.2
        bool PurchaseProducts(Dictionary<long, int> products, string firstName, string lastName, string id, string creditCardNumber, string creditExpiration, string CVV, string address); // Requirment 2.8

        bool logout(); // Requirment 3.1
        List<long> PurchaseHistory(); // Requirment 3.7

        bool openStore(string name, string discountPolicy, string purchasePolicy); // Requirment 3.2

        long addProductInv(string storeName, string productName, string description, string discountType, int discountPercentage, string purchaseType, double price, int quantity, Category category, List <string> keys); // Requirment 4.1.1

        bool deleteProductInv(string storeName, string productName); // Requirment 4.1.2

        //Requirment 4.1.3 modify product:
        long addProduct(string storeName, string productInvName, string discountType, int discountPercentage, string purchaseType, int quantity);
        bool deleteProduct(string storeName, string productInvName, long productID);
        bool modifyProductName(string storeName, string newProductName, string oldProductName);
        bool modifyProductPrice(string storeName, string productInvName, int newPrice);
        bool modifyProductQuantity(string storeName, string productInvName, long productID, int newQuantity);
        bool modifyProductDiscountType(string storeName, string productInvName, long productID, string newDiscount, int discountPercentage);
        bool modifyProductPurchaseType(string storeName, string productInvName, long productID, string purchaseType);


        bool assignOwner(string newOwneruserName, string storeName); // Requirment 4.3

        bool assignManager(string newManageruserName, string storeName); // Requirment 4.5

        bool editPermissions(string storeName, string managerUserName, List<permissionType> permissions); //Requirement 4.6

        bool removeManager(string managerUserName, string storeName); //Requirement 4.7

        List <Tuple<string, List<Tuple<long, int>>, double>> purchaseHistory(string storeName); // Requirements 4.10 and 6.4.2


    }
}
