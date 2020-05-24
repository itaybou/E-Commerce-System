using MongoDB.Driver;

namespace ECommerceSystem.DataAccessLayer
{
    public interface IDbContext
    {
        MongoClient Client();

        IMongoDatabase Database();

        IMongoDatabase TestDatabase();
    }
}