using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.DomainLayer.UserManagement;
using ECommerceSystem.Models;
using ECommerceSystem.ServiceLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ECommerceSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var initializer = new SystemInitializer();
            initializer.Initialize();
            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }
    }
}