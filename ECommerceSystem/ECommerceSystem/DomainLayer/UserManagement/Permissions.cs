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
        private bool _isOwner;
        private Dictionary<string, bool> _permissions;

        public Permissions(Subscribed assignedBy, bool isOwner)
        {
            this._assignedBy = assignedBy;
            this._isOwner = isOwner;
            initPermmisionsDict(); // init all permissions false
        }

        private void initPermmisionsDict()
        {
            this._permissions = new Dictionary<string, bool>();
            _permissions["addProduct"] = false;
            _permissions["deleteProduct"] = false;
            _permissions["modifyProduct"] = false;
        }

        public bool canAddProduct()
        {
            return _permissions["addProduct"] || _isOwner;
        }
    }
}
