using ECommerceSystem.DomainLayer.UserManagement;
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
            return QueryAll().Where(u => u.Name.Equals(username) && u.Password.Equals(password)).FirstOrDefault();
        }

        public IEnumerable<User> GetSubscribedByUsernameStart(string username)
        {
            return QueryAll().Where(user => user.isSubscribed() && user.Name.ToLower().StartsWith(username.ToLower())).ToList();
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