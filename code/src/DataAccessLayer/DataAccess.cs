using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DataAccessLayer.repositories;
using ECommerceSystem.DataAccessLayer.repositories.cache;
using MongoDB.Bson;
using MongoDB.Driver;

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

        private DataAccess()
        {
            EntityMap.RegisterClassMaps();
            Context = new DbContext(ConnectionString, DatabaseName, TestDatabaseName);
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            if (!CollectionExists(nameof(Users)))
                Context.Database().CreateCollection(nameof(Users));
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
    }
}
