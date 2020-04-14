using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public class Guest : IUserState
    {
        public bool isSubscribed()
        {
            return false;
        }

        public string Name()
        {
            return null;
        }

        public void logPurchase(UserPurchase purchase)
        {
            throw new NotSupportedException();
        }

        public string Password()
        {
            return null;
        }
    }
}
