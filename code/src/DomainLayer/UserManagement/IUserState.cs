namespace ECommerceSystem.DomainLayer.UserManagement
{
    public interface IUserState
    {
        string Username { get; set; }

        string Password { get; set; }

        bool isSubscribed();

        void logPurchase(UserPurchase userPurchase);

        bool isSystemAdmin();

        void addPermission(Permissions permissions, string storeName);

        void removePermissions(string storeName);

        Permissions getPermission(string storeName);
    }
}