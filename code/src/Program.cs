using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.DomainLayer.UserManagement;
using System;

namespace ECommerceSystem
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IDataAccess data = DataAccess.Instance;

            var admin = new User(new SystemAdmin("itay", "linkin9p", "itay", "bou", "itay@email.com"));
            data.Users.Insert(new User(new Subscribed("itay", "linkin9p", "itay", "bou", "itay@email.com")));
            data.Users.Insert(admin);

            var user = data.Users.GetByIdOrNull(admin.Guid, u => u.Guid);
            Console.ReadLine();
        }
    }
}