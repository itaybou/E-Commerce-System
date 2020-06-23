using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.Models;
using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystemAcceptanceTests.guest_requirments
{
    // Requirment 2.7
    [TestFixture()]
    internal class ViewUserCartTest
    {
        private string uname, pswd;
        private IBridgeAdapter _bridge;
        Guid productID1;
        Guid productID2;
        Guid productID3;
        Guid productID4;

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
            _bridge.openStore("store2");
            productID3 = _bridge.addProductInv("store2", "d", "product3", 100, 25, Category.ART, new List<string>(), -1, -1, "");
            productID4 = _bridge.addProductInv("store2", "d", "product4", 100, 25, Category.ART, new List<string>(), -1, -1, "");
            _bridge.logout();
        }

        [TearDown]
        public void tearDown()
        {
            DataAccess.Instance.DropTestDatabase();
            _bridge.initSessions();
        }

        
        [TestCase()]
        public void TestViewUserCartTest()
        {
           

            Assert.AreEqual(0, _bridge.ViewUserCart().Cart.Count);

            _bridge.AddTocart(productID1, "store1", 10);
            Assert.AreEqual(1,_bridge.ViewUserCart().Cart.Count);
            Assert.AreEqual("store1", _bridge.ViewUserCart().Cart.ElementAt(0).Key.Name);

            _bridge.AddTocart(productID2, "store1", 10);

            Assert.AreEqual(_bridge.ViewUserCart().Cart.ElementAt(0).Value.Count, 2);

            

            _bridge.AddTocart(productID3, "store2", 10);

            //Assert.AreEqual("store2", _bridge.ViewUserCart().Cart.ElementAt(1).Key.Name);
            Assert.AreEqual(_bridge.ViewUserCart().Cart.ElementAt(1).Value.Count, 1);
            
        }
        
    }
}