using ECommerceSystem.DomainLayer.SystemManagement;
using ECommerceSystem.DomainLayer.Utilities;
using ECommerceSystem.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var range = new Range<double>(1.0, 3.0);
            range.inRange(2.0);
            UserServices s = new UserServices();
            s.register("itay", "itAy5dad", "fname", "lname", "mail@mail");
            SystemLogger.logger.Trace("hello");
        }
    }
}
