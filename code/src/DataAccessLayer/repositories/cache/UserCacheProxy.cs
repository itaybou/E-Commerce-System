using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Exceptions;
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
            try
            {
                return UserRepository.FetchAll();
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : fetch all users");
            }

        }

        public IEnumerable<User> FindAllBy(Expression<Func<User, bool>> predicate)
        {
            try
            {
                return UserRepository.FindAllBy(predicate);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : find all users by predicate");
            }
        }

        public User FindOneBy(Expression<Func<User, bool>> predicate)
        {
            var user = UsersCache.Values.Select(u => u.Element).AsQueryable().Where(predicate).FirstOrDefault();
            if (user == null)
            {
                try
                {
                    user = UserRepository.FindOneBy(predicate);
                    Cache(user);
                }
                catch (Exception e)
                {
                    SystemLogger.logger.Error(e.ToString());
                    throw new DatabaseException("Faild : find user by predicate");
                }
            }
            return user;
        }

        public User GetByIdOrNull(Guid id, Expression<Func<User, Guid>> idFunc)
        {
            var user = UsersCache.ContainsKey(id) ? UsersCache[id].GetAccessElement() : null;
            if (user == null)
            {
                try
                {
                    user = UserRepository.GetByIdOrNull(id, idFunc);
                    Cache(user);
                }
                catch (Exception e)
                {
                    SystemLogger.logger.Error(e.ToString());
                    throw new DatabaseException("Faild : get user by id");
                }
            }
            return user;
        }

        public void Insert(User entity)
        {
            try
            {
                UserRepository.Insert(entity);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : insert user");
            }

        }

        public IQueryable<User> QueryAll()
        {
            try
            {
                return UserRepository.QueryAll();
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : query all users");
            }


        }

        public void Remove(User entity, Guid id, Expression<Func<User, Guid>> idFunc)
        {
            try
            {
                UserRepository.Remove(entity, id, idFunc);
                Uncache(id);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : remove user");
            }

        }

        public void Update(User entity, Guid id, Expression<Func<User, Guid>> idFunc)
        {
            try
            {
                UserRepository.Update(entity, id, idFunc);
                if (UsersCache.ContainsKey(id))
                    Recache(entity);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : update user");
            }
        }

        public void Upsert(User entity, Guid id, Expression<Func<User, Guid>> idFunc)
        {
            try
            {
                UserRepository.Upsert(entity, id, idFunc);
                if (UsersCache.ContainsKey(id))
                    Recache(entity);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : upsert user");
            }
        }

        public User GetSubscribedUser(string username, string password)
        {
            try
            {
                var user = UserRepository.GetSubscribedUser(username, password);
                Cache(user);
                return user;
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : get subscribed user");
            }
        }

        public UserShoppingCart GetUserCart(Guid userID)
        {
            try
            {
                if (UsersCache.ContainsKey(userID))
                    return UsersCache[userID].Element.Cart;
                return GetByIdOrNull(userID, u => u.Guid).Cart;
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : get user by id to get cart");
            }

        }

        public IEnumerable<User> GetSubscribedByUsernameStart(string username)
        {
            try
            {
                return UserRepository.GetSubscribedByUsernameStart(username);
            }

            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : get subscribed by username");
            }
        }

        public void setContext(IDbContext context)
        {
            UserRepository.setContext(context);
        }

        public void RemoveCacheData()
        {
            UsersCache.Clear();
        }
    }
}