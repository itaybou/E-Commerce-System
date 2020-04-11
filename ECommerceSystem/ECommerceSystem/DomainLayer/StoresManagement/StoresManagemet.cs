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
        private UsersManagement _userManagement;
        private long _productInvID;

        public StoresManagement()
        {
            this._userManagement = UsersManagement.Instance;
            this._stores = new List<Store>();
            this._productInvID = 0;
        }
        

        // Return the user that logged in to the system if the user is subscribed
        // If the user isn`t subscribed return null

        private User isLoggedInUserSubscribed()
        {
            User loggedInUser = _userManagement.getLoggedInUser(); //sync
            if (!loggedInUser.isSubscribed()) // sync
            {
                return null;
            }
            return loggedInUser;
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

        //@pre - logged in user is subscribed
        public bool openStore(string name, DiscountPolicy discountPolicy, PurchasePolicy purchasePolicy)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            if (getStoreByName(name) != null) //name already exist
            {
                return false;
            }

            Store newStore = new Store(discountPolicy, purchasePolicy, loggedInUser.Name(), name); //sync - make user.name property
            _userManagement.addOwnStore(newStore, loggedInUser);
            return true;
        }

        //*********Add, Delete, Modify Products*********

        //@pre - logged in user is subscribed
        public bool addProductInv(string storeName, string productInvName, Discount discount, PurchaseType purchaseType, double price, int quantity)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            Store store = getStoreByName(storeName);
            return store == null ? false : store.addProductInv(loggedInUser.Name(), productInvName, discount, purchaseType, price, quantity, ++_productInvID);
        }

        //@pre - logged in user is subscribed
        public bool addProduct(string storeName, string productInvName, Discount discount, PurchaseType purchaseType, int quantity)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            Store store = getStoreByName(storeName);
            return store == null ? false : store.addProduct(loggedInUser.Name(), productInvName, discount, purchaseType, quantity);
        }

        //@pre - logged in user is subscribed
        public bool deleteProductInventory(string storeName, string productInvName)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            Store store = getStoreByName(storeName);
            return store == null ? false : store.deleteProductInventory(loggedInUser.Name(), productInvName);
        }

        //@pre - logged in user is subscribed
        public bool deleteProduct(string storeName, string productInvName, int productID)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            Store store = getStoreByName(storeName);
            return store == null ? false : store.deleteProduct(loggedInUser.Name(), productInvName, productID);
        }

        //@pre - logged in user is subscribed
        public bool modifyProductName(string storeName, string newProductName, string oldProductName)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            Store store = getStoreByName(storeName);
            return store == null ? false : store.modifyProductName(loggedInUser.Name(), newProductName, oldProductName);
        }

        //@pre - logged in user is subscribed
        public bool modifyProductPrice(string storeName, string productInvName, int newPrice)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }
            Store store = getStoreByName(storeName);
            return store == null ? false : store.modifyProductPrice(loggedInUser.Name(), productInvName, newPrice);
        }

        //@pre - logged in user is subscribed
        public bool modifyProductQuantity(string storeName, string productInvName, int productID ,int newQuantity)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }
            Store store = getStoreByName(storeName);
            return store == null ? false : store.modifyProductQuantity(loggedInUser.Name(), productInvName, productID, newQuantity);
        }

        //@pre - logged in user is subscribed
        public bool modifyProductDiscountType(string storeName, string productInvName, int productID, Discount newDiscount)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }
            Store store = getStoreByName(storeName);
            return store == null ? false : store.modifyProductDiscountType(loggedInUser.Name(), productInvName, productID, newDiscount);
        }

        //@pre - logged in user is subscribed
        public bool modifyProductPurchaseType(string storeName, string productInvName, int productID, PurchaseType purchaseType)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }
            Store store = getStoreByName(storeName);
            return store == null ? false : store.modifyProductPurchaseType(loggedInUser.Name(), productInvName, productID, purchaseType);
        }


        //*********Assign*********

        //@pre - logged in user is subscribed
        public bool assignOwner(string newOwneruserName, string storeName)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            if (!_userManagement.isSubscribed(newOwneruserName)) //newOwneruserName isn`t subscribed
            {
                return false;
            }

            Store store = getStoreByName(storeName);
            bool isSuccess = store == null? false : store.assignOwner(loggedInUser, newOwneruserName);
            if (isSuccess)
            {
                User assignedUser = _userManagement.getUserByName(newOwneruserName);
                _userManagement.addOwnStore(store, assignedUser);
                return true;
            }
            else
                return false;
        }

        //@pre - logged in user is subscribed
        public bool assignManager(string newManageruserName, string storeName)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            if (!_userManagement.isSubscribed(newManageruserName)) //newManageruserName isn`t subscribed
            {
                return false;
            }

            Store store = getStoreByName(storeName);
            bool isSuccess = store == null ? false : store.assignManager(loggedInUser, newManageruserName);
            if (isSuccess)
            {
                // Add the store to the list of the stores that the user manage
                User assignedUser = _userManagement.getUserByName(newManageruserName);
                _userManagement.addManagerStore(store, assignedUser);
                return true;
            }
            else
                return false;
        }

        //@pre - logged in user is subscribed
        public bool removeManager(string managerUserName, string storeName)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            Store store = getStoreByName(storeName);
            bool isSuccess = store == null ? false : store.removeManager(loggedInUser, managerUserName);
            if (isSuccess)
            {
                // Remove the store from the list of the stores that the user manage
                User assignedUser = _userManagement.getUserByName(managerUserName);
                _userManagement.removeManagerStore(store, assignedUser);
                return true;
            }
            else
                return false;
        }


        //*********Edit permmiossions*********

        public bool editPermissions(string storeName, string managerUserName, List<permissionType> permissions)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            Store store = getStoreByName(storeName);
            if(store == null)
            {
                return false;
            }

            return store.editPermissions(managerUserName, permissions, loggedInUser.Name());

        }

        public Dictionary<Store, List<Product>> getAllStoresProducts()
        {
            var storeProdcuts = new Dictionary<Store, List<Product>>();
            _stores.ForEach(s => 
                {
                var storeInfo = s.getStoreInfo();
                storeProdcuts.Add(storeInfo.Item1, storeInfo.Item2);
                }
            );
            return storeProdcuts;
        }
    }
}
