using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.DomainLayer.Utilities;
using ECommerceSystem.Models;
using ECommerceSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public class StoreManagement
    {
        private List<Store> _stores;
        private UsersManagement _userManagement;

        private static readonly Lazy<StoreManagement> lazy = new Lazy<StoreManagement>(() => new StoreManagement());

        public static StoreManagement Instance => lazy.Value;

        public List<Store> Stores { get => _stores; set => _stores = value; }

        private StoreManagement()
        {
            this._userManagement = UsersManagement.Instance;
            this._stores = new List<Store>();
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
        public Guid addProductInv(string storeName, string description, string productInvName, Discount discount, PurchaseType purchaseType, double price, int quantity, string categoryName, List<string> keywords)
        {
            if (!EnumMethods.GetValues(typeof(Category)).Contains(categoryName.ToUpper())) 
            {
                SystemLogger.LogError("Invalid category name provided " + categoryName);
            }
            var category = (Category)Enum.Parse(typeof(Category), categoryName.ToUpper());
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return Guid.Empty;
            }

            Permissions permission = loggedInUser.getPermission(storeName);
            if (permission == null)
            {
                return Guid.Empty;
            }

            return permission.addProductInv(loggedInUser.Name(), productInvName, description, discount, purchaseType, price, quantity, category, keywords);
        }

        //@pre - logged in user is subscribed
        //return the new product id or -1 in case of fail
        public Guid addProduct(string storeName, string productInvName, Discount discount, PurchaseType purchaseType, int quantity)
        {
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return Guid.Empty;
            }

            Permissions permission = loggedInUser.getPermission(storeName);
            if (permission == null)
            {
                return Guid.Empty;
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
        public bool deleteProduct(string storeName, string productInvName, Guid productID)
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
        public bool modifyProductQuantity(string storeName, string productInvName, Guid productID, int newQuantity)
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
        public bool modifyProductDiscountType(string storeName, string productInvName, Guid productID, Discount newDiscount)
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
        public bool modifyProductPurchaseType(string storeName, string productInvName, Guid productID, PurchaseType purchaseType)
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

        public bool editPermissions(string storeName, string managerUserName, List<string> permissiosnNames)
        {
            var permissionValues = EnumMethods.GetValues(typeof(PermissionType));
            if(!permissiosnNames.Any(p => permissionValues.Contains(p.ToUpper()))) {
                SystemLogger.LogError("Invalid permission type provided in permission list " + permissiosnNames.ToString());
                throw new ArgumentException();
            }
            var permissions = permissiosnNames.Select(p => (PermissionType)Enum.Parse(typeof(PermissionType), p.ToUpper())).ToList();
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

        public Tuple<StoreModel, List<ProductModel>> getStoreProducts(string storeName)
        {
            var info = _stores.Find(s => s.Name.Equals(storeName)).getStoreInfo();
            return Tuple.Create(ModelFactory.CreateStore(info.Item1), info.Item2.Select(p => ModelFactory.CreateProduct(p)).ToList());
        }

        public Dictionary<StoreModel, List<ProductModel>> getAllStoresProducts()
        {
            var storeProdcuts = new Dictionary<StoreModel, List<ProductModel>>();
            _stores.ForEach(s =>
                {
                    var storeInfo = s.getStoreInfo();
                    storeProdcuts.Add(ModelFactory.CreateStore(storeInfo.Item1),
                            storeInfo.Item2.Select(p => ModelFactory.CreateProduct(p)).ToList());
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
            var allProducts = new List<ProductInventory>();
            foreach (Store store in _stores.Where(s => storeRatingFilter.inRange(s.Rating)))
            {
                allProducts = allProducts.Concat(store.Inventory.Products).ToList();
            }
            return allProducts;
        }

        public IEnumerable<StorePurchaseModel> purchaseHistory(string storeName)
        {
            User loggedInUser = _userManagement.getLoggedInUser();
            if (loggedInUser == null)
            {
                return null;
            }

            if (loggedInUser.isSystemAdmin())
            {
                Store store = getStoreByName(storeName);
                return store.purchaseHistory().Select(h => ModelFactory.CreateStorePurchase(h));
            }
            else
            {
                Permissions permission = loggedInUser.getPermission(storeName);
                if (permission == null)
                {
                    return null;
                }

                return permission.purchaseHistory().Select(h => ModelFactory.CreateStorePurchase(h));
            }
        }

        public void logStorePurchase(Store store, User user, double totalPrice, IDictionary<Product, int> storeBoughtProducts)
        {
            var purchasedProducts = storeBoughtProducts.Select(prod => new Product(prod.Key.Name, prod.Key.Description, prod.Key.Discount, prod.Key.PurchaseType, prod.Value, prod.Key.CalculateDiscount(), prod.Key.Id)).ToList();
            store.logPurchase(new StorePurchase(user, totalPrice, purchasedProducts));
        }

    }

}