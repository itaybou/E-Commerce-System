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
            initPermmisionsDict();
        }

        

    }
}
