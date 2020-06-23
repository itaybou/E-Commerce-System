namespace ECommerceSystem.DataAccessLayer.repositories.cache
{
    internal interface ICacheProxy<T, K>
    {
        int CleanCacheMinutesTime { get; }
        int StoreCachedObjectsSecondsTime { get; }

        void Uncache(K id);

        void Cache(T entity);

        void Recache(T entity);

        void CleanCache();

        void RemoveCacheData();
    }
}