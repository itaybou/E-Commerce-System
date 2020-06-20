using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.Exceptions;
using ECommerceSystem.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ECommerceSystem.DataAccessLayer.repositories.cache
{
    internal class UserStatisticsCacheProxy : IUserStatisticsRepository, ICacheProxy<UserStatistics, DateTime>
    {
        private CacheCleaner CacheCleaner;
        public int CleanCacheMinutesTime => 15; // Time interval to clean cache in minutes
        public int StoreCachedObjectsSecondsTime => 60 * 10;

        private IDictionary<DateTime, CachedObject<UserStatistics>> UserStatsCache { get; }
        private IUserStatisticsRepository StatsRepository { get; }

        public UserStatisticsCacheProxy(IDbContext context, string repositoryName)
        {
            CacheCleaner = new CacheCleaner(CleanCache, CleanCacheMinutesTime * 60);
            UserStatsCache = new ConcurrentDictionary<DateTime, CachedObject<UserStatistics>>();
            StatsRepository = new UserStatisticsRepository(context, repositoryName);
            CacheCleaner.StartCleaner();
        }

        public void Cache(UserStatistics stats)
        {
            if (stats != null && !UserStatsCache.ContainsKey(stats.Date))
            {
                var cachedStats = new CachedObject<UserStatistics>(stats);
                UserStatsCache.Add(stats.Date, cachedStats);
            }
        }

        public void Uncache(DateTime id)
        {
            if (UserStatsCache.ContainsKey(id))
                UserStatsCache.Remove(id);
        }

        public void CleanCache()
        {
            foreach (var stats in UserStatsCache)
            {
                if (stats.Value.CachedTime() > StoreCachedObjectsSecondsTime)
                    Uncache(stats.Key);
            }
        }

        public void Recache(UserStatistics entity)
        {
            Uncache(entity.Date);
            Cache(entity);
        }

        public ICollection<UserStatistics> FetchAll()
        {
            try
            {
                return StatsRepository.FetchAll();
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : fetch all user statistics");
            }

        }

        public IEnumerable<UserStatistics> FindAllBy(Expression<Func<UserStatistics, bool>> predicate)
        {
            try
            {
                return StatsRepository.FindAllBy(predicate);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : find all user stats by predicate");
            }
        }

        public UserStatistics FindOneBy(Expression<Func<UserStatistics, bool>> predicate)
        {
            var stats = UserStatsCache.Values.Select(u => u.Element).AsQueryable().Where(predicate).FirstOrDefault();
            if (stats == null)
            {
                try
                {
                    stats = StatsRepository.FindOneBy(predicate);
                }
                catch (Exception e)
                {
                    SystemLogger.logger.Error(e.ToString());
                    throw new DatabaseException("Faild : find user by predicate");
                }
                Cache(stats);
            }
            return stats;
        }

        public UserStatistics GetByIdOrNull(DateTime id, Expression<Func<UserStatistics, DateTime>> idFunc)
        {
            var stats = UserStatsCache.ContainsKey(id) ? UserStatsCache[id].GetAccessElement() : null;
            if (stats == null)
            {
                try
                {
                    stats = StatsRepository.GetByIdOrNull(id, idFunc);

                }
                catch (Exception e)
                {
                    SystemLogger.logger.Error(e.ToString());
                    throw new DatabaseException("Faild : get user by id");
                }
                Cache(stats);
            }
            return stats;
        }

        public void Insert(UserStatistics entity)
        {
            try
            {
                StatsRepository.Insert(entity);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : insert user");
            }

        }

        public IQueryable<UserStatistics> QueryAll()
        {
            try
            {
                return StatsRepository.QueryAll();
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : query all user stats");
            }


        }

        public void Remove(UserStatistics entity, DateTime id, Expression<Func<UserStatistics, DateTime>> idFunc)
        {
            Uncache(id);
            try
            {
                StatsRepository.Remove(entity, id, idFunc);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : remove user stats");
            }

        }

        public void Update(UserStatistics entity, DateTime id, Expression<Func<UserStatistics, DateTime>> idFunc)
        {
            try
            {
                StatsRepository.Update(entity, id, idFunc);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : update user stats");
            }


            if (UserStatsCache.ContainsKey(id))
                Recache(entity);
        }

        public void Upsert(UserStatistics entity, DateTime id, Expression<Func<UserStatistics, DateTime>> idFunc)
        {
            try
            {
                StatsRepository.Upsert(entity, id, idFunc);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : upsert user stats");
            }


            if (UserStatsCache.ContainsKey(id))
                Recache(entity);
        }

        public IEnumerable<UserStatistics> GetStatisticsByRange(DateTime from, DateTime to)
        {
            try
            {
                return StatsRepository.GetStatisticsByRange(from, to);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : get user statistics in range " + from + " - " + to);
            }
        }

        public void UpdateStatistics(UserTypes type)
        {
            try
            {
                StatsRepository.UpdateStatistics(type);
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : update user statistics");
            }
        }
    }
}