using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystem.DataAccessLayer.repositories
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(IDbContext context, string repositoryName) : base(context, repositoryName) { }

        public User GetSubscribedUser(string username, string password)
        {
            try
            {
                return QueryAll().Where(u => u.Name.Equals(username) && u.Password.Equals(password)).FirstOrDefault();
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : get subscribed user");
            }
        }

        public IEnumerable<User> GetSubscribedByUsernameStart(string username)
        {
            try
            {
                return QueryAll().Where(user => user.isSubscribed() && user.Name.ToLower().StartsWith(username.ToLower())).ToList();
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : get subscribed user by username");
            }

        }

        public void CacheUser(User user)
        {
            return;
        }

        public void UncacheUser(User user)
        {
            return;
        }
    }
}