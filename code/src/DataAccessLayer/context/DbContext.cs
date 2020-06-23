using MongoDB.Driver;
using ECommerceSystem.Exceptions;
using System;
using ECommerceSystem.DomainLayer.SystemManagement;

namespace ECommerceSystem.DataAccessLayer
{
    public class DbContext : IDbContext
    {
        private MongoClient MongoClient { get; }
        private DatabaseConfiguration Configuration { get; }

        public DbContext(string connectionString, string databaseName)
        {
            try
            {
                Configuration = new DatabaseConfiguration(connectionString, databaseName);
                MongoClient = new MongoClient(Configuration.ConnectionString);
            } catch(Exception e)
            {
                SystemLogger.LogError("Failed to create mongo client, failed to establish db connection to " + Configuration.ConnectionString + "," + Configuration.DatabaseName + ": " + e);
                throw new DatabaseException("Failed to connect to databade.");
            }
        }

        public MongoClient Client() => MongoClient;

        public IMongoDatabase Database() => MongoClient.GetDatabase(Configuration.DatabaseName);


        private class DatabaseConfiguration
        {
            private readonly string _connectionString;
            private readonly string _databaseName;

            public DatabaseConfiguration(string connectionString, string databaseName)
            {
                _connectionString = connectionString;
                _databaseName = databaseName;
            }

            public string ConnectionString => _connectionString;

            public string DatabaseName => _databaseName;
        }
    }
}