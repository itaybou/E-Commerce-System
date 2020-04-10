using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    class Guest : IUserState
    {
        public bool isSubscribed()
        {
            return false;
        }

        public string Name()
        {
            return null;
        }
    }
}
