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
    public class LocationPolicyTests
    {
        List<string> empty;
        List<string> bannedLocations;


        LocationPolicy emptyPolicy;
        LocationPolicy bannedLocationsPolicy;


        [OneTimeSetUp]
        public void setUpFixture()
        {
            empty = new List<string>();
            bannedLocations = new List<string>() { "Iran", "Iraq" };
            emptyPolicy = new LocationPolicy(empty, Guid.NewGuid());
            bannedLocationsPolicy = new LocationPolicy(bannedLocations, Guid.NewGuid());
        }

        [Test()]
        public void canBuyTest()
        {
            Assert.IsFalse(emptyPolicy.canBuy(null, 0, null));
            Assert.IsFalse(bannedLocationsPolicy.canBuy(null, 0, null));


            Assert.IsTrue(emptyPolicy.canBuy(null, 0, "Iran"));
            Assert.IsTrue(emptyPolicy.canBuy(null, 0, ""));


            Assert.IsFalse(bannedLocationsPolicy.canBuy(null, 0, "Iran"));
            Assert.IsFalse(bannedLocationsPolicy.canBuy(null, 0, "Iraq"));
            Assert.IsTrue(bannedLocationsPolicy.canBuy(null, 0, ""));
            Assert.IsTrue(bannedLocationsPolicy.canBuy(null, 0, "Israel"));


        }

    }
}