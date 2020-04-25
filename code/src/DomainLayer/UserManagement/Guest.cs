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
            return "";
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

        public void addPermission(Permissions permissions, string storeName)
        {
            return; // guest dont have permissions
        }


        public void removePermissions(string storeName)
        {
            return;  // guest dont have permissions
        }

        public Permissions getPermission(string storeName)
        {
            return null;  // guest dont have permissions
        }
    }
}