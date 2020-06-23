using ECommerceSystem.DomainLayer.StoresManagement;

namespace ECommerceSystem.DataAccessLayer.repositories
{
    public interface IStoreRepository : IRepository<Store, string>
    {
        void UncachStore(Store store);
    }
}