using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public enum PermissionType
    {
        AddProductInv,
        DeleteProductInv,
        ModifyProduct,
        WatchAndComment,
        WatchPurchaseHistory,
    }

    public class Permissions
    {
        private User _assignedBy;
        private Dictionary<PermissionType, bool> _permissions;
        private bool _isOwner;

        public User AssignedBy { get => _assignedBy;}

        private Permissions(User assignedBy, bool isOwner)
        {
            this._assignedBy = assignedBy;
            initPermmisionsDict(isOwner);
            this._isOwner = isOwner;
        }

        public static Permissions CreateOwner(User assignedBy)
        {
            if (assignedBy == null || assignedBy.isSubscribed())
            {
                Permissions permissions = new Permissions(assignedBy, true);
                return permissions;
            }
            else
            {
                return null;
            }
        }

        public static Permissions CreateManager(User assignedBy)
        {
            if (assignedBy.isSubscribed())
            {
                Permissions permissions = new Permissions(assignedBy, false);
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
    }
}