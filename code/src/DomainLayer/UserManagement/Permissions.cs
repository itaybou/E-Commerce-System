using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.DataAccessLayer.serializers;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.Models;
using ECommerceSystem.Models.DiscountPolicyModels;
using ECommerceSystem.Models.PurchasePolicyModels;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public class Permissions : IStoreInterface
    {
        public Guid AssignedBy { get; set; }
        public bool IsOwner { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<PermissionType, bool> PermissionTypes { get; set; }

        [BsonIgnore]
        public IStoreInterface Store { get; set; }

        private Permissions(Guid assignedBy, bool isOwner, Store store, Dictionary<PermissionType, bool> permissions = null)
        {
            this.AssignedBy = assignedBy;
            this.PermissionTypes = permissions;
            initPermmisionsDict(isOwner);
            this.IsOwner = isOwner;
            this.Store = store;
        }

        public static Permissions CreateOwner(User assignedBy, Store store)
        {
            if (assignedBy == null || assignedBy.isSubscribed()) //null if this is the user who open the store
            {
                var assignerID = assignedBy == null ? Guid.Empty : assignedBy.Guid;
                Permissions permissions = new Permissions(assignerID, true, store);
                return permissions;
            }
            else
            {
                return null;
            }
        }

        public static Permissions CreateManager(User assignedBy, Store store)
        {
            if (assignedBy.isSubscribed())
            {
                var assignerID = assignedBy == null ? Guid.Empty : assignedBy.Guid;
                Permissions permissions = new Permissions(assignerID, false, store);
                return permissions;
            }
            else
            {
                return null;
            }
        }

        private void initPermmisionsDict(bool isOwner)
        {
            PermissionTypes = new Dictionary<PermissionType, bool>();
            PermissionTypes[PermissionType.AddProductInv] = isOwner;
            PermissionTypes[PermissionType.DeleteProductInv] = isOwner;
            PermissionTypes[PermissionType.ModifyProduct] = isOwner;
            PermissionTypes[PermissionType.ManagePurchasePolicy] = isOwner;
            PermissionTypes[PermissionType.ManageDiscounts] = isOwner;
            PermissionTypes[PermissionType.WatchPurchaseHistory] = true;   // defualt for manager
            PermissionTypes[PermissionType.WatchAndComment] = true;     // default for manager
        }

        public void makeOwner()
        {
            foreach (KeyValuePair<PermissionType, bool> per in PermissionTypes)
            {
                PermissionTypes[per.Key] = true;
            }
            this.IsOwner = true;
        }

        public bool isOwner()
        {
            return IsOwner;
        }

        public bool canAddProduct()
        {
            return PermissionTypes[PermissionType.AddProductInv];
        }

        public bool canDeleteProduct()
        {
            return PermissionTypes[PermissionType.DeleteProductInv];
        }

        public bool canModifyProduct()
        {
            return PermissionTypes[PermissionType.ModifyProduct];
        }

        public bool canWatchPurchaseHistory()
        {
            return PermissionTypes[PermissionType.WatchPurchaseHistory];
        }

        public bool canWatchAndomment()
        {
            return PermissionTypes[PermissionType.WatchAndComment];
        }

        public void edit(List<PermissionType> permissions)
        {
            // reset all permissions to false
            for (int i = 0; i < PermissionTypes.Count; i++)
            {
                PermissionType per = PermissionTypes.ElementAt(i).Key;
                PermissionTypes[per] = permissions.Contains(per);
            }
        }

        public bool canWatchHistory()
        {
            return PermissionTypes[PermissionType.WatchPurchaseHistory];
        }

        public bool canManagePurchasePolicy()
        {
            return PermissionTypes[PermissionType.ManagePurchasePolicy];
        }

        public bool canManageDiscounts()
        {
            return PermissionTypes[PermissionType.ManageDiscounts];
        }

        public Guid addProductInv(string activeUserName, string productName, string description, double price, int quantity, Category category, List<string> keywords, int minQuantity, int maxQuantity, string imageUrl)
        {
            if (this.canAddProduct())
            {
                return Store.addProductInv(activeUserName, productName, description, price, quantity, category, keywords, minQuantity, maxQuantity, imageUrl);
            }
            else return Guid.Empty;
        }

        public Guid addProduct(string loggedInUserName, string productInvName, int quantity, int minQuantity, int maxQuantity)
        {
            if (this.canModifyProduct())
            {
                return Store.addProduct(loggedInUserName, productInvName, quantity, minQuantity, maxQuantity);
            }
            else return Guid.Empty;
        }

        public bool deleteProductInventory(string loggedInUserName, string productInvName)
        {
            if (this.canDeleteProduct())
            {
                return Store.deleteProductInventory(loggedInUserName, productInvName);
            }
            else return false;
        }

        public bool deleteProduct(string loggedInUserName, string productInvName, Guid productID)
        {
            if (this.canModifyProduct())
            {
                return Store.deleteProduct(loggedInUserName, productInvName, productID);
            }
            else return false;
        }

        public bool modifyProductPrice(string loggedInUserName, string productName, int newPrice)
        {
            if (this.canModifyProduct())
            {
                return Store.modifyProductPrice(loggedInUserName, productName, newPrice);
            }
            else return false;
        }

        //public bool modifyProductDiscountType(string loggedInUserName, string productInvName, Guid productID, DiscountType newDiscount)
        //{
        //    if (this.canModifyProduct())
        //    {
        //        return _store.modifyProductDiscountType(loggedInUserName, productInvName, productID, newDiscount);
        //    }
        //    else return false;
        //}

        //public bool modifyProductPurchaseType(string loggedInUserName, string productInvName, Guid productID, PurchaseType purchaseType)
        //{
        //    if (this.canModifyProduct())
        //    {
        //        return _store.modifyProductPurchaseType(loggedInUserName, productInvName, productID, purchaseType);
        //    }
        //    else return false;
        //}

        public bool modifyProductQuantity(string loggedInUserName, string productName, Guid productID, int newQuantity)
        {
            if (this.canModifyProduct())
            {
                return Store.modifyProductQuantity(loggedInUserName, productName, productID, newQuantity);
            }
            else return false;
        }

        public bool modifyProductName(string loggedInUserName, string newProductName, string oldProductName)
        {
            if (this.canModifyProduct())
            {
                return Store.modifyProductName(loggedInUserName, newProductName, oldProductName);
            }
            else return false;
        }

        public Permissions assignOwner(User loggedInUser, string newOwneruserName)
        {
            if (this.isOwner())
            {
                return Store.assignOwner(loggedInUser, newOwneruserName);
            }
            else return null;
        }

        public Permissions assignManager(User loggedInUser, string newManageruserName)
        {
            if (this.isOwner())
            {
                return Store.assignManager(loggedInUser, newManageruserName);
            }
            else return null;
        }

        public bool removeManager(User loggedInUser, string managerUserName)
        {
            if (this.isOwner())
            {
                return Store.removeManager(loggedInUser, managerUserName);
            }
            else return false;
        }

        public bool editPermissions(string managerUserName, List<PermissionType> permissions, string loggedInUserName)
        {
            if (this.isOwner())
            {
                return Store.editPermissions(managerUserName, permissions, loggedInUserName);
            }
            else return false;
        }

        public void rateStore(double rating)
        {
            Store.rateStore(rating);
        }

        public void logPurchase(StorePurchaseModel purchase)
        {
            Store.logPurchase(purchase);
        }

        public Permissions getPermissionByName(string userName)
        {
            throw new NotImplementedException();
        }

        public List<StorePurchaseModel> purchaseHistory()
        {
            if (this.canWatchHistory())
            {
                return Store.purchaseHistory();
            }
            else return null;
        }

        public Guid addDayOffPolicy(List<DayOfWeek> daysOff)
        {
            if (this.canManagePurchasePolicy())
            {
                return Store.addDayOffPolicy(daysOff);
            }
            else return Guid.Empty;
        }

        public void removePurchasePolicy(Guid policyID)
        {
            if (this.canManagePurchasePolicy())
            {
                Store.removePurchasePolicy(policyID);
            }
        }

        public Guid addLocationPolicy(List<string> banLocations)
        {
            if (this.canManagePurchasePolicy())
            {
                return Store.addLocationPolicy(banLocations);
            }
            else return Guid.Empty;
        }

        public List<PurchasePolicyModel> getAllPurchasePolicyByStoreName()
        {
            if (this.canManagePurchasePolicy())
            {
                return Store.getAllPurchasePolicyByStoreName();

            }
            else return null;
        }

        public Guid addMinPriceStorePolicy(double minPrice)
        {
            if (this.canManagePurchasePolicy())
            {
                return Store.addMinPriceStorePolicy(minPrice);
            }
            else return Guid.Empty;
        }

        public Guid addAndPurchasePolicy(Guid iD1, Guid iD2)
        {
            if (this.canManagePurchasePolicy())
            {
                return Store.addAndPurchasePolicy(iD1, iD2);
            }
            else return Guid.Empty;
        }

        public Guid addOrPurchasePolicy(Guid iD1, Guid iD2)
        {
            if (this.canManagePurchasePolicy())
            {
                return Store.addOrPurchasePolicy(iD1, iD2);
            }
            else return Guid.Empty;
        }

        public Guid addXorPurchasePolicy(Guid iD1, Guid iD2)
        {
            if (this.canManagePurchasePolicy())
            {
                return Store.addXorPurchasePolicy(iD1, iD2);
            }
            else return Guid.Empty;
        }

        public Guid addVisibleDiscount(Guid productID, float percentage, DateTime expDate)
        {
            if (this.canManageDiscounts())
            {
                return Store.addVisibleDiscount(productID, percentage, expDate);
            }
            else return Guid.Empty;
        }

        public Guid addCondiotionalProcuctDiscount(Guid productID, float percentage, DateTime expDate, int minQuantityForDiscount)
        {
            if (this.canManageDiscounts())
            {
                return Store.addCondiotionalProcuctDiscount(productID, percentage, expDate, minQuantityForDiscount);
            }
            else return Guid.Empty;
        }

        public Guid addConditionalStoreDiscount(float percentage, DateTime expDate, int minPriceForDiscount)
        {
            if (this.canManageDiscounts())
            {
                return Store.addConditionalStoreDiscount(percentage, expDate, minPriceForDiscount);
            }
            else return Guid.Empty;
        }

        public AssignOwnerAgreement createOwnerAssignAgreement(User assigner, string newOwneruserName)
        {
            if (this.isOwner())
            {
                return Store.createOwnerAssignAgreement(assigner, newOwneruserName);
            }
            else return null;
        }

        public Guid addAndDiscountPolicy(List<Guid> IDS)
        {
            if (this.canManageDiscounts())
            {
                return Store.addAndDiscountPolicy(IDS);
            }
            else return Guid.Empty;
        }

        public Guid addOrDiscountPolicy(List<Guid> IDs)
        {
            if (this.canManageDiscounts())
            {
                return Store.addOrDiscountPolicy(IDs);
            }
            else return Guid.Empty;
        }

        public Guid addXorDiscountPolicy(List<Guid> IDs)
        {
            if (this.canManageDiscounts())
            {
                return Store.addXorDiscountPolicy(IDs);
            }
            else return Guid.Empty;
        }

        public bool removeProductDiscount(Guid discountID, Guid productID)
        {
            if (this.canManageDiscounts())
            {
                return Store.removeProductDiscount(discountID, productID);
            }
            else return false;
        }

        public bool removeCompositeDiscount(Guid discountID)
        {
            if (this.canManageDiscounts())
            {
                return Store.removeCompositeDiscount(discountID);
            }
            else return false;
        }

        public bool removeOwner(Guid activeUserID, string ownerToRemoveUserName)
        {
            if (this.isOwner())
            {
                return Store.removeOwner(activeUserID, ownerToRemoveUserName);
            }
            else return false;
        }

        public bool removeStoreLevelDiscount(Guid discountID)
        {
            if (this.canManageDiscounts())
            {
                return Store.removeCompositeDiscount(discountID);
            }
            else return false;
        }

        public string StoreName()
        {
            if (Store is Store)
            {
                return Store.StoreName();
            }
            return null;
        }

        public List<DiscountPolicyModel> getAllStoreLevelDiscounts()
        {
            if (this.canManageDiscounts())
            {
                return Store.getAllStoreLevelDiscounts();
            }
            else return null;
        }

        public List<DiscountPolicyModel> getAllDiscountsForCompose()
        {
            if (this.canManageDiscounts())
            {
                return Store.getAllDiscountsForCompose();
            }
            else return null;
        }
    }
}