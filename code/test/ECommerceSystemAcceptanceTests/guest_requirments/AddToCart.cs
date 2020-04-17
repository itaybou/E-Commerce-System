using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ECommerceSystemAcceptanceTests.guest_requirments
{
    // Requirment 2.4
    [TestFixture()]
    internal class AddToCart
    {
        private string uname, pswd;
        private IBridgeAdapter _bridge;

        [OneTimeSetUp]
        public void oneTimeSetup()
        {
            _bridge = Driver.getAcceptanceBridge();
            uname = "test_user1";
            pswd = "Hell0World";
        }

        [SetUp]
        public void setUp()
        {
            _bridge.register(uname, pswd, "user", "userlname", "mymail@mail.com");
            _bridge.login(uname, pswd);
            _bridge.openStoreWithProducts("store1", uname, new List<string>() { { "product1" }, { "product2" }, { "product3" } } );
        }

        [TearDown]
        public void tearDown()
        {
            _bridge.usersCleanUp();
            _bridge.storesCleanUp();
        }

        [TestCase()]
        public void TestAddToCart()
        {
            var prod = _bridge.AddTocart(1, 20);
            Assert.AreEqual(prod[1],40);
        }
    }
}