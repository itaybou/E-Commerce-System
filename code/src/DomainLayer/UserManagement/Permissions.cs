using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.StoresManagement.Discount;
using ECommerceSystem.Models;
using ECommerceSystem.Models.DiscountPolicyModels;
using ECommerceSystem.Models.PurchasePolicyModels;

namespace ECommerceSystem.DomainLayer.UserManagement
{

    public class Permissions : IStoreInterface
    {
        private User _assignedBy;
        private Dictionary<PermissionType, bool> _permissions;
        private bool _isOwner;
        private IStoreInterface _store;


        public User AssignedBy { get => _assignedBy; }
        public IStoreInterface Store { get => _store; set => _store = value; }
        public Dictionary<PermissionType, bool> PermissionTypes { get => _permissions; set => _permissions = value; }

        private Permissions(User assignedBy, bool isOwner, Store store, Dictionary<PermissionType, bool> permissions = null)
        {
            this._assignedBy = assignedBy;
            this._permissions = permissions;
            initPermmisionsDict(isOwner);
            this._isOwner = isOwner;
            this._store = store;
        }

        public static Permissions CreateOwner(User assignedBy, Store store)
        {
            if (assignedBy == null || assignedBy.isSubscribed()) //null if this is the user who open the store
            {
                Permissions permissions = new Permissions(assignedBy, true, store);
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
                Permissions permissions = new Permissions(assignedBy, false, store);
                return permissions;
            }
            else
            {
                return null;
            }
        }

        private void initPermmisionsDict(bool isOwner)
        {
            _permissions = new Dictionary<PermissionType, bool>();
            _permissions[PermissionType.AddProductInv] = isOwner;
            _permissions[PermissionType.DeleteProductInv] = isOwner;
            _permissions[PermissionType.ModifyProduct] = isOwner;
            _permissions[PermissionType.ManagePurchasePolicy] = isOwner;
            _permissions[PermissionType.ManageDiscounts] = isOwner;
            _permissions[PermissionType.WatchPurchaseHistory] = true;   // defualt for manager
            _permissions[PermissionType.WatchAndComment] = true;     // default for manager
        }

        public void makeOwner()
        {
            foreach (KeyValuePair<PermissionType, bool> per in _permissions)
            {
                _permissions[per.Key] = true;
            }
            this._isOwner = true;
        }

        public bool isOwner()
        {
            return _isOwner;
        }

        public bool canAddProduct()
        {
            return _permissions[PermissionType.AddProductInv];
        }

        public bool canDeleteProduct()
        {
            return _permissions[PermissionType.DeleteProductInv];
        }

        public bool canModifyProduct()
        {
            return _permissions[PermissionType.ModifyProduct];
        }

        public bool canWatchPurchaseHistory()
        {
            return _permissions[PermissionType.WatchPurchaseHistory];
        }

        public bool canWatchAndomment()
        {
            return _permissions[PermissionType.WatchAndComment];
        }

        public void edit(List<PermissionType> permissions)
        {
            // reset all permissions to false
            for (int i = 0; i < _permissions.Count; i++)
            {
                PermissionType per = _permissions.ElementAt(i).Key;
                _permissions[per] = permissions.Contains(per);
            }
        }

        public bool canWatchHistory()
        {
            return _permissions[PermissionType.WatchPurchaseHistory];
        }

        public bool canManagePurchasePolicy()
        {
            return _permissions[PermissionType.ManagePurchasePolicy];
        }

        public bool canManageDiscounts()
        {
            return _permissions[PermissionType.ManageDiscounts];
        }

        public Guid addProductInv(string activeUserName, string productName, string description,  double price, int quantity, Category category, List<string> keywords, int minQuantity, int maxQuantity)
        {
            if (this.canAddProduct())
            {
                return _store.addProductInv(activeUserName, productName, description, price, quantity, category, keywords, minQuantity, maxQuantity);
            }
            else return Guid.Empty;
        }


        public Guid addProduct(string loggedInUserName, string productInvName, int quantity, int minQuantity, int maxQuantity)
        {
            if (this.canModifyProduct())
            {
                return _store.addProduct(loggedInUserName, productInvName, quantity, minQuantity, maxQuantity);
            }
            else return Guid.Empty;
        }

        public bool deleteProductInventory(string loggedInUserName, string productInvName)
        {
            if (this.canDeleteProduct())
            {
                return _store.deleteProductInventory(loggedInUserName, productInvName);
            }
            else return false;
        }

        public bool deleteProduct(string loggedInUserName, string productInvName, Guid productID)
        {
            if (this.canModifyProduct())
            {
                return _store.deleteProduct(loggedInUserName, productInvName, productID);
            }
            else return false;
        }

        public bool modifyProductPrice(string loggedInUserName, string productName, int newPrice)
        {
            if (this.canModifyProduct())
            {
                return _store.modifyProductPrice(loggedInUserName, productName, newPrice);
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
                return _store.modifyProductQuantity(loggedInUserName, productName, productID, newQuantity);
            }
            else return false;
        }

        public bool modifyProductName(string loggedInUserName, string newProductName, string oldProductName)
        {
            if (this.canModifyProduct())
            {
                return _store.modifyProductName(loggedInUserName, newProductName, oldProductName);
            }
            else return false;
        }

        public Permissions assignOwner(User loggedInUser, string newOwneruserName)
        {
            if (this.isOwner())
            {
                return _store.assignOwner(loggedInUser, newOwneruserName);
            }
            else return null;
        }

