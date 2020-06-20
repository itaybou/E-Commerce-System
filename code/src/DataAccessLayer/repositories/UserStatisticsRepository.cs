using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Exceptions;
using ECommerceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DataAccessLayer.repositories
{
    public class UserStatisticsRepository : Repository<UserStatistics, DateTime>, IUserStatisticsRepository
    {
        private static object _statsLock = new object();
        public UserStatisticsRepository(IDbContext context, string repositoryName) : base(context, repositoryName) { }

        public IEnumerable<UserStatistics> GetStatisticsByRange(DateTime from, DateTime to)
        {
            try
            {
                lock(_statsLock)
                {
                    return FetchAll().Where(stats => stats.Date.Date >= from.Date && stats.Date.Date <= to.Date.AddDays(1));
                }
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : get user statistics in range " + from + " - " + to);
            }
        }

        [Obsolete]
        public void UpdateStatistics(UserTypes type)
        {
            try
            {
                lock (_statsLock)
                {
                    var date = DateTime.Now.Date;
                    var dateStats = GetByIdOrNull(date, s => s.Date);
                    if (dateStats == null)
                        dateStats = new UserStatistics(date);
                    if (type != UserTypes.Guests)
                        dateStats.Statistics[UserTypes.Guests]--;
                    var count = dateStats.Statistics[type];
                    dateStats.Statistics[type] = count + 1;
                    Upsert(dateStats, dateStats.Date, s => s.Date);
                }
            }
            catch (Exception e)
            {
                SystemLogger.logger.Error(e.ToString());
                throw new DatabaseException("Faild : update user statistics");
            }
        }
    }
}
