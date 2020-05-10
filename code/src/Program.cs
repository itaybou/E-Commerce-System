using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.DomainLayer.SystemManagement.logger;
using ECommerceSystem.Utilities;
using ECommerceSystem.Models;
using ECommerceSystem.ServiceLayer;
using System;
using System.Linq;

namespace ECommerceSystem
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SystemLogger.initLogger();
            hello("helloworld", 3, 4.5);

        }
        
        [Trace("info")]
        public static int hello(string s, int i, double b)
        {
            return world(5);
        }

        [Trace("info")]
        public static int world(int i)
        {
            SystemLogger.LogError("hello world");
            return 1;
        }
    }
}