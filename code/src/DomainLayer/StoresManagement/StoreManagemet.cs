using ECommerceSystem.CommunicationLayer;
using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Models;
using ECommerceSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.DomainLayer.StoresManagement
{
    public class StoreManagement
    {
        private UsersManagement _userManagement;
        private ICommunication _communication;
        private IDataAccess _data;

        private static readonly Lazy<StoreManagement> lazy = new Lazy<StoreManagement>(() => new StoreManagement());
        public static StoreManagement Instance => lazy.Value;

        private StoreManagement()
        {
            _communication = Communication.Instance;
            _data = DataAccess.Instance;
            _userManagement = UsersManagement.Instance;
        }

        private IEnumerable<(UserModel, PermissionModel)> getRoleHolders(string storeName, bool owner)
        {
            var roleHolders = new List<(User, Permissions)>();
            var permissions = _data.Stores.FetchAll().Select(store =>
            {
                if (store.Name.Equals(storeName))
                {
                    return store.Permissions;
                }
                return null;
            });
            foreach (var permission in permissions)
            {
                if (permission != null)
                {
                    foreach (var user in permission.Keys)
                    {
                        var userPermissions = permission[user];
                        var condition = owner ? permission[user].isOwner() : !permission[user].isOwner();
                        if (condition)
                            roleHolders.Add((_userManagement.getUserByName(user), userPermissions));
                    }
                }
                else return new List<(UserModel, PermissionModel)>();
            }
            return roleHolders.Select(holder => (ModelFactory.CreateUser(holder.Item1), ModelFactory.CreatePermissions(holder.Item2)));
        }

        public (IEnumerable<(UserModel, PermissionModel)>, string) getStoreOwners(string storeName)
        {
            return (getRoleHolders(storeName, true), storeName);
        }

        public (IEnumerable<(UserModel, PermissionModel)>, string) getStoreManagers(string storeName)
        {
            return (getRoleHolders(storeName, false), storeName);
        }

        // Return the user that logged in to the system if the user is subscribed
        // If the user isn`t subscribed return null
        private User isUserIDSubscribed(Guid userID)
        {
            var user = _userManagement.getUserByGUID(userID);
            return user.isSubscribed() ? user : null;
        }

        // Return null if the name isn`t exist
        public Store getStoreByName(string name)
        {
            return _data.Stores.GetByIdOrNull(name, s => s.Name);
        }

        //@pre - userID exist and subscribed
        public bool openStore(Guid userID, string name)
        {
            User activeUser = isUserIDSubscribed(userID); //check if if exist and subscribed
            if (activeUser == null) //userID isn`t exist or the user isn`t subscribed
                return false;

            if (getStoreByName(name) != null) //name already exist
                return false;

            Store newStore = new Store(activeUser.Name, name); //sync - make user.name property

            Permissions permissions = Permissions.CreateOwner(null, newStore);
            newStore.addOwner(activeUser.Name, permissions);
            _userManagement.addPermission(activeUser, permissions, newStore.Name);

            _data.Transactions.OpenStoreTransaction(activeUser, newStore);
            return true;
        }

        //*********Add, Delete, Modify Products*********

        //@pre - userID exist and subscribed
        //return product(not product inventory!) id, return -1 in case of fail
        public Guid addProductInv(Guid userID, string storeName, string description, string productInvName, double price, int quantity, Category categoryName, List<string> keywords, int minQuantity, int maxQuantity)
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

            var result = permission.addProductInv(activeUser.Name, productInvName, description, price, quantity, categoryName, keywords, minQuantity, maxQuantity);
            _data.Stores.Update((Store)permission.Store, storeName, s => s.Name);
            return result;
        }

        //@pre - userID exist and subscribed
        //return the new product id or -1 in case of fail
        public Guid addProduct(Guid userID, string storeName, string productInvName, int quantity, int minQuantity, int maxQuantity)
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

            return permission.addProduct(activeUser.Name, productInvName, quantity, minQuantity, maxQuantity);
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

            return permission.deleteProductInventory(activeUser.Name, productInvName);
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

            return permission.deleteProduct(activeUser.Name, productInvName, productID);
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

            return permission.modifyProductName(activeUser.Name, newProductName, oldProductName);
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

            return permission.modifyProductPrice(activeUser.Name, productInvName, newPrice);
        }

        internal IDictionary<PermissionType, bool> getUserPermissionTypes(string storeName, string username)
        {
            return _data.Stores.FindOneBy(store => store.Name.Equals(storeName)).getUsernamePermissionTypes(username);
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

            return permission.modifyProductQuantity(activeUser.Name, productInvName, productID, newQuantity);
        }

        //@pre - userID exist and subscribed
        //public bool modifyProductDiscountType(Guid userID, string storeName, string productInvName, Guid productID, DiscountType newDiscount)
        //{
        //    User activeUser = isUserIDSubscribed(userID);
        //    if (activeUser == null) //The logged in user isn`t subscribed
        //    {
        //        return false;
        //    }
        //    Permissions permission = activeUser.getPermission(storeName);
        //    if (permission == null)
        //    {
        //        return false;
        //    }

        //    return permission.modifyProductDiscountType(activeUser.Name(), productInvName, productID, newDiscount);
        //}

        ////@pre - userID exist and subscribed
        //public bool modifyProductPurchaseType(Guid userID, string storeName, string productInvName, Guid productID, PurchaseType purchaseType)
        //{
        //    User activeUser = isUserIDSubscribed(userID);
        //    if (activeUser == null) //The logged in user isn`t subscribed
        //    {
        //        return false;
        //    }
        //    Permissions permission = activeUser.getPermission(storeName);
        //    if (permission == null)
        //    {
        //        return false;
        //    }

        //    return permission.modifyProductPurchaseType(activeUser.Name(), productInvName, productID, purchaseType);
        //}

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

                _communication.SendPrivateNotification(assigneeUser.Guid, new AssignOwnerNotification(newOwneruserName, activeUser.Name, storeName));
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

            if (activeUserPermissions == null)
            {
                return false;
            }

            Permissions newManagerPer = activeUserPermissions.assignManager(activeUser, newManageruserName);

            if (newManagerPer != null)
            {
                // Add the permission to the new manager
                User assigneeUser = _userManagement.getUserByName(newManageruserName);
                _userManagement.addPermission(assigneeUser, newManagerPer, storeName);

                _communication.SendPrivateNotification(assigneeUser.Guid, new AssignManagerNotification(newManageruserName, activeUser.Name, storeName));
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

                _communication.SendPrivateNotification(toRemoveUser.Guid, new RemoveManagerNotification(managerUserName, activeUser.Name, storeName));
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

            return permission.editPermissions(managerUserName, permissiosnNames, activeUser.Name);
        }

        public Tuple<StoreModel, List<ProductModel>> getStoreProducts(string storeName)
        {
            var info = _data.Stores.FindOneBy(s => s.Name.Equals(storeName)).getStoreInfo();
            return Tuple.Create(ModelFactory.CreateStore(info.Item1), info.Item2.Select(p => ModelFactory.CreateProduct(p)).ToList());
        }

        public Dictionary<StoreModel, List<ProductModel>> getAllStoresProducts()
        {
            var storeProdcuts = new Dictionary<StoreModel, List<ProductModel>>();
            _data.Stores.FetchAll().ToList().ForEach(s =>
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
            var stores = _data.Stores.FetchAll();
            foreach (Store store in stores)
            {
                allProducts = allProducts.Concat(store.Inventory.Products).ToList();
            }
            return allProducts;
        }

        public (ProductModel, string) getProductInventory(Guid prodID)
        {
            var stores = _data.Stores.FetchAll();
            foreach (Store store in stores)
            {
                foreach (var prodInv in store.Inventory)
                {
                    foreach (var prod in prodInv.ProductList)
                    {
                        if (prod.Id.Equals(prodID))
                            return (ModelFactory.CreateProduct(prodInv.ProductList.First()), store.Name);
                    }
                }
            }
            return (null, null);
        }

        public List<ProductInventory> getAllStoreInventoryWithRating(Range<double> storeRatingFilter)
        {
            var allProducts = new List<ProductInventory>();
            var stores = _data.Stores.FindAllBy(s => storeRatingFilter.inRange(s.Rating));
            foreach (Store store in stores)
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
            StorePurchaseModel storePurchaseModel = new StorePurchaseModel(user.Name, totalPrice, products);

            store.logPurchase(storePurchaseModel);

            List<Guid> notificationsUsers = new List<Guid>();
            foreach (string username in store.Permissions.Keys)
            {
                notificationsUsers.Add(_userManagement.getUserByName(username).Guid);
            }
            _communication.SendGroupNotification(notificationsUsers, new PurchaseNotification(storePurchaseModel.Username, store.Name));
        }

        public IDictionary<string, PermissionModel> getUserPermissions(Guid userID)
        {
            var user = _userManagement.getUserByGUID(userID);
            var dict = _data.Stores.FetchAll().ToDictionary(s => s.Name, s => s.getPermissionByName(user.Name)).
                Where(k => k.Value != null).ToDictionary(k => k.Key, k => ModelFactory.CreatePermissions(k.Value));
            return dict;
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