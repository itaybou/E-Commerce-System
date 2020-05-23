using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DataAccessLayer
{
    public interface IDbContext
    {
        IMongoDatabase Database();
        IMongoDatabase TestDatabase();
    }
}
