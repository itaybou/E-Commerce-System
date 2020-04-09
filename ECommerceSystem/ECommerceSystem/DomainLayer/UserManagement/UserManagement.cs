using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.StoresManagement;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    class UserManagement
    {
        private List<User> _users;


        public void addOwnStore(Store store, User user)
        {
            user.addOwnStore(store);
        }

        public void addManagerStore(Store store, User assignedUser)
        {
            assignedUser.addManagerStore(store);
        }
    }
}
