﻿using System;
using System.Collections.Generic;
using System.Linq;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.Models;

namespace ECommerceSystem.DomainLayer.UserManagement
{

    public class Permissions : IStoreInterface
    {
        private User _assignedBy;
        private Dictionary<PermissionType, bool> _permissions;
        private bool _isOwner;
        private Store _store;


        public User AssignedBy { get => _assignedBy; }
        public Store Store { get => _store; set => _store = value; }
        public Dictionary<PermissionType, bool> PermissionTypes { get => _permissions; set => _permissions = value; }

        private Permissions(User assignedBy, bool isOwner, Store store, Dictionary<PermissionType, bool> permissions = null)
        {
            this._assignedBy = assignedBy;
            initPermmisionsDict(isOwner);
            this._isOwner = isOwner;
            this._store = store;
            _permissions = permissions;
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

        internal bool canWatchHistory()
        {
            return _permissions[PermissionType.WatchPurchaseHistory];
        }

        public Guid addProductInv(string activeUserName, string productName, string description, Discount discount, PurchaseType purchaseType, double price, int quantity, Category category, List<string> keywords)
        {
            if (this.canAddProduct())
            {
                return _store.addProductInv(activeUserName, productName, description, discount, purchaseType, price, quantity, category, keywords);
            }
            else return Guid.Empty;
        }


        public Guid addProduct(string loggedInUserName, string productInvName, Discount discount, PurchaseType purchaseType, int quantity)
        {
            if (this.canModifyProduct())
            {
                return _store.addProduct(loggedInUserName, productInvName, discount, purchaseType, quantity);
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

        public bool modifyProductDiscountType(string loggedInUserName, string productInvName, Guid productID, Discount newDiscount)
        {
            if (this.canModifyProduct())
            {
                return _store.modifyProductDiscountType(loggedInUserName, productInvName, productID, newDiscount);
            }
            else return false;
        }

        public bool modifyProductPurchaseType(string loggedInUserName, string productInvName, Guid productID, PurchaseType purchaseType)
        {
            if (this.canModifyProduct())
            {
                return _store.modifyProductPurchaseType(loggedInUserName, productInvName, productID, purchaseType);
            }
            else return false;
        }

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

        public void logPurchase(StorePurchase purchase)
        {
            _store.logPurchase(purchase);
        }

        public Permissions getPermissionByName(string userName)
        {
            throw new NotImplementedException();
        }

        public List<StorePurchase> purchaseHistory()
        {
            if (this.canWatchHistory())
            {
                return _store.purchaseHistory();
            }
            else return null;
        }
    }
}