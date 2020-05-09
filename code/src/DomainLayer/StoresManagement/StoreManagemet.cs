using ECommerceSystem.DomainLayer.StoresManagement.Discount;
using ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies;
using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Utilities;
using ECommerceSystem.Models;
using ECommerceSystem.Models.Notifications;
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
            return activeUser;
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

        //@pre - userID exist and subscribed
        public bool openStore(Guid userID, string name)
        {
            User activeUser = isUserIDSubscribed(userID); //check if if exist and subscribed
            if (activeUser == null) //userID isn`t exist or the user isn`t subscribed
            {
                return false;
            }


            if (getStoreByName(name) != null) //name already exist
            {
                return false;
            }

            Store newStore = new Store(activeUser.Name(), name); //sync - make user.name property

            Permissions permissions = Permissions.CreateOwner(null, newStore);
            newStore.addOwner(activeUser.Name(), permissions);
            _userManagement.addPermission(activeUser, permissions, newStore.Name);

            _stores.Add(newStore);
            return true;
        }

        //*********Add, Delete, Modify Products*********

        //@pre - userID exist and subscribed
        //return product(not product inventory!) id, return -1 in case of fail
        public Guid addProductInv(Guid userID, string storeName, string description, string productInvName, PurchaseType purchaseType, double price, int quantity, Category categoryName, List<string> keywords, int minQuantity, int maxQuantity)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                SystemLogger.LogError("Invalid category name provided " + categoryName);
            }
            var category = (Category)Enum.Parse(typeof(Category), categoryName.ToUpper());
            User loggedInUser = isLoggedInUserSubscribed();
            if (loggedInUser == null) //The logged in user isn`t subscribed
            {
                return Guid.Empty;
            }

            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return Guid.Empty;
            }

            return permission.addProductInv(activeUser.Name(), productInvName, description, purchaseType, price, quantity, categoryName, keywords, minQuantity, maxQuantity);
        }

        //@pre - userID exist and subscribed
        //return the new product id or -1 in case of fail
        public Guid addProduct(Guid userID, string storeName, string productInvName, PurchaseType purchaseType, int quantity, int minQuantity, int maxQuantity)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return Guid.Empty;
            }

            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return Guid.Empty;
            }

            return permission.addProduct(activeUser.Name(), productInvName, purchaseType, quantity, minQuantity, maxQuantity);
        }

        //@pre - userID exist and subscribed
        public bool deleteProductInventory(Guid userID, string storeName, string productInvName)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }

            return permission.deleteProductInventory(activeUser.Name(), productInvName);
        }

        //@pre - userID exist and subscribed
        public bool deleteProduct(Guid userID, string storeName, string productInvName, Guid productID)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }

            return permission.deleteProduct(activeUser.Name(), productInvName, productID);
        }

        //@pre - userID exist and subscribed
        public bool modifyProductName(Guid userID, string storeName, string newProductName, string oldProductName)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }

            return permission.modifyProductName(activeUser.Name(), newProductName, oldProductName);
        }

        //@pre - userID exist and subscribed
        public bool modifyProductPrice(Guid userID, string storeName, string productInvName, int newPrice)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }

            return permission.modifyProductPrice(activeUser.Name(), productInvName, newPrice);
        }

        //@pre - userID exist and subscribed
        public bool modifyProductQuantity(Guid userID, string storeName, string productInvName, Guid productID, int newQuantity)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }

            return permission.modifyProductQuantity(activeUser.Name(), productInvName, productID, newQuantity);
        }

        //@pre - userID exist and subscribed
        public bool modifyProductDiscountType(Guid userID, string storeName, string productInvName, Guid productID, DiscountType newDiscount)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }

            return permission.modifyProductDiscountType(activeUser.Name(), productInvName, productID, newDiscount);
        }

        //@pre - userID exist and subscribed
        public bool modifyProductPurchaseType(Guid userID, string storeName, string productInvName, Guid productID, PurchaseType purchaseType)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }

            return permission.modifyProductPurchaseType(activeUser.Name(), productInvName, productID, purchaseType);
        }

        //*********Assign*********

        //@pre - userID exist and subscribed
        public bool assignOwner(Guid userID, string newOwneruserName, string storeName)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            if (!_userManagement.isSubscribed(newOwneruserName)) //newOwneruserName isn`t subscribed
            {
                return false;
            }
            Permissions activeUserPermissions = activeUser.getPermission(storeName);

            if (activeUserPermissions == null)
            {
                return false;
            }
            Permissions newOwmerPer = activeUserPermissions.assignOwner(activeUser, newOwneruserName);
            if (newOwmerPer != null)
            {
                User assigneeUser = _userManagement.getUserByName(newOwneruserName);
                _userManagement.addPermission(assigneeUser, newOwmerPer, storeName);

                List<User> notificationsUsers = new List<User>();
                notificationsUsers.Add(assigneeUser);
                this.sendNotification(new AssignOwnerNotification(newOwneruserName, activeUser.Name(), storeName), notificationsUsers);
                return true;
            }
            else
                return false;
        }

        //@pre - userID exist and subscribed
        public bool assignManager(Guid userID, string newManageruserName, string storeName)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            if (!_userManagement.isSubscribed(newManageruserName)) //newManageruserName isn`t subscribed
            {
                return false;
            }

            Permissions activeUserPermissions = activeUser.getPermission(storeName);
            if (activeUserPermissions == null)
            {
                return false;
            }

            if(activeUserPermissions == null)
            {
                return false; 
            }

            Permissions newManagerPer =  activeUserPermissions.assignManager(activeUser, newManageruserName);

            if (newManagerPer != null)
            {
                // Add the permission to the new manager
                User assigneeUser = _userManagement.getUserByName(newManageruserName);
                _userManagement.addPermission(assigneeUser, newManagerPer, storeName);

                List<User> notificationsUsers = new List<User>();
                notificationsUsers.Add(assigneeUser);
                this.sendNotification(new AssignManagerNotification(newManageruserName, activeUser.Name(), storeName), notificationsUsers);
                return true;
            }
            else
                return false;
        }

        //@pre - userID exist and subscribed
        public bool removeManager(Guid userID, string managerUserName, string storeName)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }


            bool isSuccess = permission == null ? false : permission.removeManager(activeUser, managerUserName);
            if (isSuccess)
            {
                // Remove the permission from the user
                User toRemoveUser = _userManagement.getUserByName(managerUserName);
                _userManagement.removePermissions(storeName, toRemoveUser);

                List<User> notificationsUsers = new List<User>();
                notificationsUsers.Add(toRemoveUser);
                this.sendNotification(new RemoveManagerNotification(managerUserName, activeUser.Name(), storeName), notificationsUsers);
                return true;
            }
            else
                return false;
        }

        //*********Edit permmiossions*********

        public bool editPermissions(Guid userID, string storeName, string managerUserName, List<PermissionType> permissiosnNames)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }

            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }

            return permission.editPermissions(managerUserName, permissiosnNames, activeUser.Name());
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

        public IEnumerable<StorePurchaseModel> purchaseHistory(Guid userID, string storeName)
        {
            User activeUser = _userManagement.getUserByGUID(userID);
            if (activeUser == null)
            {
                return null;
            }

            if (activeUser.isSystemAdmin())
            {
                Store store = getStoreByName(storeName);
                return store.purchaseHistory();
            }
            else
            {
                Permissions permission = activeUser.getPermission(storeName);
                if (permission == null)
                {
                    return null;
                }

                return permission.purchaseHistory();
            }
        }

        public void logStorePurchase(Store store, User user, double totalPrice, IDictionary<Product, int> storeBoughtProducts)
        {
            List<ProductModel> products = storeBoughtProducts.Select(prod => new ProductModel(prod.Key.Id, prod.Key.Name, prod.Key.Description, prod.Value, prod.Key.BasePrice, prod.Key.CalculateDiscount())).ToList();
            StorePurchaseModel storePurchaseModel = new StorePurchaseModel(user.Name(), totalPrice, products);

            store.logPurchase(storePurchaseModel);

            List<User> notificationsUsers = new List<User>();
            foreach (string username in store.Premmisions.Keys)
            {
                notificationsUsers.Add(_userManagement.getUserByName(username));
            }
            this.sendNotification(new PurchaseNotification(storePurchaseModel, store.Name), notificationsUsers);
        }

        public IDictionary<string, PermissionModel> getUserPermissions(Guid userID)
        {
            var user = _userManagement.getUserByGUID(userID);
            var dict = _stores.ToDictionary(s => s.Name, s => s.getPermissionByName(user.Name())).
                Where(k => k.Value != null).ToDictionary(k => k.Key, k => ModelFactory.CreatePermissions(k.Value));
            return dict;
        }

        public void sendNotification(Notification notification, List <User> recipients)
        {
            foreach(User u in recipients)
            {
                if (_userManagement.isLoggedIn(u.Guid))
                {
                    _communication.sendNotification(notification);
                }
                else
                {
                    u.addNotification(notification);
                }
            }
        }

        //*********Manage Purchase Policy  --   REQUIREMENT 4.2*********

        //*********ADD*********

        public Guid AddDayOffPolicy(Guid userID, string storeName, List<DayOfWeek> daysOff)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return Guid.Empty;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return Guid.Empty;
            }

            return permission.addDayOffPolicy(daysOff);
        }

        public Guid addLocationPolicy(Guid userID, string storeName, List<string> banLocations)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return Guid.Empty;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return Guid.Empty;
            }

            return permission.addLocationPolicy(banLocations);
        }

        public Guid addMinPriceStorePolicy(Guid userID, string storeName, double minPrice)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return Guid.Empty;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return Guid.Empty;
            }

            return permission.addMinPriceStorePolicy(minPrice);
        }

        public Guid addAndPurchasePolicy(Guid userID, string storeName, Guid ID1, Guid ID2)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return Guid.Empty;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return Guid.Empty;
            }

            return permission.addAndPurchasePolicy(ID1, ID2);
        }

        public Guid addOrPurchasePolicy(Guid userID, string storeName, Guid ID1, Guid ID2)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return Guid.Empty;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return Guid.Empty;
            }

            return permission.addOrPurchasePolicy(ID1, ID2);
        }

        public Guid addXorPurchasePolicy(Guid userID, string storeName, Guid ID1, Guid ID2)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return Guid.Empty;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return Guid.Empty;
            }

            return permission.addXorPurchasePolicy(ID1, ID2);
        }
        //*********REMOVE*********

        public bool removePurchasePolicy(Guid userID, string storeName, Guid policyID)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }

            permission.removePurchasePolicy(policyID);
            return true;
        }





        //*********Manage Dicsount Policy  --   REQUIREMENT 4.2*********


        //*********ADD*********
        public Guid addVisibleDiscount(Guid userID, string storeName, Guid productID, float percentage, DateTime expDate)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return Guid.Empty;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return Guid.Empty;
            }

            return permission.addVisibleDiscount(productID, percentage, expDate);
        }

        public Guid addCondiotionalProcuctDiscount(Guid userID, string storeName, Guid productID, float percentage, DateTime expDate, int minQuantityForDiscount)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return Guid.Empty;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return Guid.Empty;
            }

            return permission.addCondiotionalProcuctDiscount(productID, percentage, expDate, minQuantityForDiscount);
        }

        public Guid addConditionalStoreDiscount(Guid userID, string storeName, float percentage, DateTime expDate, int minPriceForDiscount)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return Guid.Empty;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return Guid.Empty;
            }

            return permission.addConditionalStoreDiscount(percentage, expDate, minPriceForDiscount);
        } 

        public Guid addAndDiscountPolicy(Guid userID, string storeName, List<Guid> IDs)
        {

            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return Guid.Empty;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return Guid.Empty;
            }

            return permission.addAndDiscountPolicy(IDs);
        }

        //*********REMOVE*********
        public Guid addOrDiscountPolicy(Guid userID, string storeName, List<Guid> IDs)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return Guid.Empty;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return Guid.Empty;
            }

            return permission.addOrDiscountPolicy(IDs);
        }

        public Guid addXorDiscountPolicy(Guid userID, string storeName, List<Guid> IDs)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return Guid.Empty;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return Guid.Empty;
            }

            return permission.addXorDiscountPolicy(IDs);
        }

        public bool removeProductDiscount(Guid userID, string storeName, Guid discountID, Guid productID)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }

            return permission.removeProductDiscount(discountID, productID);
        }

        public bool removeCompositeDiscount(Guid userID, string storeName, Guid discountID)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }

            return permission.removeCompositeDiscount(discountID);
        }

        public bool removeStoreLevelDiscount(Guid userID, string storeName, Guid discountID)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return false;
            }

            return permission.removeStoreLevelDiscount(discountID);
        }

    }

}