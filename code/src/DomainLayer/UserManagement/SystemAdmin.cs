using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public class SystemAdmin : Subscribed
    {
        public SystemAdmin(string uname, string pswd, string fname, string lname, string email) : base(uname, pswd, fname, lname, email) { }
    }
}
