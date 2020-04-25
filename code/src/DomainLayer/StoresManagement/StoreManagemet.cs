using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.DomainLayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public class StoreManagement
    {
        private List<Store> _stores;
        private UsersManagement _userManagement;
        private long _productInvID;

        private static readonly Lazy<StoreManagement> lazy = new Lazy<StoreManagement>(() => new StoreManagement());

        public static StoreManagement Instance => lazy.Value;

        public List<Store> Stores { get => _stores; set => _stores = value; }

        private StoreManagement()
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
            foreach (Store store in _stores)
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

            Permissions permissions = Permissions.CreateOwner(null, newStore);
            newStore.addOwner(loggedInUser.Name(), permissions);
            _userManagement.addPermission(loggedInUser, permissions, newStore.Name);

            _stores.Add(newStore);
            return true;
        }


        //*********Add, Delete, Modify Products*********

        //@pre - logged in user is subscribed
        //return product(not product inventory!) id, return -1 in case of fail
        public long addProductInv(string storeName, string description, string productInvName, Discount discount, PurchaseType purchaseType, double price, int quantity, Category category, List<string> keywords)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return -1;
            }

            Permissions permission = loggedInUser.getPermission(storeName);
            if (permission == null)
            {
                return -1;
            }

            return permission.addProductInv(loggedInUser.Name(), productInvName, description, discount, purchaseType, price, quantity, category, keywords, ++_productInvID);
        }

        //@pre - logged in user is subscribed
        //return the new product id or -1 in case of fail
        public long addProduct(string storeName, string productInvName, Discount discount, PurchaseType purchaseType, int quantity)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return -1;
            }

            Permissions permission = loggedInUser.getPermission(storeName);
            if (permission == null)
            {
                return -1;
            }

            return permission.addProduct(loggedInUser.Name(), productInvName, discount, purchaseType, quantity);
        }

        //@pre - logged in user is subscribed
        public bool deleteProductInventory(string storeName, string productInvName)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            Permissions permission = loggedInUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }

            return permission.deleteProductInventory(loggedInUser.Name(), productInvName);
        }

        //@pre - logged in user is subscribed
        public bool deleteProduct(string storeName, string productInvName, long productID)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            Permissions permission = loggedInUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }

            return permission.deleteProduct(loggedInUser.Name(), productInvName, productID);
        }

        //@pre - logged in user is subscribed
        public bool modifyProductName(string storeName, string newProductName, string oldProductName)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            Permissions permission = loggedInUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }

            return permission.modifyProductName(loggedInUser.Name(), newProductName, oldProductName);
        }

        //@pre - logged in user is subscribed
        public bool modifyProductPrice(string storeName, string productInvName, int newPrice)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }
            Permissions permission = loggedInUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }

            return permission.modifyProductPrice(loggedInUser.Name(), productInvName, newPrice);
        }

        //@pre - logged in user is subscribed
        public bool modifyProductQuantity(string storeName, string productInvName, long productID, int newQuantity)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }
            Permissions permission = loggedInUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }

            return permission.modifyProductQuantity(loggedInUser.Name(), productInvName, productID, newQuantity);
        }

        //@pre - logged in user is subscribed
        public bool modifyProductDiscountType(string storeName, string productInvName, long productID, Discount newDiscount)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }
            Permissions permission = loggedInUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }

            return permission.modifyProductDiscountType(loggedInUser.Name(), productInvName, productID, newDiscount);
        }

        //@pre - logged in user is subscribed
        public bool modifyProductPurchaseType(string storeName, string productInvName, long productID, PurchaseType purchaseType)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }
            Permissions permission = loggedInUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }

            return permission.modifyProductPurchaseType(loggedInUser.Name(), productInvName, productID, purchaseType);
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
            Permissions loggedInUserPermission = loggedInUser.getPermission(storeName);

            if (loggedInUserPermission == null)
            {
                return false;
            }
            Permissions newOwmerPer = loggedInUserPermission.assignOwner(loggedInUser, newOwneruserName);
            if (newOwmerPer != null)
            {
                User assigneeUser = _userManagement.getUserByName(newOwneruserName);
                _userManagement.addPermission(assigneeUser, newOwmerPer, storeName);
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

            Permissions loggedInUserPermission = loggedInUser.getPermission(storeName);
            if (loggedInUserPermission == null)
            {
                return false;
            }

            if(loggedInUserPermission == null)
            {
                return false; 
            }

            Permissions newManagerPer =  loggedInUserPermission.assignManager(loggedInUser, newManageruserName);

            if (newManagerPer != null)
            {
                // Add the permission to the new manager
                User assigneeUser = _userManagement.getUserByName(newManageruserName);
                _userManagement.addPermission(assigneeUser, newManagerPer, storeName);
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

            Permissions permission = loggedInUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }


            bool isSuccess = permission == null ? false : permission.removeManager(loggedInUser, managerUserName);
            if (isSuccess)
            {
                // Remove the permission from the user
                User toRemoveUser = _userManagement.getUserByName(managerUserName);
                _userManagement.removePermissions(storeName, toRemoveUser);
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

            Permissions permission = loggedInUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }

            return permission.editPermissions(managerUserName, permissions, loggedInUser.Name());
        }

        public Tuple<Store, List<Product>> getStoreProducts(string storeName)
        {
            return _stores.Find(s => s.Name.Equals(storeName)).getStoreInfo();
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

        public List<ProductInventory> getAllStoresProdcutInventories()
        {
            var allProducts = new List<ProductInventory>();
            foreach (Store store in _stores)
            {
                allProducts = allProducts.Concat(store.Inventory.Products).ToList();
            }
            return allProducts;
        }

        public List<ProductInventory> getAllStoreInventoryWithRating(Range<double> storeRatingFilter)
        {
            var allProdcuts = new List<ProductInventory>();
            foreach (Store store in _stores.Where(s => storeRatingFilter.inRange(s.Rating)))
            {
                allProdcuts.Concat(store.Inventory.Products);
            }
            return allProdcuts;
        }

        public List<StorePurchase> purchaseHistory(string storeName)
        {
            User loggedInUser = _userManagement.getLoggedInUser();
            if (loggedInUser == null)
            {
                return null;
            }

            if (loggedInUser.isSystemAdmin())
            {
                Store store = getStoreByName(storeName);
                return store.purchaseHistory();
            }
            else
            {
                Permissions permission = loggedInUser.getPermission(storeName);
                if (permission == null)
                {
                    return null;
                }

                return permission.purchaseHistory();
            }

        }

        public void logStorePurchase(Store store, User user, double totalPrice, Dictionary<Product, int> storeBoughtProducts)
        {
            var purchasedProducts = storeBoughtProducts.Select(prod => new Product(prod.Key.Discount, prod.Key.PurchaseType, prod.Value, prod.Key.CalculateDiscount(), prod.Key.Id)).ToList();
            store.logPurchase(new StorePurchase(user, totalPrice, purchasedProducts));
        }

    }

}