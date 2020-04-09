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
            this._isOwner = isOwner;
            initPermmisionsDict(isOwner);
        }

        public static Permissions Create(User assignedBy, bool isOwner)
        {
            if(assignedBy.isSubscribed())
            {
                return new Permissions(assignedBy, isOwner);
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
