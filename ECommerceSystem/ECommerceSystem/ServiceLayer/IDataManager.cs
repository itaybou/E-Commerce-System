using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.DomainLayer.UserManagement;

namespace ECommerceSystem.ServiceLayer
{
    interface IDataManager<T>
    {
        List<T> getAll();
        void insert(T t);
        void remove(T t);
    }
}
