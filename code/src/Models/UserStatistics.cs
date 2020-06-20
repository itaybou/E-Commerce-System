using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Models
{
    public class UserStatistics
    {
        public DateTime Date { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<UserTypes, long> Statistics { get; set; }
        public IEnumerable<UserStatistics> AllStatistics { get; set; }

        public UserStatistics(DateTime date)
        {
            Date = date;
            Statistics = new Dictionary<UserTypes, long>()
            {
                {UserTypes.Guests, 0 },
                {UserTypes.Subscribed, 0 },
                {UserTypes.StoreManagers, 0 },
                {UserTypes.StoreOwners, 0 },
                {UserTypes.Admins, 0 }
            };
        }

        public UserStatistics(IEnumerable<UserStatistics> statistics)
        {
            Date = DateTime.Now;
            AllStatistics = statistics;
            Statistics = new Dictionary<UserTypes, long>()
            {
                {UserTypes.Guests, 0 },
                {UserTypes.Subscribed, 0 },
                {UserTypes.StoreManagers, 0 },
                {UserTypes.StoreOwners, 0 },
                {UserTypes.Admins, 0 }
            };
            foreach(var stats in statistics)
            {
                Statistics[UserTypes.Guests] += stats.Statistics[UserTypes.Guests];
                Statistics[UserTypes.Subscribed] += stats.Statistics[UserTypes.Subscribed];
                Statistics[UserTypes.StoreManagers] += stats.Statistics[UserTypes.StoreManagers];
                Statistics[UserTypes.StoreOwners] += stats.Statistics[UserTypes.StoreOwners];
                Statistics[UserTypes.Admins] += stats.Statistics[UserTypes.Admins];
            }
        }
    }
}
