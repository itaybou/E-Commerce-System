using ECommerceSystem.DomainLayer.StoresManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DataAccessLayer.repositories
{
    interface IStoreRepository : IRepository<Store, string>
    {
    }
}
