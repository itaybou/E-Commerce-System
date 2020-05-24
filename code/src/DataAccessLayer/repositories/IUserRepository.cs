using ECommerceSystem.DomainLayer.UserManagement;
using System;

namespace ECommerceSystem.DataAccessLayer.repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        User GetSubscribedUser(string username, string password);

        void CacheUser(User user);

        void UncacheUser(User user);
    }
}