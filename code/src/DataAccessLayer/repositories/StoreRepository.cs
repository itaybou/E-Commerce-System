using ECommerceSystem.DomainLayer.StoresManagement;

namespace ECommerceSystem.DataAccessLayer.repositories
{
    public class StoreRepository : Repository<Store, string>, IStoreRepository
    {
        public StoreRepository(IDbContext context, string repositoryName) : base(context, repositoryName) { }

        public void UncachStore(Store store)
        {
            return;
        }
    }
}