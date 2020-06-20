using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Models;
using System;
using System.Collections.Generic;

namespace ECommerceSystem.DataAccessLayer.repositories
{
    public interface IUserStatisticsRepository : IRepository<UserStatistics, DateTime>
    {
        IEnumerable<UserStatistics> GetStatisticsByRange(DateTime from, DateTime to);

        void UpdateStatistics(UserTypes type);
    }
}