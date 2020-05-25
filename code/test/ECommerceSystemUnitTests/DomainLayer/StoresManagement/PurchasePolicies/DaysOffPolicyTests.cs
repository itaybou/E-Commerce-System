using NUnit.Framework;
using ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.DomainLayer.StoresManagement.PurchasePolicies.Tests
{
    [TestFixture()]
    public class DaysOffPolicyTests
    {

        List<DayOfWeek> today;
        List<DayOfWeek> tommorrow;
        List<DayOfWeek> todayAndTommorrow;

        DaysOffPolicy todayPolicy;
        DaysOffPolicy tommorrowPolicy;
        DaysOffPolicy todayAndTommorrowPolicy;

        [OneTimeSetUp]
        public void setUpFixture()
        {
            today = new List<DayOfWeek>() { DateTime.Today.DayOfWeek };
            tommorrow = new List<DayOfWeek>() { DateTime.Today.AddDays(1).DayOfWeek };
            todayAndTommorrow = new List<DayOfWeek>() { DateTime.Today.AddDays(1).DayOfWeek, DateTime.Today.DayOfWeek };
            todayPolicy = new DaysOffPolicy(today, Guid.NewGuid());
            tommorrowPolicy = new DaysOffPolicy(tommorrow, Guid.NewGuid());
            todayAndTommorrowPolicy = new DaysOffPolicy(todayAndTommorrow, Guid.NewGuid());
        }



        [Test()]
        public void canBuyTest()
        {
            Assert.IsTrue(tommorrowPolicy.canBuy(null, 0, null));
            Assert.IsFalse(todayPolicy.canBuy(null, 0, null));
            Assert.IsFalse(todayAndTommorrowPolicy.canBuy(null, 0, null));
        }

    }
}