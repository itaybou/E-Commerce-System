using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DataAccessLayer
{
    public interface IRepository<T, K> where T : class
    {
        void Insert(T entity);
        void Upsert(T entity, K id, Expression<Func<T, K>> idFunc);
        void Update(T entity, K id, Expression<Func<T, K>> idFunc);
        void Remove(T entity, K id, Expression<Func<T, K>> idFunc);
        ICollection<T> FetchAll();
        T GetByIdOrNull(K id, Expression<Func<T, K>> idFunc);
        T FindOneBy(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindAllBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> QueryAll();

    }
}
