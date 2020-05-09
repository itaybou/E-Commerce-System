using ECommerceSystem.Models;

namespace ECommerceSystem.DomainLayer.UserManagement
{
    public interface IUserState
    {
        bool isSubscribed();

        string Name();

        string Password();

        void logPurchase(UserPurchase userPurchase);

        bool isSystemAdmin();

        void addPermission(Permissions permissions, string storeName);

        void removePermissions(string storeName);

        Permissions getPermission(string storeName);
    }
}