        public Permissions assignManager(User loggedInUser, string newManageruserName)
        {
            if (this.isOwner())
            {
                return _store.assignManager(loggedInUser, newManageruserName);
            }
            else return null;
        }

        public bool removeManager(User loggedInUser, string managerUserName)
        {
            if (this.isOwner())
            {
                return _store.removeManager(loggedInUser, managerUserName);
            }
            else return false;
        }

        public bool editPermissions(string managerUserName, List<PermissionType> permissions, string loggedInUserName)
        {
            if (this.isOwner())
            {
                return _store.editPermissions(managerUserName, permissions, loggedInUserName);
            }
            else return false;
        }


        public void rateStore(double rating)
        {
            _store.rateStore(rating);
        }

        public void logPurchase(StorePurchaseModel purchase)
        {
            _store.logPurchase(purchase);
        }

        public Permissions getPermissionByName(string userName)
        {
            throw new NotImplementedException();
        }

        public List<StorePurchaseModel> purchaseHistory()
        {
            if (this.canWatchHistory())
            {
                return _store.purchaseHistory();
            }
            else return null;
        }

        public Guid addDayOffPolicy(List<DayOfWeek> daysOff)
        {
            if (this.canManagePurchasePolicy())
            {
                return _store.addDayOffPolicy(daysOff);
            }
            else return Guid.Empty;
        }

        public void removePurchasePolicy(Guid policyID)
        {
            if (this.canManagePurchasePolicy())
            {
                _store.removePurchasePolicy(policyID);
                
            }    
        }

        public Guid addLocationPolicy(List<string> banLocations)
        {
            if (this.canManagePurchasePolicy())
            {
                return _store.addLocationPolicy(banLocations);

            }
            else return Guid.Empty;
        }

        public List<PurchasePolicyModel> getAllPurchasePolicyByStoreName()
        {
            if (this.canManagePurchasePolicy())
            {
                return _store.getAllPurchasePolicyByStoreName();

            }
            else return null;
        }

        public Guid addMinPriceStorePolicy(double minPrice)
        {
            if (this.canManagePurchasePolicy())
            {
                return _store.addMinPriceStorePolicy(minPrice);

            }
            else return Guid.Empty;
        }

        public Guid addAndPurchasePolicy(Guid iD1, Guid iD2)
        {
            if (this.canManagePurchasePolicy())
            {
                return _store.addAndPurchasePolicy(iD1, iD2);
            }
            else return Guid.Empty;
        }

        public Guid addOrPurchasePolicy(Guid iD1, Guid iD2)
        {
            if (this.canManagePurchasePolicy())
            {
                return _store.addOrPurchasePolicy(iD1, iD2);
            }
            else return Guid.Empty;
        }

        public Guid addXorPurchasePolicy(Guid iD1, Guid iD2)
        {
            if (this.canManagePurchasePolicy())
            {
                return _store.addXorPurchasePolicy(iD1, iD2);
            }
            else return Guid.Empty;
        }

        public Guid addVisibleDiscount(Guid productID, float percentage, DateTime expDate)
        {
            if (this.canManageDiscounts())
            {
                return _store.addVisibleDiscount(productID, percentage, expDate);
            }
            else return Guid.Empty;
        }

        public Guid addCondiotionalProcuctDiscount(Guid productID, float percentage, DateTime expDate, int minQuantityForDiscount)
        {
            if (this.canManageDiscounts())
            {
                return _store.addCondiotionalProcuctDiscount(productID, percentage, expDate, minQuantityForDiscount);
            }
            else return Guid.Empty;
        }

        public Guid addConditionalStoreDiscount(float percentage, DateTime expDate, int minPriceForDiscount)
        {
            if (this.canManageDiscounts())
            {
                return _store.addConditionalStoreDiscount(percentage, expDate, minPriceForDiscount);
            }
            else return Guid.Empty;
        }

        public Guid addAndDiscountPolicy(List<Guid> IDS)
        {
            if (this.canManageDiscounts())
            {
                return _store.addAndDiscountPolicy(IDS);
            }
            else return Guid.Empty;
        }

        public Guid addOrDiscountPolicy(List<Guid> IDs)
        {
            if (this.canManageDiscounts())
            {
                return _store.addOrDiscountPolicy(IDs);
            }
            else return Guid.Empty;
        }

        public Guid addXorDiscountPolicy(List<Guid> IDs)
        {
            if (this.canManageDiscounts())
            {
                return _store.addXorDiscountPolicy(IDs);
            }
            else return Guid.Empty;
        }

        public bool removeProductDiscount(Guid discountID, Guid productID)
        {
            if (this.canManageDiscounts())
            {
                return _store.removeProductDiscount(discountID, productID);
            }
            else return false;
        }

        public bool removeCompositeDiscount(Guid discountID)
        {
            if (this.canManageDiscounts())
            {
                return _store.removeCompositeDiscount(discountID);
            }
            else return false;
        }

        public bool removeStoreLevelDiscount(Guid discountID)
        {
            if (this.canManageDiscounts())
            {
                return _store.removeCompositeDiscount(discountID);
            }
            else return false;
        }

        public string StoreName()
        {
            if (_store is Store)
            {
                return _store.StoreName();
            } return null;
        }

        public List<DiscountPolicyModel> getAllStoreLevelDiscounts()
        {
            if (this.canManageDiscounts())
            {
                return _store.getAllStoreLevelDiscounts();
            }
            else return null;
        }

        public List<DiscountPolicyModel> getAllDiscountsForCompose()
        {
            if (this.canManageDiscounts())
            {
                return _store.getAllDiscountsForCompose();
            }
            else return null;
        }
    }
}