using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.UserManagement
{   
    class Permissions
    {
        private Subscribed _assignedBy;
        private Dictionary<string, bool> _permissions;

        public Permissions(Subscribed assignedBy, bool isOwner)
        {
            this._assignedBy = assignedBy;
            initPermmisionsDict(isOwner);
        }

        private void initPermmisionsDict(bool isOwner)
        {
            this._permissions = new Dictionary<string, bool>();
            _permissions["addProduct"] = isOwner;
            _permissions["deleteProductInv"] = isOwner;
            _permissions["modifyProduct"] = isOwner;
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
