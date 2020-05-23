using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ECommerceSystem.DataAccessLayer.repositories.cache
{
    class CachedObject<T>
    {
        T _element;
        DateTime _cacheTime;

        public CachedObject(T element)
        {
            _element = element;
            _cacheTime = DateTime.Now;

        }

        public void SetAccessed()
        {
            CacheTime = DateTime.Now;
        }

        public double CachedTime()
        {
            var now = DateTime.Now;
            return now.Subtract(_cacheTime).TotalSeconds;
        }

        public T Element 
        { 
            get => _element;
            set => _element = value;
        }
        public DateTime CacheTime { get => _cacheTime; set => _cacheTime = value; }
    }
}
