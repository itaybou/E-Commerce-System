using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DataAccessLayer
{
    public class DbContext : IDbContext
    {
        private MongoClient MongoClient { get; }
        private DatabaseConfiguration Configuration { get; }

        public DbContext(string connectionString, string databaseName, string testDatabaseName)
        {
            Configuration = new DatabaseConfiguration(connectionString, databaseName, testDatabaseName);
            MongoClient = new MongoClient();//Configuration.ConnectionString);
        }

        public IMongoDatabase Database() => MongoClient.GetDatabase(Configuration.DatabaseName);
        public IMongoDatabase TestDatabase() => MongoClient.GetDatabase(Configuration.TestDatabaseName);

        private class DatabaseConfiguration
        {
            private readonly string _connectionString;
            private readonly string _databaseName;
            private readonly string _testDatabaseName;

            public DatabaseConfiguration(string connectionString, string databaseName, string testDatabaseName)
            {
                _connectionString = connectionString;
                _databaseName = databaseName;
                _testDatabaseName = testDatabaseName;
            }

            public string ConnectionString => _connectionString;

            public string DatabaseName => _databaseName;

            public string TestDatabaseName => _testDatabaseName;
        }

    }
}
