using ECommerceSystem.DomainLayer.StoresManagement;
using System;
using System.Collections.Generic;

namespace ECommerceSystem.DataAccessLayer.repositories
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        void UncacheProduct(Product prod);

    }
}
