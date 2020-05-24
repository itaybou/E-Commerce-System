using ECommerceSystem.DomainLayer.StoresManagement;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ECommerceSystem.DataAccessLayer.repositories.cache
{
    internal class StoresCacheProxy : IStoreRepository, ICacheProxy<Store, string>
    {
        private CacheCleaner CacheCleaner;
        public int CleanCacheMinutesTime => 10; // Time interval to clean cache in minutes
        public int StoreCachedObjectsSecondsTime => 60 * 10;

        private IDictionary<string, CachedObject<Store>> StoresCache { get; }
        private IStoreRepository StoreRepository { get; }

        public StoresCacheProxy(IDbContext context, string repositoryName)
        {
            CacheCleaner = new CacheCleaner(CleanCache, CleanCacheMinutesTime * 60);
            StoresCache = new ConcurrentDictionary<string, CachedObject<Store>>();
            StoreRepository = new StoreRepository(context, repositoryName);
            CacheCleaner.StartCleaner();
        }

        public void Cache(Store entity)
        {
            if (entity != null && !StoresCache.ContainsKey(entity.Name))
            {
                var cachedStore = new CachedObject<Store>(entity);
                StoresCache.Add(entity.Name, cachedStore);
            }
        }

        public void Uncache(string id)
        {
            if (StoresCache.ContainsKey(id))
                StoresCache.Remove(id);
        }

        public void Recache(Store entity)
        {
            Uncache(entity.Name);
            Cache(entity);
        }

        public void CleanCache()
        {
            foreach (var store in StoresCache)
            {
                if (store.Value.CachedTime() > StoreCachedObjectsSecondsTime)
                    Uncache(store.Key);
            }
        }

        public ICollection<Store> FetchAll()
        {
            return StoreRepository.FetchAll();
        }

        public IEnumerable<Store> FindAllBy(Expression<Func<Store, bool>> predicate)
        {
            return StoreRepository.FindAllBy(predicate);
        }

        public Store FindOneBy(Expression<Func<Store, bool>> predicate)
        {
            var store = StoresCache.Values.Select(u => u.Element).AsQueryable().Where(predicate).FirstOrDefault();
            if (store == null)
            {
                store = StoreRepository.FindOneBy(predicate);
                Cache(store);
            }
            return store;
        }

        public Store GetByIdOrNull(string id, Expression<Func<Store, string>> idFunc)
        {
            var user = StoresCache.ContainsKey(id) ? StoresCache[id].GetAccessElement() : null;
            if (user == null)
            {
                user = StoreRepository.GetByIdOrNull(id, idFunc);
                Cache(user);
            }
            return user;
        }

        public void Insert(Store entity)
        {
            StoreRepository.Insert(entity);
        }

        public IQueryable<Store> QueryAll()
        {
            return StoreRepository.QueryAll();
        }

        public void Remove(Store entity, string id, Expression<Func<Store, string>> idFunc)
        {
            Uncache(id);
            StoreRepository.Remove(entity, id, idFunc);
        }

        public void Update(Store entity, string id, Expression<Func<Store, string>> idFunc)
        {
            StoreRepository.Update(entity, id, idFunc);
            if (StoresCache.ContainsKey(id))
                Recache(entity);
        }

        public void Upsert(Store entity, string id, Expression<Func<Store, string>> idFunc)
        {
            StoreRepository.Upsert(entity, id, idFunc);
            if (StoresCache.ContainsKey(id))
                Recache(entity);
        }
    }
}