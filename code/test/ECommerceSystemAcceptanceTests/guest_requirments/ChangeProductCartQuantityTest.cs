using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.Models;
using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ECommerceSystemAcceptanceTests.guest_requirments
{
    // Requirment 2.7.2
    [TestFixture()]
    internal class ChangeProductCartQuantityTest
    {
        private string uname, pswd;
        private IBridgeAdapter _bridge;

        Guid productID1;
        Guid productID2;
        Guid productID3;

        [OneTimeSetUp]
        public void oneTimeSetup()
        {
            _bridge = Driver.getAcceptanceBridge();
            DataAccess.Instance.SetTestContext();

            uname = "test_user1";
            pswd = "Hell0World";
        }

        [SetUp]
        public void setUp()
        {
            _bridge.register(uname, pswd, "user", "userlname", "mymail@mail.com");
            _bridge.login(uname, pswd);
            _bridge.openStore("store1");
            productID1 = _bridge.addProductInv("store1", "d", "product1", 100, 25, Category.ART, new List<string>(), -1, -1, "");
            productID2 = _bridge.addProductInv("store1", "d", "product2", 100, 25, Category.ART, new List<string>(), -1, -1, "");
            _bridge.logout();
        }

        [TearDown]
        public void tearDown()
        {
            _bridge.initSessions();
        }

        [OneTimeTearDown]
        public void oneTimetearDown()
        {
            DataAccess.Instance.DropTestDatabase();
        }

        [TestCase()]
        public void TestChangeProductCartQuantity()
        {
            Assert.False(_bridge.ChangeProductCartQuantity(productID1, 5));
            _bridge.AddTocart(productID1, "store1", 10);
            Assert.True(_bridge.ChangeProductCartQuantity(productID1, 20));
            Assert.False(_bridge.ChangeProductCartQuantity(productID1, 30));
            Assert.False(_bridge.ChangeProductCartQuantity(productID2, 1));
            _bridge.AddTocart(productID2, "store1", 10);
            Assert.True(_bridge.ChangeProductCartQuantity(productID1, 1));
            Assert.True(_bridge.ChangeProductCartQuantity(productID1, 0));
        }
    }
}