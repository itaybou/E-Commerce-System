using ECommerceSystem.DomainLayer.StoresManagement;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ECommerceSystem.DataAccessLayer.repositories.cache
{
    internal class ProductCacheProxy : IProductRepository, ICacheProxy<Product, Guid>
    {
        private CacheCleaner CacheCleaner;
        public int CleanCacheMinutesTime => 15; // Time interval to clean cache in minutes
        public int StoreCachedObjectsSecondsTime => 60 * 10;

        private IDictionary<Guid, CachedObject<Product>> ProductCache { get; }
        private IProductRepository ProductRepository { get; }

        public ProductCacheProxy(IDbContext context, string repositoryName)
        {
            CacheCleaner = new CacheCleaner(CleanCache, CleanCacheMinutesTime * 60);
            ProductCache = new ConcurrentDictionary<Guid, CachedObject<Product>>();
            ProductRepository = new ProductRepository(context, repositoryName);
            CacheCleaner.StartCleaner();
        }

        public void Cache(Product entity)
        {
            if (entity != null && !ProductCache.ContainsKey(entity.Id))
            {
                var cachedStore = new CachedObject<Product>(entity);
                ProductCache.Add(entity.Id, cachedStore);
            }
        }

        public void Uncache(Guid id)
        {
            if (ProductCache.ContainsKey(id))
                ProductCache.Remove(id);
        }

        public void Recache(Product entity)
        {
            Uncache(entity.Id);
            Cache(entity);
        }

        public void CleanCache()
        {
            foreach (var store in ProductCache)
            {
                if (store.Value.CachedTime() > StoreCachedObjectsSecondsTime)
                    Uncache(store.Key);
            }
        }

        public ICollection<Product> FetchAll()
        {
            return ProductRepository.FetchAll();
        }

        public IEnumerable<Product> FindAllBy(Expression<Func<Product, bool>> predicate)
        {
            return ProductRepository.FindAllBy(predicate);
        }

        public Product FindOneBy(Expression<Func<Product, bool>> predicate)
        {
            var store = ProductCache.Values.Select(u => u.Element).AsQueryable().Where(predicate).FirstOrDefault();
            if (store == null)
            {
                store = ProductRepository.FindOneBy(predicate);
                Cache(store);
            }
            return store;
        }

        public Product GetByIdOrNull(Guid id, Expression<Func<Product, Guid>> idFunc)
        {
            var user = ProductCache.ContainsKey(id) ? ProductCache[id].GetAccessElement() : null;
            if (user == null)
            {
                user = ProductRepository.GetByIdOrNull(id, idFunc);
                Cache(user);
            }
            return user;
        }

        public void Insert(Product entity)
        {
            ProductRepository.Insert(entity);
        }

        public IQueryable<Product> QueryAll()
        {
            return ProductRepository.QueryAll();
        }

        public void Remove(Product entity, Guid id, Expression<Func<Product, Guid>> idFunc)
        {
            Uncache(id);
            ProductRepository.Remove(entity, id, idFunc);
        }

        public void Update(Product entity, Guid id, Expression<Func<Product, Guid>> idFunc)
        {
            ProductRepository.Update(entity, id, idFunc);
            if (ProductCache.ContainsKey(id))
                Recache(entity);
        }

        public void Upsert(Product entity, Guid id, Expression<Func<Product, Guid>> idFunc)
        {
            ProductRepository.Upsert(entity, id, idFunc);
            if (ProductCache.ContainsKey(id))
                Recache(entity);
        }
    }
}
