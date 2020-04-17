using System.Collections.Generic;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;

namespace ECommerceSystemAcceptanceTests.adapters
{
    interface IBridgeAdapter
    {
        bool register(string uname, string pswd, string fname, string lname, string email); // Requirment 2.2

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

        List<StorePurchase> purchaseHistory(string storeName); // Requirements 4.10 and 6.4.2


    }
}
