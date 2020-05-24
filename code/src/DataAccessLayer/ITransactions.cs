using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;

namespace ECommerceSystem.DataAccessLayer
{
    public interface ITransactions
    {
        void OpenStoreTransaction(User owner, Store opened);
    }
}