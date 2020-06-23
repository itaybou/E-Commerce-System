using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.Exceptions;
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
            try
            {
                return StoreRepository.FetchAll();
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : fetch all stores");
            }

        }

        public IEnumerable<Store> FindAllBy(Expression<Func<Store, bool>> predicate)
        {
            try
            {
                return StoreRepository.FindAllBy(predicate);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : find all by stores");
            }

        }

        public Store FindOneBy(Expression<Func<Store, bool>> predicate)
        {
            var store = StoresCache.Values.Select(u => u.Element).AsQueryable().Where(predicate).FirstOrDefault();
            if (store == null)
            {
                try
                {
                    store = StoreRepository.FindOneBy(predicate);
                }
                catch (Exception e)
                {
                    SystemLogger.logger.Error(e.ToString());
                    throw new DatabaseException("Faild : faild find one store");
                }

                Cache(store);
            }
            return store;
        }

        public Store GetByIdOrNull(string id, Expression<Func<Store, string>> idFunc)
        {
            var store = StoresCache.ContainsKey(id) ? StoresCache[id].GetAccessElement() : null;
            if (store == null)
            {
                try
                {
                    store = StoreRepository.GetByIdOrNull(id, idFunc);
                }
                catch (Exception e)
                {
                    SystemLogger.logger.Error(e.ToString());
                    throw new DatabaseException("Faild : get store by id");
                }

                Cache(store);
            }
            return store;
        }

        public void Insert(Store entity)
        {
            try
            {
                StoreRepository.Insert(entity);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : insert store");
            }

        }

        public IQueryable<Store> QueryAll()
        {
            try
            {
                return StoreRepository.QueryAll();
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : query all stores");
            }


        }

        public void Remove(Store entity, string id, Expression<Func<Store, string>> idFunc)
        {
            Uncache(id);
            try
            {
                StoreRepository.Remove(entity, id, idFunc);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : remove store");
            }

        }

        public void Update(Store entity, string id, Expression<Func<Store, string>> idFunc)
        {
            try
            {
                StoreRepository.Update(entity, id, idFunc);
            }
            
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : update store");
            }
            if (StoresCache.ContainsKey(id))
                Recache(entity);
        }

        public void Upsert(Store entity, string id, Expression<Func<Store, string>> idFunc)
        {
            try
            {
                StoreRepository.Upsert(entity, id, idFunc);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : upsert store");
            }  
            if (StoresCache.ContainsKey(id))
                Recache(entity);
        }

        public void setContext(IDbContext context)
        {
            StoreRepository.setContext(context);
        }

        public void RemoveCacheData()
        {
            StoresCache.Clear();
        }

        public void UncachStore(Store store)
        {
            Uncache(store.Name);
        }
    }
}