using System;

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

        public bool isSystemAdmin()
        {
            return false;
        }
    }
}