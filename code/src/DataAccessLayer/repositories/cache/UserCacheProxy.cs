using ECommerceSystem.DomainLayer.UserManagement;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ECommerceSystem.DataAccessLayer.repositories.cache
{
    internal class UserCacheProxy : IUserRepository, ICacheProxy<User, Guid>
    {
        private CacheCleaner CacheCleaner;
        public int CleanCacheMinutesTime => 5; // Time interval to clean cache in minutes
        public int StoreCachedObjectsSecondsTime => 60 * 10;

        private IDictionary<Guid, CachedObject<User>> UsersCache { get; }
        private IUserRepository UserRepository { get; }

        public UserCacheProxy(IDbContext context, string repositoryName)
        {
            CacheCleaner = new CacheCleaner(CleanCache, CleanCacheMinutesTime * 60);
            UsersCache = new ConcurrentDictionary<Guid, CachedObject<User>>();
            UserRepository = new UserRepository(context, repositoryName);
            CacheCleaner.StartCleaner();
        }

        public void Cache(User user)
        {
            if (user != null && !UsersCache.ContainsKey(user.Guid))
            {
                var cachedUser = new CachedObject<User>(user);
                UsersCache.Add(user.Guid, cachedUser);
            }
        }

        public void Uncache(Guid id)
        {
            if (UsersCache.ContainsKey(id))
                UsersCache.Remove(id);
        }

        public void CacheUser(User user)
        {
            Cache(user);
        }

        public void UncacheUser(User user)
        {
            Uncache(user.Guid);
        }

        public void CleanCache()
        {
            foreach (var user in UsersCache)
            {
                if (user.Value.Element.isSubscribed() && user.Value.CachedTime() > StoreCachedObjectsSecondsTime)
                    Uncache(user.Key);
            }
        }

        public void Recache(User entity)
        {
            Uncache(entity.Guid);
            Cache(entity);
        }

        public ICollection<User> FetchAll()
        {
            return UserRepository.FetchAll();
        }

        public IEnumerable<User> FindAllBy(Expression<Func<User, bool>> predicate)
        {
            return UserRepository.FindAllBy(predicate);
        }

        public User FindOneBy(Expression<Func<User, bool>> predicate)
        {
            var user = UsersCache.Values.Select(u => u.Element).AsQueryable().Where(predicate).FirstOrDefault();
            if (user == null)
            {
                user = UserRepository.FindOneBy(predicate);
                Cache(user);
            }
            return user;
        }

        public User GetByIdOrNull(Guid id, Expression<Func<User, Guid>> idFunc)
        {
            var user = UsersCache.ContainsKey(id) ? UsersCache[id].GetAccessElement() : null;
            if (user == null)
            {
                user = UserRepository.GetByIdOrNull(id, idFunc);
                Cache(user);
            }
            return user;
        }

        public void Insert(User entity)
        {
            UserRepository.Insert(entity);
        }

        public IQueryable<User> QueryAll()
        {
            return UserRepository.QueryAll();
        }

        public void Remove(User entity, Guid id, Expression<Func<User, Guid>> idFunc)
        {
            Uncache(id);
            UserRepository.Remove(entity, id, idFunc);
        }

        public void Update(User entity, Guid id, Expression<Func<User, Guid>> idFunc)
        {
            UserRepository.Update(entity, id, idFunc);
            if (UsersCache.ContainsKey(id))
                Recache(entity);
        }

        public void Upsert(User entity, Guid id, Expression<Func<User, Guid>> idFunc)
        {
            UserRepository.Upsert(entity, id, idFunc);
            if (UsersCache.ContainsKey(id))
                Recache(entity);
        }

        public User GetSubscribedUser(string username, string password)
        {
            var user = UserRepository.GetSubscribedUser(username, password);
            Cache(user);
            return user;
        }

        public UserShoppingCart GetUserCart(Guid userID)
        {
            if (UsersCache.ContainsKey(userID))
                return UsersCache[userID].Element.Cart;

            return GetByIdOrNull(userID, u => u.Guid).Cart;
        }

        public IEnumerable<User> GetSubscribedByUsernameStart(string username)
        {
            return UserRepository.GetSubscribedByUsernameStart(username);
        }
    }
}