using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.Models;
using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystemAcceptanceTests.guest_requirments
{
    // Requirment 2.6
    [TestFixture()]
    internal class AddToCartTest
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
            _bridge.initSessions();
        }

        [OneTimeTearDown]
        public void oneTimetearDown()
        {
            DataAccess.Instance.DropTestDatabase();
        }

        [TestCase()]
        public void TestAddToCartNotMatchProductAndStore()
        {
            Assert.IsFalse(_bridge.AddTocart(productID1, "store2" , 20));
        }

        [TestCase()]
        public void TestAddToCartExceedQuantity()
        {
            Assert.IsFalse(_bridge.AddTocart(productID1, "store1", 200));
        }

        [TestCase()]
        public void TestAddToCart()
        {
            Assert.IsTrue(_bridge.AddTocart(productID1, "store1" , 20));
            ShoppingCartModel cart = _bridge.ViewUserCart();
            Assert.AreEqual(20, cart.Cart.ElementAt(0).Value.ElementAt(0).Item2);

            Assert.IsTrue(_bridge.AddTocart(productID3, "store2", 10));
            cart = _bridge.ViewUserCart();
            Assert.AreEqual(2, cart.Cart.Count);
            Assert.AreEqual(10, cart.Cart.Where(s => s.Key.Name.Equals("store2")).Where(p => p.Key.Equals(productID3)).First().Value);

            //prod = _bridge.AddTocart(1, 20);
            //Assert.AreEqual(prod["store1"][1], 20);
            //Assert.AreEqual(prod["store2"][1], 20);
            //prod = _bridge.AddTocart(1, 20);
            //Assert.AreEqual(prod["store1"][1], 40);
            //Assert.AreEqual(prod["store2"][1], 40);
            //prod = _bridge.AddTocart(2, 20);
            //Assert.AreEqual(prod["store1"][2], 20);
            //Assert.AreEqual(prod["store2"][2], 20);
            //prod = _bridge.AddTocart(3, 20);
            //Assert.AreEqual(prod["store1"][3], 20);
            //Assert.AreEqual(prod["store2"][3], 20);
        }
        
    }
}