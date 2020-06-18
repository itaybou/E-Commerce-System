using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ECommerceSystem.DataAccessLayer.repositories
{
    // T is repository entity type, K is the key type for that entity
    public class Repository<T, K> : IRepository<T, K> where T : class
    {
        private IDbContext Context { get; }
        private string RepositoryName { get; }

        public Repository(IDbContext context, string repositoryName)
        {
            Context = context;
            RepositoryName = repositoryName;
        }

        public ICollection<T> FetchAll()
        {
            var collection = Context.Database().GetCollection<T>(RepositoryName);
            return collection.Find(new BsonDocument()).ToList();
        }

        public T GetByIdOrNull(K id, Expression<Func<T, K>> idFunc)
        {
            var collection = Context.Database().GetCollection<T>(RepositoryName);
            var filter = Builders<T>.Filter.Eq(idFunc, id);
            return collection.Find(filter).FirstOrDefault();
        }

        public void Insert(T entity)
        {
            var collection = Context.Database().GetCollection<T>(RepositoryName);
            collection.InsertOne(entity);
        }

        public IQueryable<T> QueryAll()
        {
            var collection = Context.Database().GetCollection<T>(RepositoryName);
            return FetchAll().AsQueryable();
        }

        public void Remove(T entity, K id, Expression<Func<T, K>> idFunc)
        {
            var collection = Context.Database().GetCollection<T>(RepositoryName);
            var filter = Builders<T>.Filter.Eq(idFunc, id);
            collection.DeleteOne(filter);
        }

        [Obsolete]
        public void Update(T entity, K id, Expression<Func<T, K>> idFunc)
        {
            var collection = Context.Database().GetCollection<T>(RepositoryName);
            var filter = Builders<T>.Filter.Eq(idFunc, id);
            collection.ReplaceOne(filter, entity, new UpdateOptions { IsUpsert = false });
        }

        [Obsolete]
        public void Upsert(T entity, K id, Expression<Func<T, K>> idFunc)
        {
            var collection = Context.Database().GetCollection<T>(RepositoryName);
            var filter = Builders<T>.Filter.Eq(idFunc, id);
            collection.ReplaceOne(filter, entity, new UpdateOptions { IsUpsert = true });
        }

        public T FindOneBy(Expression<Func<T, bool>> predicate)
        {
            var collection = Context.Database().GetCollection<T>(RepositoryName);
            var filter = Builders<T>.Filter.Where(predicate);
            return collection.Find(filter).FirstOrDefault();
        }

        public IEnumerable<T> FindAllBy(Expression<Func<T, bool>> predicate)
        {
            var collection = Context.Database().GetCollection<T>(RepositoryName);
            var filter = Builders<T>.Filter.Where(predicate);
            return collection.Find(filter).ToList();
        }
    }
}