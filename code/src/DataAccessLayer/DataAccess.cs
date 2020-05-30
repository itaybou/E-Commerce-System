using ECommerceSystem.DataAccessLayer.repositories;
using ECommerceSystem.DataAccessLayer.repositories.cache;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;

namespace ECommerceSystem.DataAccessLayer
{
    public class DataAccess : IDataAccess
    {
        public string ConnectionString => "localhost:27017";//"mongodb+srv://itaybou:linkin9p@ecommercesystem-lczqf.azure.mongodb.net/test?retryWrites=true&w=majority";
        public string DatabaseName => "ECommerceSystem";
        public string TestDatabaseName => "ECommerceSystemTests";

        private static readonly Lazy<DataAccess> lazy = new Lazy<DataAccess>(() => new DataAccess());
        public static DataAccess Instance => lazy.Value;

        private IDbContext Context { get; }
        private IDbContext TestContext { get; }
        public ITransactions Transactions { get; }

        private DataAccess()
        {
            EntityMap.RegisterClassMaps();
            Context = new DbContext(ConnectionString, DatabaseName);
            TestContext = new DbContext(ConnectionString, TestDatabaseName);
            Transactions = new Transactions(Context.Client(), Users, Stores, Products);
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            if (!CollectionExists(nameof(Users)))
                Context.Database().CreateCollection(nameof(Users));
            if (!CollectionExists(nameof(Stores)))
                Context.Database().CreateCollection(nameof(Stores));
            if (!CollectionExists(nameof(Products)))
                Context.Database().CreateCollection(nameof(Products));
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

        private IUserRepository test_users;

        public IUserRepository TestUsers
        {
            get
            {
                if (test_users == null)
                    test_users = new UserCacheProxy(TestContext, nameof(Users));
                return test_users;
            }
        }

        private IStoreRepository test_stores;

        public IStoreRepository TestStores
        {
            get
            {
                if (test_stores == null)
                    test_stores = new StoresCacheProxy(TestContext, nameof(Stores));
                return test_stores;
            }
        }

        private IProductRepository test_products;
        public IProductRepository TestProducts
        {
            get
            {
                if (test_products == null)
                    test_products = new ProductCacheProxy(TestContext, nameof(Products));
                return test_products;
            }
        }
    }
}