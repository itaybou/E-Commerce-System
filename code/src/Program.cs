﻿using ECommerceSystem.DomainLayer.StoresManagement;
using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.DomainLayer.Utilities;
using ECommerceSystem.ServiceLayer;
using System;
using System.Linq;

namespace ECommerceSystem
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var range = new Range<double>(1.0, 3.0);


            range.inRange(2.0);
            UserServices s = new UserServices();
            s.register("itay", "itAy5dad", "fname", "lname", "mail@mail");
        }
    }
}