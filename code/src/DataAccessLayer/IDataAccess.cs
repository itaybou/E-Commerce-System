using ECommerceSystem.DataAccessLayer.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DataAccessLayer
{
    public interface IDataAccess
    {
        string ConnectionString { get; }
        string DatabaseName { get; }
        string TestDatabaseName { get; }


        IUserRepository Users { get; }
    }
}
