using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ECommerceSystemAcceptanceTests.guest_requirments
{
    // Requirment 2.7.2
    [TestFixture()]
    internal class ChangeProductCartQuantityTest
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
            _bridge.openStoreWithProducts("store1", uname,
                new List<string>() {{"product1"}, {"product2"}, {"product3"}});
            _bridge.logout();
        }

        [TearDown]
        public void tearDown()
        {
            _bridge.usersCleanUp();
            _bridge.storesCleanUp();
        }

        [TestCase()]
        public void TestChangeProductCartQuantity()
        {
            Assert.False(_bridge.ChangeProductCartQuantity(1,5));
            _bridge.AddTocart(1, 10);
            Assert.True(_bridge.ChangeProductCartQuantity(1,20));
            Assert.False(_bridge.ChangeProductCartQuantity(1, 30));
            Assert.False(_bridge.ChangeProductCartQuantity(2, 1));
            _bridge.AddTocart(2, 10);
            Assert.True(_bridge.ChangeProductCartQuantity(1, 1));
            Assert.True(_bridge.ChangeProductCartQuantity(1, 0));






        }
    }
}