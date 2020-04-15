using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.UserManagement
{   

    public enum permissionType
    {
        AddProductInv,
        DeleteProductInv,
        ModifyProduct,
        WatchAndomment,
        WatchPurchaseHistory
        WatchHistory
    }

    public class Permissions
    {
        private User _assignedBy;
        private Dictionary<permissionType, bool> _permissions;
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
            if(assignedBy == null || assignedBy.isSubscribed())
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
            _permissions = new Dictionary<permissionType, bool>();
            _permissions[permissionType.AddProductInv] = isOwner;
            _permissions[permissionType.DeleteProductInv] = isOwner;
            _permissions[permissionType.ModifyProduct] = isOwner;
            _permissions[permissionType.WatchHistory] = true; // Default to manager
            _permissions[permissionType.WatchPurchaseHistory] = true;
            _permissions[permissionType.PurchaseHistory] = true;
            _permissions[permissionType.PurchaseHistory] = true; // Default to manager
        }

        public void makeOwner()
        {
            foreach (KeyValuePair<permissionType, bool> per in _permissions)
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
            return _permissions[permissionType.AddProductInv];
        }

        public bool canDeleteProduct()
        {
            return _permissions[permissionType.DeleteProductInv];
        }

        public bool canModifyProduct()
        {
            return _permissions[permissionType.ModifyProduct];
        }

        public bool canWatchPurchaseHistory()
        {
            return _permissions[permissionType.WatchPurchaseHistory];
        }

        public bool canWatchAndomment()
        {
            return _permissions[permissionType.WatchAndomment];
        }

        public void edit(List <permissionType> permissions)
        {
            // reset all permissions to false
            for (int i = 0; i < _permissions.Count; i++)
            {
                permissionType per = _permissions.ElementAt(i).Key;
                _permissions[per] = permissions.Contains(per);
            }
        }

    }
}
