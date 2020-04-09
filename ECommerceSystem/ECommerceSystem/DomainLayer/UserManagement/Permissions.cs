using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.UserManagement
{   
    class Permissions
    {
        private User _assignedBy;
        private Dictionary<string, bool> _permissions;
        private bool _isOwner;

        private Permissions(User assignedBy, bool isOwner)
        {
            this._assignedBy = assignedBy;
            initPermmisionsDict(isOwner);
            this._isOwner = isOwner;
        }

        public static Permissions CreateOwner(User assignedBy)
        {
            if(assignedBy.isSubscribed())
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
            this._permissions = new Dictionary<string, bool>();
            _permissions["addProduct"] = isOwner;
            _permissions["deleteProductInv"] = isOwner;
            _permissions["modifyProduct"] = isOwner;
            _permissions["WatchAndomment"] = true;
            _permissions["PurchaseHistory"] = true;
        }

        public void makeOwner()
        {
            foreach (KeyValuePair<string, bool> per in _permissions)
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
            return _permissions["addProduct"];
        }

        public bool canDeleteProduct()
        {
            return _permissions["deleteProductInv"];
        }

        public bool canModifyProduct()
        {
            return _permissions["modifyProduct"];
        }

    }
}
