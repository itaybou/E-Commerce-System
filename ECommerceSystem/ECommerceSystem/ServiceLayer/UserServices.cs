using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.UserManagement;

namespace ECommerceSystem.ServiceLayer
{
    class UserServices
    {
        private UserManagement _management;

        public UserServices()
        {
            _management = UserManagement.Instance;
        }

        /// <summary>
        /// Check whether the user is already in subscribed state and register user to system
        /// </summary>
        /// <param name="uname">username</param>
        /// <param name="pswd">password</param>
        /// <param name="fname">first name</param>
        /// <param name="lname">last name</param>
        /// <param name="email">email address</param>
        /// <returns></returns>
        public bool register(string uname, string pswd, string fname, string lname, string email)
        {
            return !_management.getLoggedInUser().isSubscribed() && _management.register(uname, pswd, fname, lname, email) == null;
        }

        public bool login(string uname, string pswd)
        {
            return _management.login(uname, pswd);
        }
    }
}
