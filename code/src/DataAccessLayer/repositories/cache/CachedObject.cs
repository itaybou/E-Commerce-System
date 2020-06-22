using ECommerceSystem.Utilities.interfaces;
using Newtonsoft.Json;
using System;

namespace ECommerceSystem.DataAccessLayer.repositories.cache
{
    internal class CachedObject<T>
    {
        private T _element;
        private DateTime _cacheTime;

        public CachedObject(T element)
        {
            _element = element;
            _cacheTime = DateTime.Now;
        }

        public T GetAccessElement()
        {
            CacheTime = DateTime.Now;
            return _element;
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