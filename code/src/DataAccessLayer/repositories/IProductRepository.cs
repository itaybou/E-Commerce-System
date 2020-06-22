using ECommerceSystem.DomainLayer.StoresManagement;
using System;

namespace ECommerceSystem.DataAccessLayer.repositories
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        void UncacheProduct(Product prod);
    }
}
