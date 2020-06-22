using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.Exceptions;
using NLog;
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
            foreach (var product in ProductCache)
            {
                if (product.Value.CachedTime() > StoreCachedObjectsSecondsTime)
                    Uncache(product.Key);
            }
        }

        public ICollection<Product> FetchAll()
        {
            try
            {
                return ProductRepository.FetchAll();
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : fetch all product repository");
            }
            
        }

        public IEnumerable<Product> FindAllBy(Expression<Func<Product, bool>> predicate)
        {
            return ProductRepository.FindAllBy(predicate);
        }

        public Product FindOneBy(Expression<Func<Product, bool>> predicate)
        {
            var product = ProductCache.Values.Select(u => u.Element).AsQueryable().Where(predicate).FirstOrDefault();
            if (product == null)
            {
                try
                {
                    product = ProductRepository.FindOneBy(predicate);
                }
                catch (Exception e)
                {
                    SystemLogger.logger.Error(e.ToString());
                    throw new DatabaseException("Faild : find product");
                }
                Cache(product);
            }
            return product;
        }

        public Product GetByIdOrNull(Guid id, Expression<Func<Product, Guid>> idFunc)
        {
            var product = ProductCache.ContainsKey(id) ? ProductCache[id].GetAccessElement() : null;
            if (product == null)
            {
                try
                {
                    product = ProductRepository.GetByIdOrNull(id, idFunc);
                }
                catch (Exception e)
                {
                    SystemLogger.logger.Error(e.ToString());
                    throw new DatabaseException("Faild : get product by id");
                }
                
                Cache(product);
            }
            return product;
        }

        public void Insert(Product entity)
        {
            try
            {
                ProductRepository.Insert(entity);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : insert product");
            }
            
        }

        public IQueryable<Product> QueryAll()
        {
            try
            {
                return ProductRepository.QueryAll();
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : query all products");
            }
            
        }

        public void Remove(Product entity, Guid id, Expression<Func<Product, Guid>> idFunc)
        {
            Uncache(id);
            try
            {
                ProductRepository.Remove(entity, id, idFunc);
            }
            catch(Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : remove product from DB");
            }
            
        }

        public void Update(Product entity, Guid id, Expression<Func<Product, Guid>> idFunc)
        {
            try
            {
                ProductRepository.Update(entity, id, idFunc);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : update product in DB");
            }
            
            if (ProductCache.ContainsKey(id))
                Recache(entity);
        }

        public void Upsert(Product entity, Guid id, Expression<Func<Product, Guid>> idFunc)
        {
            try
            {
                ProductRepository.Upsert(entity, id, idFunc);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : upsert product in DB");
            }
            if (ProductCache.ContainsKey(id))
                Recache(entity);
        }

        public void setContext(IDbContext context)
        {
            ProductRepository.setContext(context);
        }

        public void RemoveCacheDate()
        {
            ProductCache.Clear();
        }
    }
}
