using ECommerceSystem.DataAccessLayer.repositories;
using ECommerceSystem.DataAccessLayer.repositories.cache;
using ECommerceSystem.Exceptions;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;

namespace ECommerceSystem.DataAccessLayer
{
    public class DataAccess : IDataAccess
    {
        public string ConnectionString => "mongodb+srv://itaybou:linkin9p@ecommercesystem-lczqf.azure.mongodb.net/test?retryWrites=true&w=majority"; //"localhost:27017";
        public string DatabaseName => "ECommerceSystem";
        public string TestDatabaseName => "ECommerceSystemTests";

        private static readonly Lazy<DataAccess> lazy = new Lazy<DataAccess>(() => new DataAccess());
        public static DataAccess Instance => lazy.Value;

        private IDbContext ContextBackup { get; set; }
        private IDbContext Context { get; set; }
        private IDbContext TestContext { get; }
        public ITransactions Transactions { get; }

        private DataAccess()
        {
            EntityMap.RegisterClassMaps();
            Context = new DbContext(ConnectionString, DatabaseName);
            TestContext = new DbContext(ConnectionString, TestDatabaseName);
            Transactions = new Transactions(Context.Client(), Users, Stores, Products);
        }

        public void InitializeDatabase()
        {
            try
            {
                if (!CollectionExists(nameof(Users)))
                    Context.Database().CreateCollection(nameof(Users));
                if (!CollectionExists(nameof(Stores)))
                    Context.Database().CreateCollection(nameof(Stores));
                if (!CollectionExists(nameof(Products)))
                    Context.Database().CreateCollection(nameof(Products));
            }
            catch (Exception)
            {
                throw new DatabaseException("Drop database failed");
            }
        }

        public void InitializeTestDatabase()
        {
            if (!CollectionExists(nameof(Users)))
                TestContext.Database().CreateCollection(nameof(Users));
            if (!CollectionExists(nameof(Stores)))
                TestContext.Database().CreateCollection(nameof(Stores));
            if (!CollectionExists(nameof(Products)))
                TestContext.Database().CreateCollection(nameof(Products));
        }

        public void DropDatabase()
        {
            try
            {
                Context.Client().DropDatabase(DatabaseName);
            } catch(Exception)
            {
                throw new DatabaseException("Drop database failed");
            }
        }

        public void DropTestDatabase()
        {
            TestContext.Client().DropDatabase(TestDatabaseName);
        }

        private bool CollectionExists(string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var options = new ListCollectionNamesOptions { Filter = filter };

            return Context.Database().ListCollectionNames(options).Any();
        }

        public void SetTestContext()
        {
            InitializeTestDatabase();
            ContextBackup = Context;
            Context = TestContext;
        }

        public void SetDbContext()
        {
            DropTestDatabase();
            Context = ContextBackup;
        }

        private IUserRepository users;

        public IUserRepository Users
        {
            get
            {
                if (users == null)
                    users = new UserCacheProxy(Context, nameof(Users));
                return users;
            }
        }

        private IStoreRepository stores;

        public IStoreRepository Stores
        {
            get
            {
                if (stores == null)
                    stores = new StoresCacheProxy(Context, nameof(Stores));
                return stores;
            }
        }

        private IProductRepository products;
        public IProductRepository Products
        {
            get
            {
                if (products == null)
                    products = new ProductCacheProxy(Context, nameof(Products));
                return products;
            }
        }
    }
}