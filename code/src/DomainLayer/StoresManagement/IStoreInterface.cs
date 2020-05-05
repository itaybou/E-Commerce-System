using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.UserManagement;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    interface IStoreInterface
    {

        Guid addProductInv(string activeUserName, string productName, string description, Discount discount, PurchaseType purchaseType, double price,
            int quantity, Category category, List<string> keywords);

        Guid addProduct(string loggedInUserName, string productInvName, Discount discount, PurchaseType purchaseType, int quantity);

        bool deleteProductInventory(string loggedInUserName, string productInvName);

        bool deleteProduct(string loggedInUserName, string productInvName, Guid productID);

        bool modifyProductPrice(string loggedInUserName, string productName, int newPrice);

        bool modifyProductDiscountType(string loggedInUserName, string productInvName, Guid productID, Discount newDiscount);

        bool modifyProductPurchaseType(string loggedInUserName, string productInvName, Guid productID, PurchaseType purchaseType);

        bool modifyProductQuantity(string loggedInUserName, string productName, Guid productID, int newQuantity);

        bool modifyProductName(string loggedInUserName, string newProductName, string oldProductName);

        Permissions assignOwner(User loggedInUser, string newOwneruserName);

        Permissions assignManager(User loggedInUser, string newManageruserName);

        bool removeManager(User loggedInUser, string managerUserName);

        bool editPermissions(string managerUserName, List<PermissionType> permissions, string loggedInUserName);

        void rateStore(double rating);

        void logPurchase(StorePurchase purchase);

        Permissions getPermissionByName(string userName);

        List<StorePurchase> purchaseHistory();






    }
}
