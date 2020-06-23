using ECommerceSystem.DataAccessLayer.repositories;
using ECommerceSystem.DataAccessLayer.repositories.cache;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Exceptions;
using ECommerceSystem.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;

namespace ECommerceSystem.DataAccessLayer
{
    public class DataAccess : IDataAccess
    {
        public string ConnectionString => "mongodb://localhost:27017";//"mongodb+srv://itaybou:linkin9p@ecommercesystem-lczqf.azure.mongodb.net/test?retryWrites=true&w=majority";
        public string LocalConnectionString => "mongodb://localhost:27017";
        public string DatabaseName => "ECommerceSystem";
        public string LocalDatabaseName => "LocalECommerceSystem";
        public string TestDatabaseName => "ECommerceSystemTests";

        private static readonly Lazy<DataAccess> lazy = new Lazy<DataAccess>(() => new DataAccess());
        public static DataAccess Instance => lazy.Value;

        private IDbContext ContextBackup { get; set; }

        private IDbContext LocalContext { get; set; }
        private IDbContext Context { get; set; }
        private IDbContext TestContext { get; set; }
        public ITransactions Transactions { get; set; }

        private DataAccess()
        {
            EntityMap.RegisterClassMaps();
        }

        public void InitializeDBContext()
        {
            try
            {
                Context = new DbContext(ConnectionString, DatabaseName);
                LocalContext = new DbContext(LocalConnectionString, LocalDatabaseName);
                TestContext = new DbContext(LocalConnectionString, TestDatabaseName);
                Transactions = new Transactions(Context.Client(), Users, Stores, Products);
            }
            catch (DatabaseException)
            {
                throw new DatabaseException("Failed to establish db connection.");
            }
        }

        public void InitializeDatabase()
        {
            try
            {
                if (!CollectionExists(nameof(Users), Context))
                    Context.Database().CreateCollection(nameof(Users));
                if (!CollectionExists(nameof(Stores), Context))
                    Context.Database().CreateCollection(nameof(Stores));
                if (!CollectionExists(nameof(Products), Context))
                    Context.Database().CreateCollection(nameof(Products));
                if (!CollectionExists(nameof(UserStatistics), LocalContext))
                    LocalContext.Database().CreateCollection(nameof(UserStatistics));
            }
            catch (Exception)
            {
                throw new DatabaseException("Drop database failed");
            }
        }

        public void InitializeTestDatabase()
        {
            if (!CollectionExists(nameof(Users), TestContext))
                TestContext.Database().CreateCollection(nameof(Users));
            if (!CollectionExists(nameof(Stores), TestContext))
                TestContext.Database().CreateCollection(nameof(Stores));
            if (!CollectionExists(nameof(Products), TestContext))
                TestContext.Database().CreateCollection(nameof(Products));
            if (!CollectionExists(nameof(UserStatistics), TestContext))
                TestContext.Database().CreateCollection(nameof(UserStatistics));
        }

        public void DropDatabase()
        {
            try
            {
                Context.Client().DropDatabase(DatabaseName);
                LocalContext.Client().DropDatabase(LocalDatabaseName);
            } catch(Exception)
            {
                throw new DatabaseException("Drop database failed");
            }
        }

        public void DropTestDatabase()
        {
            try
            {
                TestContext.Client().DropDatabase(TestDatabaseName);
                ((ICacheProxy<User, Guid>)Users).RemoveCacheData();
                ((ICacheProxy<Product, Guid>)Products).RemoveCacheData();
                ((ICacheProxy<Store, string>)Stores).RemoveCacheData();
            }
            catch (Exception)
            {
                throw new DatabaseException("Drop database failed");
            }


        }

        private bool CollectionExists(string collectionName, IDbContext context)
        {
            var filter = new BsonDocument("name", collectionName);
            var options = new ListCollectionNamesOptions { Filter = filter };

            return context.Database().ListCollectionNames(options).Any();
        }

        public void SetTestContext()
        {
            InitializeDBContext();
            InitializeTestDatabase();
            ContextBackup = Context;
            Context = TestContext;
            Users.setContext(TestContext);
            Stores.setContext(TestContext);
            Products.setContext(TestContext);
        }

        public void SetDbContext()
        {
            DropTestDatabase();
            Context = ContextBackup;
            Users.setContext(Context);
            Stores.setContext(Context);
            Products.setContext(Context);
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

        private IUserStatisticsRepository stats;
        public IUserStatisticsRepository Stats
        {
            get
            {
                if (stats == null)
                    stats = new UserStatisticsCacheProxy(LocalContext, nameof(UserStatistics));
                return stats;
            }
        }
    }
}