using ECommerceSystem.DomainLayer.StoresManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DataAccessLayer.repositories
{
    public class ProductRepository : Repository<Product, Guid>, IProductRepository
    {
        public ProductRepository(IDbContext context, string repositoryName) : base(context, repositoryName) { }

        public IEnumerable<Product> GetProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UncacheProduct(Product prod)
        {
            return;
        }
    }
}
