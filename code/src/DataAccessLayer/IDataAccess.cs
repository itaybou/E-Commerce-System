using ECommerceSystem.DataAccessLayer.repositories;

namespace ECommerceSystem.DataAccessLayer
{
    public interface IDataAccess
    {
        string ConnectionString { get; }
        string DatabaseName { get; }
        string TestDatabaseName { get; }

        ITransactions Transactions { get; }
        IUserRepository Users { get; }
        IStoreRepository Stores { get; }
        IProductRepository Products { get; }
    }
}