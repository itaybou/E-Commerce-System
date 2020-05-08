﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.StoresManagement.Discount;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Models;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    interface IStoreInterface
    {

        Guid addProductInv(string activeUserName, string productName, string description, DiscountType discount, PurchaseType purchaseType, double price,
            int quantity, Category category, List<string> keywords, int minQuantity, int maxQuantity);

        Guid addProduct(string loggedInUserName, string productInvName, DiscountType discount, PurchaseType purchaseType, int quantity, int minQuantity, int maxQuantity);

        bool deleteProductInventory(string loggedInUserName, string productInvName);

        bool deleteProduct(string loggedInUserName, string productInvName, Guid productID);

        bool modifyProductPrice(string loggedInUserName, string productName, int newPrice);

        bool modifyProductDiscountType(string loggedInUserName, string productInvName, Guid productID, DiscountType newDiscount);

        bool modifyProductPurchaseType(string loggedInUserName, string productInvName, Guid productID, PurchaseType purchaseType);

        bool modifyProductQuantity(string loggedInUserName, string productName, Guid productID, int newQuantity);

        bool modifyProductName(string loggedInUserName, string newProductName, string oldProductName);

        Permissions assignOwner(User loggedInUser, string newOwneruserName);

        Permissions assignManager(User loggedInUser, string newManageruserName);

        bool removeManager(User loggedInUser, string managerUserName);

        bool editPermissions(string managerUserName, List<PermissionType> permissions, string loggedInUserName);

        void rateStore(double rating);

        void logPurchase(StorePurchaseModel purchase);

        Permissions getPermissionByName(string userName);

        List <StorePurchaseModel> purchaseHistory();


        Guid addDayOffPolicy(List<DayOfWeek> daysOff);
        void removePurchasePolicy(Guid policyID);
        Guid addLocationPolicy(List<string> banLocations);
        Guid addMinPriceStorePolicy(double minPrice);
        Guid addAndPurchasePolicy(Guid iD1, Guid iD2);
        Guid addOrPurchasePolicy(Guid iD1, Guid iD2);
        Guid addXorPurchasePolicy(Guid iD1, Guid iD2);
    }
}
