using ECommerceSystem.CommunicationLayer;
using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Models;
using ECommerceSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceSystem.CommunicationLayer.notifications;
using ECommerceSystem.Models.PurchasePolicyModels;
using ECommerceSystem.Models.DiscountPolicyModels;
using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.Exceptions;
using ECommerceSystem.Models.notifications;

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
            _data.Stores.UncachStore(_data.Stores.FindOneBy(s => s.Name.Equals(storeName)));
            var permissions = _data.Stores.FetchAll().ToList().Find(store => store.Name.Equals(storeName)).StorePermissions;
            foreach (var user in permissions.Keys)
            {
                var userPermissions = permissions[user];
                var condition = owner ? permissions[user].isOwner() : !permissions[user].isOwner();
                if (condition)
                {
                    roleHolders.Add((_userManagement.getUserByName(user), userPermissions));
                }
            }
            return roleHolders.Select(holder => (ModelFactory.CreateUser(holder.Item1), ModelFactory.CreatePermissions(holder.Item2)));
        }

        public (IEnumerable<(UserModel, PermissionModel)>, string) getStoreOwners(Guid userID, string storeName)
        {
            User user = isUserIDSubscribed(userID);
            if (user == null) //The logged in user isn`t subscribed
            {
                return (null, null);
            }
            return (getRoleHolders(storeName, true), storeName);
        }

        public (IEnumerable<(UserModel, PermissionModel)>, string) getStoreManagers(Guid userID, string storeName)
        {
            User user = isUserIDSubscribed(userID);
            if (user == null) //The logged in user isn`t subscribed
            {
                return (null, null);
            }
            return (getRoleHolders(storeName, false), storeName);
        }

        // Return the user that logged in to the system if the user is subscribed
        // If the user isn`t subscribed return null
        private User isUserIDSubscribed(Guid userID)
        {
            var user = _userManagement.getUserByGUID(userID, false);
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

            Store newStore = new Store(activeUser.Name, name); 

            try
            {
                Permissions permissions = Permissions.CreateOwner(null, newStore);
                newStore.addOwner(activeUser.Name, permissions);
                _userManagement.addPermission(activeUser, permissions, newStore.Name);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Logic error: faild to open store");
            }

             _data.Transactions.OpenStoreTransaction(activeUser, newStore);
            return true;
            


        }

        //*********Add, Delete, Modify Products*********

        //@pre - userID exist and subscribed
        //return product(not product inventory!) id, return Guid.Empty in case of fail
        public Guid addProductInv(Guid userID, string storeName, string description, string productInvName, double price, int quantity, Category categoryName, List<string> keywords, int minQuantity, int maxQuantity, string imageUrl)
        {
            Guid result;
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
            try
            {
                result = permission.addProductInv(activeUser.Name, productInvName, description, price, quantity, categoryName, keywords, minQuantity, maxQuantity, imageUrl);
            }
            catch(Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Logic error: faild to add product inventory");
            }
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

            try
            {
                return permission.addProduct(activeUser.Name, productInvName, quantity, minQuantity, maxQuantity);
            }
            catch(Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Logic error: faild to add product");
            }
            
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

            try
            {
                return permission.deleteProductInventory(activeUser.Name, productInvName);
            }
            catch(Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Logic error: faild to delete product inventory");
            }
           
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

            try
            {
                return permission.deleteProduct(activeUser.Name, productInvName, productID);
            }
            catch(Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Logic error: faild to delete product");
            }
            
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

            try
            {
                return permission.modifyProductName(activeUser.Name, newProductName, oldProductName);
            }
            catch(Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Logic error: faild to modify product name");
            }           
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

            try
            {
                return permission.modifyProductPrice(activeUser.Name, productInvName, newPrice);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Logic error: faild to modify product price");
            }          
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

            try
            {
                return permission.modifyProductQuantity(activeUser.Name, productInvName, productID, newQuantity);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Logic error: faild to modify product quantity");
            }
        }


        public List<PurchasePolicyModel> getAllPurchasePolicyByStoreName(Guid userid, string storeName)
        {
            User activeUser = isUserIDSubscribed(userid);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return null;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return null;
            }

            return permission.getAllPurchasePolicyByStoreName();

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

        //@return id of the agreement
        public Guid createOwnerAssignAgreement(Guid userID, string newOwneruserName, string storeName)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return Guid.Empty;
            }

            if (!_userManagement.isSubscribed(newOwneruserName)) //newOwneruserName isn`t subscribed
            {
                return Guid.Empty;
            }
            var store = getStoreByName(storeName);

            if (store == null)
            {
                return Guid.Empty;
            }
            AssignOwnerAgreement agreement;
            try
            {
                agreement = store.createOwnerAssignAgreement(activeUser, newOwneruserName);
                if (agreement == null)
                {
                    return Guid.Empty;
                }
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Logic error: faild to create owner assigner agreement");
            }
            var request = new OwnerAssignRequest(activeUser.Name, newOwneruserName, storeName, agreement.ID, Protocol.AssignRequest);
            List<Guid> approvers = new List<Guid>();
            foreach(string approverUserName in agreement.PendingApproval)
            {
                User approver = _userManagement.getUserByName(approverUserName);
                approvers.Add(approver.Guid);
                approver.addUserRequest(request);
                _data.Users.Update(approver, approver.Guid, u => u.Guid);
            }
            _data.Stores.Update(store, store.Name, s => s.Name);

            if(approvers.Count != 0)
            {
                _communication.SendGroupNotificationRequest(approvers, request);
            }
            else
            {
                if(!assignOwnerAftterApproval(activeUser, newOwneruserName, storeName)) //there is only 1 owner in the store(activeUser), so there is no need to approve
                    return Guid.Empty;
            }

            return agreement.ID;
        }

        public bool approveAssignOwnerRequest(Guid userID, Guid agreementID, string storeName)
        {
            Store store = getStoreByName(storeName);
            if (store == null)
            {
                return false;
            }

            AssignOwnerAgreement assignOwnerAgreement = store.getAgreementByID(agreementID);
            if (assignOwnerAgreement == null)
            {
                return false;
            }

            User approver = _userManagement.getUserByGUID(userID, false);
            if(approver == null)
            {
                return false;
            }
            try
            {
                if (!store.approveAssignOwnerRequest(approver.Name, assignOwnerAgreement))
                {
                    return false;
                }

                approver.removeUserRequest(agreementID);
                _data.Transactions.ApplyRolePermissionsTransaction(approver, store);

                if (assignOwnerAgreement.isDone())
                {
                    return assignOwnerAftterApproval(_userManagement.getUserByGUID(assignOwnerAgreement.AssignerID, true), assignOwnerAgreement.AsigneeUserName, storeName);
                }

                return true;
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Logic error: faild to create approve owner request");
            }
        }

        public bool disApproveAssignOwnerRequest(Guid userID, Guid agreementID, string storeName)
        {
            Store store = getStoreByName(storeName);
            if (store == null)
            {
                return false;
            }

            AssignOwnerAgreement assignOwnerAgreement = store.getAgreementByID(agreementID);
            if (assignOwnerAgreement == null)
            {
                return false;
            }

            User disapprover = _userManagement.getUserByGUID(userID, true);

            try
            {
                if (!store.disapproveAssignOwnerRequest(disapprover.Name, assignOwnerAgreement))
                {
                    return false;
                }

                //remove the agreement request from all the pending owners:
                foreach (string ownerUserName in assignOwnerAgreement.PendingApproval)
                {
                    var user = _userManagement.getUserByName(ownerUserName);
                    user.removeUserRequest(agreementID);
                    _data.Transactions.ApplyRolePermissionsTransaction(user, store);
                }

                //send disapprove notification to the assignee and assigner
                INotitficationType notitfication = new DisappvoveAssignOwnerNotification(assignOwnerAgreement.AsigneeUserName, disapprover.Name, storeName);
                _communication.SendPrivateNotification(assignOwnerAgreement.AssignerID, notitfication);
                _communication.SendPrivateNotification(_userManagement.getUserByName(assignOwnerAgreement.AsigneeUserName).Guid, notitfication);
                return true;
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Logic error: faild to  disapprove owner request");
            }
        }


        private bool assignOwnerAftterApproval(User assigner, string newOwneruserName, string storeName)
        {
            Permissions newOwmerPer = getStoreByName(storeName).assignOwner(assigner, newOwneruserName);
            //add the permissions object to the user
            if (newOwmerPer != null)
            {
                User assigneeUser = _userManagement.getUserByName(newOwneruserName);
                _userManagement.addPermission(assigneeUser, newOwmerPer, storeName);
                _userManagement.addAssignee(assigner, storeName, assigneeUser.Guid);
                _data.Transactions.AssignOwnerManagerTransaction(assigner, assigneeUser, (Store)newOwmerPer.Store);
                _communication.SendPrivateNotification(assigneeUser.Guid, new AssignOwnerNotification(newOwneruserName, assigner.Name, storeName));
                return true;
            }
            else
                return false;
        }

        public bool removeOwner(Guid activeUserID, string ownerToRemoveUserName, string storeName)
        {
            User activeUser = isUserIDSubscribed(activeUserID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return false;
            }
            if (!_userManagement.isSubscribed(ownerToRemoveUserName)) //ownerToRemoveUserName isn`t subscribed
            {
                return false;
            }
            User toRevmoe = _userManagement.getUserByName(ownerToRemoveUserName);
            bool output = removeOwnerRec(activeUser, toRevmoe, storeName);

            if (output)
            {
                
                var result = _userManagement.removeAssignee(activeUser, storeName, toRevmoe.Guid);
                if(result)
                {
                    var store = getStoreByName(storeName);

                    _data.Transactions.ApplyRolePermissionsTransaction(toRevmoe, store);
                    _data.Transactions.ApplyRolePermissionsTransaction(activeUser, store);
                }
            }
            return output;
        }


        private bool removeOwnerRec(User removerUser, User toRemove, string storeName)
        {
            bool output = true;
            Permissions removerUserPermissions = removerUser.getPermission(storeName);

            if (removerUserPermissions == null)
            {
                return false;
            }

            if (removerUserPermissions.removeOwner(removerUser.Guid, toRemove.Name))
            {
                //remove all the owners\managers that the removed owner assign
                List<Guid> assignedByRemovedOwner = _userManagement.getAssigneesOfStore(toRemove, storeName); // list of the owners and managers that the removed owner assign
                if (assignedByRemovedOwner != null)
                {
                    foreach (Guid assigneeID in assignedByRemovedOwner)
                    {
                        User assigneeUser = _userManagement.getUserByGUID(assigneeID, true);
                        Permissions asigneePermissions = assigneeUser.getPermission(storeName);

                        if (asigneePermissions.isOwner())
                        {
                            output = output && removeOwnerRec(toRemove, _userManagement.getUserByGUID(assigneeID, true), storeName);
                        }
                        else
                        {
                            output = output && removeManager(toRemove.Guid, assigneeUser.Name, storeName);
                        }
                    }
                    _userManagement.removeAllAssigneeOfStore(toRemove, storeName);
                }
                _userManagement.removePermissions(storeName, toRemove); //remove permissions object from the user 
                _data.Transactions.ApplyRolePermissionsTransaction(toRemove, getStoreByName(storeName));

                //-------------------------------------------------------------------------
                return output;
            }
            else return false;
        }

        //@pre - userID exist and subscribed
        public bool assignManager(Guid userID, string newManageruserName, string storeName)
        {
            User activeUser = isUserIDSubscribed(userID);
            Permissions newManagerPer;
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
            try
            {
                newManagerPer = activeUserPermissions.assignManager(activeUser, newManageruserName);
                if (newManagerPer != null)
                {
                    // Add the permission to the new manager
                    User assigneeUser = _userManagement.getUserByName(newManageruserName);
                    _userManagement.addPermission(assigneeUser, newManagerPer, storeName);
                    _userManagement.addAssignee(activeUser, storeName, assigneeUser.Guid);
                    var store = (Store)newManagerPer.Store;
                    _data.Transactions.AssignOwnerManagerTransaction(activeUser, assigneeUser, store);

                    _communication.SendPrivateNotification(assigneeUser.Guid, new AssignManagerNotification(newManageruserName, activeUser.Name, storeName));
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Logic error: faild to  assign manager");
            }
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
            try
            {
                bool isSuccess = permission == null ? false : permission.removeManager(activeUser, managerUserName);
                if (isSuccess)
                {
                    // Remove the permission from the user
                    User toRemoveUser = _userManagement.getUserByName(managerUserName);
                    _userManagement.removePermissions(storeName, toRemoveUser);
                    _userManagement.removeAssignee(activeUser, storeName, toRemoveUser.Guid);
                    var store = (Store)permission.Store;
                    _data.Transactions.AssignOwnerManagerTransaction(activeUser, toRemoveUser, store);
                    _communication.SendPrivateNotification(toRemoveUser.Guid, new RemoveManagerNotification(managerUserName, activeUser.Name, storeName));
                    return true;
                }
                else
                    return false;
            }
            catch(Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Faild : remove manager");
            }

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

            try
            {
                var newUserPermissions = permission.editPermissions(managerUserName, permissiosnNames, activeUser.Name);
                if(newUserPermissions != null)
                {
                    var store = (Store)permission.Store;
                    var user = _userManagement.getUserByName(managerUserName);
                    ((Subscribed)user.State).Permissions[storeName] = newUserPermissions;
                    _data.Transactions.ApplyRolePermissionsTransaction(user, store);
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Faild : edit permissions");
            }
            
        }

        public Tuple<StoreModel, List<ProductModel>> getStoreProductGroup(Guid productInvID, string storeName)
        {
            var result = new List<ProductModel>();
            _data.Stores.UncachStore(_data.Stores.FindOneBy(s => s.Name.Equals(storeName)));
            var storeInfo = _data.Stores.FindOneBy(s => s.Name.Equals(storeName)).getStoreInfo();
            storeInfo.Item2.ForEach(prod => {
                if (prod.ID == productInvID)
                    prod.ProductList.ForEach(p => result.Add(ModelFactory.CreateProduct(p, prod.ImageUrl)));
            });
            return Tuple.Create(ModelFactory.CreateStore(storeInfo.Item1), result);
        }

        public void rateProduct(Guid id, int rating)
        {
            var store = _data.Stores.FindOneBy(s => s.Inventory.Products.Any(p => p.ID == id));
            var products = store.Inventory.Products;
            products.ForEach(p =>
            {
                if (p.ID == id)
                    p.rateProduct(rating);
            });
            _data.Stores.Update(store, store.Name, s => s.Name);
        }

        public void rateStore(string storeName, int rating)
        {
            var store = _data.Stores.GetByIdOrNull(storeName, s => s.Name);
            store.rateStore(rating);
            _data.Stores.Update(store, store.Name, s => s.Name);
        }

        public Tuple<StoreModel, List<ProductInventoryModel>> getStoreProducts(string storeName)
        {
            var info = _data.Stores.FindOneBy(s => s.Name.Equals(storeName)).getStoreInfo();
            return Tuple.Create(ModelFactory.CreateStore(info.Item1), info.Item2.Select(p => ModelFactory.CreateProductInventory(p, storeName)).ToList());
        }

        public Dictionary<StoreModel, List<ProductInventoryModel>> getAllStoresProducts()
        {
            var storeProdcuts = new Dictionary<StoreModel, List<ProductInventoryModel>>();
            _data.Stores.FetchAll().ToList().ForEach(s =>
                {
                    var storeInfo = s.getStoreInfo();
                    storeProdcuts.Add(ModelFactory.CreateStore(storeInfo.Item1),
                            storeInfo.Item2.Select(p => ModelFactory.CreateProductInventory(p, storeInfo.Item1.Name)).ToList());
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

        public (ProductInventoryModel, string) getProductInventory(Guid prodID)
        {
            var stores = _data.Stores.FetchAll();
            foreach (Store store in stores)
            {
                foreach (var prodInv in store.Inventory.Products)
                {
                    if (prodInv.ID.Equals(prodID))
                        return (ModelFactory.CreateProductInventory(prodInv, store.Name), store.Name);
                    
                }
            }
            return (null, null);
        }

        public List<ProductInventory> getAllStoreInventoryWithRating(Range<double> storeRatingFilter)
        {
            var allProducts = new List<ProductInventory>();
            var stores = _data.Stores.FindAllBy(s => s.Rating.CompareTo(storeRatingFilter.min) >= 0 && s.Rating.CompareTo(storeRatingFilter.max) <= 0);
            foreach (Store store in stores)
            {
                allProducts = allProducts.Concat(store.Inventory.Products).ToList();
            }
            return allProducts;
        }

        public IEnumerable<StorePurchaseModel> purchaseHistory(Guid userID, string storeName)
        {
            _data.Stores.UncachStore(_data.Stores.FindOneBy(s => s.Name.Equals(storeName)));
            _data.Users.UncacheUser(_userManagement.getUserByGUID(userID, false));
            User activeUser = _userManagement.getUserByGUID(userID, false);
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
            List<ProductModel> products = storeBoughtProducts.Select(prod => 
            new ProductModel(prod.Key.Id, prod.Key.Name, prod.Key.Description,
            prod.Value, prod.Key.BasePrice,
            prod.Key.Discount != null ? prod.Key.Discount.CreateModel() : null,
            prod.Key.PurchasePolicy != null ? prod.Key.PurchasePolicy.CreateModel() : null)).ToList();
            StorePurchaseModel storePurchaseModel = new StorePurchaseModel(user.Name, totalPrice, products, DateTime.Now);

            store.logPurchase(storePurchaseModel);
            _data.Stores.Update(store, store.Name, s => s.Name);
        }

        public void sendPurchaseNotification(Store store, string username)
        {
            List<Guid> notificationsUsers = new List<Guid>();
            foreach (string manager in store.StorePermissions.Keys) //for each owner/manager
            {
                notificationsUsers.Add(_userManagement.getUserByName(manager).Guid);
            }
            _communication.SendGroupNotification(notificationsUsers, new PurchaseNotification(username, store.Name));
        }

        public IDictionary<string, PermissionModel> getUserPermissions(Guid userID)
        {
            var user = _userManagement.getUserByGUID(userID, true);
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

            try
            {
                return permission.addDayOffPolicy(daysOff);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Faild : Add Day Off Policy");
            }

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

            try
            {
                return permission.addLocationPolicy(banLocations);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Faild : add Location Policy");
            }
            
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

            try
            {
                return permission.addMinPriceStorePolicy(minPrice);
            }
            
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Faild : add Min Price Store Policy");
            }
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

            try
            {
                return permission.addAndPurchasePolicy(ID1, ID2);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Faild : add And Purchase Policy");
            }
            
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

            try
            {
                return permission.addOrPurchasePolicy(ID1, ID2);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Faild : add OR Purchase Policy");
            }
            
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

            try
            {
                return permission.addXorPurchasePolicy(ID1, ID2);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Faild : add Xor Purchase Policy");
            }
            
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

            try
            {
                permission.removePurchasePolicy(policyID);
                return true;
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Faild : remove Purchase Policy");
            }

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

            try
            {
                return permission.addVisibleDiscount(productID, percentage, expDate);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Faild : add Visible Discount");
            }

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

            try
            {
                return permission.addCondiotionalProcuctDiscount(productID, percentage, expDate, minQuantityForDiscount);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Faild : add Condiotional Procuct Discount");
            }

        }

        public Guid addConditionalCompositeProcuctDiscount(Guid userID, string storeName, Guid productID, float percentage, DateTime expDate, CompositeDiscountPolicyModel conditionalTree)
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


            try
            {
                return permission.addConditionalCompositeProcuctDiscount(productID, percentage, expDate, conditionalTree);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Faild : add composite condiotional Procuct Discount");
            }

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

            try
            {
                return permission.addConditionalStoreDiscount(percentage, expDate, minPriceForDiscount);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Faild : add Conditional Store Discount");
            }

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

            try
            {
                return permission.addAndDiscountPolicy(IDs);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Faild : add And Discount Policy");
            }

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

            try
            {
                return permission.addOrDiscountPolicy(IDs);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Faild : add Or Discount Policy");
            }

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

            try
            {
                return permission.addXorDiscountPolicy(IDs);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Faild : add Xor Discount Policy");
            }

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

            try
            {
                return permission.removeProductDiscount(discountID, productID);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Faild : remove Product Discount");
            }

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

            try
            {
                return permission.removeCompositeDiscount(discountID);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Faild : remove Composite Discount");
            }
            
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

            try
            {
                return permission.removeStoreLevelDiscount(discountID);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new LogicException("Faild : remove Store Level Discount");
            }


        }


        public List<DiscountPolicyModel> getAllStoreLevelDiscounts(Guid userID, string storeName)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return null;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return null;
            }

            return permission.getAllStoreLevelDiscounts();
        }

        public List<DiscountPolicyModel> getAllDiscountsForCompose(Guid userID, string storeName)
        {
            User activeUser = isUserIDSubscribed(userID);
            if (activeUser == null) //The logged in user isn`t subscribed
            {
                return null;
            }
            Permissions permission = activeUser.getPermission(storeName);
            if (permission == null)
            {
                return null;
            }

            return permission.getAllDiscountsForCompose();
        }

        public ICollection<Store> getAllStores()
        {
            return _data.Stores.FetchAll();
        }
    }
}