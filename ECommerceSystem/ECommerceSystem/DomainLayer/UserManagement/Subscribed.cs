using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    class Subscribed : IUserState
    {
        private List<Store> _storesOwned;
        private List<Store> _storesManaged;
    }
}
