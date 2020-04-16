using System;
using System.Collections.Generic;
using System.Text;
using ECommerceSystem.ServiceLayer;

namespace ECommerceSystemAcceptanceTests.adapters
{
    class RealBridge : IBridgeAdapter
    {
        private UserServices _userServices;

        public RealBridge()
        {
            _userServices = new UserServices();
        }

        public bool register(string uname, string pswd, string fname, string lname, string email)
        {
            return _userServices.register(uname, pswd, fname, lname, email);
        }
    }
}