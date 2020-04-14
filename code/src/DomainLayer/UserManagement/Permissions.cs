using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.UserManagement
{   

    public enum permissionType
    {
        AddProduct,
        DeleteProductInv,
        ModifyProduct,
        WatchAndomment,
        PurchaseHistory,
        WatchHistory
    }

    public class Permissions
    {
        private User _assignedBy;
        private Dictionary<permissionType, bool> _permissions;
        private bool _isOwner;

        internal User AssignedBy { get => _assignedBy; set => _assignedBy = value; }

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
            _permissions[permissionType.AddProduct] = isOwner;
            _permissions[permissionType.DeleteProductInv] = isOwner;
            _permissions[permissionType.ModifyProduct] = isOwner;
            _permissions[permissionType.WatchHistory] = isOwner;
            _permissions[permissionType.WatchAndomment] = true; // Default to manager
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
            return _permissions[permissionType.AddProduct];
        }

        public bool canDeleteProduct()
        {
            return _permissions[permissionType.DeleteProductInv];
        }

        public bool canModifyProduct()
        {
            return _permissions[permissionType.ModifyProduct];
        }

        public bool canWatchHistory()
        {
            return _permissions[permissionType.WatchHistory];
        }

        public void edit(List <permissionType> permissions)
        {
            foreach (KeyValuePair<permissionType, bool> per in _permissions)
            {
                _permissions[per.Key] = permissions.Contains(per.Key);
            }
        }

    }
}
