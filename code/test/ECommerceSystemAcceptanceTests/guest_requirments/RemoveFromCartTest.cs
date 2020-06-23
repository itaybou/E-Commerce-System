using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.Models;
using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ECommerceSystemAcceptanceTests.guest_requirments
{
    // Requirment 2.7.1
    [TestFixture()]
    internal class RemoveFromCartTest
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
            _bridge.login(uname, pswd);
            _bridge.openStore("store1");
            productID1 = _bridge.addProductInv("store1", "d", "product1", 100, 25, Category.ART, new List<string>(), -1, -1, "");
            productID2 = _bridge.addProductInv("store1", "d", "product2", 100, 25, Category.ART, new List<string>(), -1, -1, "");
            productID3 = _bridge.addProductInv("store1", "d", "product2", 100, 25, Category.ART, new List<string>(), -1, -1, "");
            _bridge.logout();
        }

        [TearDown]
        public void tearDown()
        {
            DataAccess.Instance.DropTestDatabase();
            _bridge.initSessions();
        }

        
        [TestCase()]
        public void TestRemoveFromCart()
        {
            Assert.IsFalse(_bridge.RemoveFromCart(productID1));
            _bridge.AddTocart(productID1, "store1", 10);
            Assert.IsTrue(_bridge.RemoveFromCart(productID1));
            _bridge.AddTocart(productID1, "store1", 10);
            _bridge.AddTocart(productID2, "store1", 10);
            Assert.IsTrue(_bridge.RemoveFromCart(productID2));
            Assert.IsFalse(_bridge.RemoveFromCart(productID3));
        }
        
    }
}