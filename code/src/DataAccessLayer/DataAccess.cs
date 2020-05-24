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
        public ITransactions Transactions { get; }

        private DataAccess()
        {
            EntityMap.RegisterClassMaps();
            Context = new DbContext(ConnectionString, DatabaseName, TestDatabaseName);
            Transactions = new TransactionManager(Context.Client(), Users, Stores);
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            if (!CollectionExists(nameof(Users)))
                Context.Database().CreateCollection(nameof(Users));
            if (!CollectionExists(nameof(Stores)))
                Context.Database().CreateCollection(nameof(Stores));
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
    }
}