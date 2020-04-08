using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.UserManagement;

namespace ECommerceSystem.ServiceLayer.PersistenceDuumy
{
    class DBDummy : IDataManager
    {
        private List<User> _users; // Will be in db


        public List<User> getUsers()
        {
            return _users;
        }
    }
}
