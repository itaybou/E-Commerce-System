using ECommerceSystem.DomainLayer.UserManagement;
using System;
using System.Collections.Generic;

namespace ECommerceSystem.DataAccessLayer.repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        User GetSubscribedUser(string username, string password);
        IEnumerable<User> GetSubscribedByUsernameStart(string username);

        void CacheUser(User user);

        void UncacheUser(User user);
    }
}