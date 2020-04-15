using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceSystemAcceptanceTests.adapters
{
    class ProxyBridge : IBridgeAdapter
    {
        private IBridgeAdapter real;

        internal IBridgeAdapter RealBridge { get => real; set => real = value; }

        public bool register(string uname, string pswd, string fname, string lname, string email)
        {
            if (real != null)
            {
                return real.register(uname, pswd, fname, lname, email);
            }
            else return true;
        }
    }
}