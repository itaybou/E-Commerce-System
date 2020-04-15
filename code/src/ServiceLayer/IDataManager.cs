using System.Collections.Generic;

namespace ECommerceSystem.ServiceLayer
{
    public interface IDataManager<T>
    {
        List<T> getAll();

        void insert(T t);

        bool remove(T t);
    }
}