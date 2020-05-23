using ECommerceSystem.DataAccessLayer.repositories.cache;
using ECommerceSystem.DomainLayer.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DataAccessLayer.repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        User GetSubscribedUser(string username, string password);
        void CacheUser(User user);
        void UncacheUser(User user);
    }
}
