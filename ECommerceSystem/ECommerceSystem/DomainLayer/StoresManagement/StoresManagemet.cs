using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.UserManagement;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    class StoresManagement
    {
        private List<Store> _stores;
        private UserManagement.UserManagement _userManagement;

        public StoresManagement(UserManagement.UserManagement userManagement)
        {
            this._userManagement = new UserManagement.UserManagement(); //TODO sync
            this._stores = new List<Store>();
        }


        // Return null if the name isn`t exist
        public Store getStoreByName(string name)
        {
            foreach(Store store in _stores)
            {
                if (store.Name.Equals(name))
                {
                    return store;
                }
            }
            return null; 
        }

        public bool openStore(string name, DiscountPolicy discountPolicy, PurchasePolicy purchasePolicy)
        {
            User loggedInUser = _userManagement.getLoggedInUser(); //sync
            if (!loggedInUser.isSubscribed()) // sync
            {
                return false;
            }

            if (getStoreByName(name) != null) //name already exist
            {
                return false;
            }

            Store newStore = new Store(discountPolicy, purchasePolicy, loggedInUser.name, name); //sync - make user.name property
            _userManagement.addOwnStore(newStore, loggedInUser);
            return true;
        }

        //*********Add, Delete, Modify Products*********

        public bool addProductInv(string storeName, string productInvName, Discount discount, PurchaseType purchaseType, double price, int quantity)
        {
            Store store = getStoreByName(storeName);
            return store == null ? false : store.addProductInv(productInvName, discount, purchaseType, price, quantity);
        }

        public bool addProduct(string storeName, string productInvName, Discount discount, PurchaseType purchaseType, int quantity)
        {
            Store store = getStoreByName(storeName);
            return store == null ? false : store.addProduct(productInvName, discount, purchaseType, quantity);
        }

        public bool deleteProductInventory(string storeName, string productInvName)
        {
            Store store = getStoreByName(storeName);
            return store == null ? false : store.deleteProductInventory(productInvName);
        }

        public bool deleteProduct(string storeName, string productInvName, int productID)
        {
            Store store = getStoreByName(storeName);
            return store == null ? false : store.deleteProduct(productInvName, productID);
        }

        public bool modifyProductName(string storeName, string newProductName, string oldProductName)
        {
            Store store = getStoreByName(storeName);
            return store == null ? false : store.modifyProductName(newProductName, oldProductName);
        }

        public bool modifyProductPrice(string storeName, string productInvName, int newPrice)
        {
            Store store = getStoreByName(storeName);
            return store == null ? false : store.modifyProductPrice(productInvName, newPrice);
        }

        public bool modifyProductQuantity(string storeName, string productInvName, int productID ,int newQuantity)
        {
            Store store = getStoreByName(storeName);
            return store == null ? false : store.modifyProductQuantity(productInvName, productID, newQuantity);
        }

        public bool modifyProductDiscountType(string storeName, string productInvName, int productID, Discount newDiscount)
        {
            Store store = getStoreByName(storeName);
            return store == null ? false : store.modifyProductDiscountType(productInvName, productID, newDiscount);
        }

        public bool modifyProductPurchaseType(string storeName, string productInvName, int productID, PurchaseType purchaseType)
        {
            Store store = getStoreByName(storeName);
            return store == null ? false : store.modifyProductPurchaseType(productInvName, productID, purchaseType);
        }


        //*********Assign*********

        public bool assignOwner(string userName, string storeName)
        {
            Store store = getStoreByName(storeName);
            bool isSuccess = store == null? false : store.assignOwner(userName);
            if (isSuccess)
            {
                User assignedUser = _userManagement.getUserByName(userName);
                _userManagement.addOwnStore(store, assignedUser);
                return true;
            }
            else
                return false;
        }

    }
}
