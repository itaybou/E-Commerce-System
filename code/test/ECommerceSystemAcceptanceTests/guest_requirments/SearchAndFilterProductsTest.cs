using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ECommerceSystemAcceptanceTests.guest_requirments
{
    // Requirment 2.5
    [TestFixture()]
    internal class SearchAndFilterProductsTest
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
            _bridge.openStoreWithProducts("store2", uname, new List<string>() { { "product2" }, { "product5" }, { "product4" } });
        }

        [TearDown]
        public void tearDown()
        {
            _bridge.usersCleanUp();
            _bridge.storesCleanUp();
        }

        [TestCase()]
        public void TestViewAllStoreProducts()
        {
            var prods = _bridge.SearchAndFilterProducts(null, null, null, new List<string>(), 0, 0);
            Assert.AreEqual(prods.Count, 6);
            prods = _bridge.SearchAndFilterProducts("product2", null, null, new List<string>(), 0, 0);
            Assert.AreEqual(prods.Count, 2);
            prods = _bridge.SearchAndFilterProducts("product1", null, null, new List<string>(), 0, 0);
            Assert.AreEqual(prods.Count, 1);
            prods = _bridge.SearchAndFilterProducts(null, "electronics", null, new List<string>(), 0, 0);
            Assert.AreEqual(prods.Count, 4);
            prods = _bridge.SearchAndFilterProducts(null, "babies", null, new List<string>(), 0, 0);
            Assert.AreEqual(prods.Count, 2);
            prods = _bridge.SearchAndFilterProducts(null, null, new List<string>() { { "hello" } }, new List<string>(), 0, 0);
            Assert.AreEqual(prods.Count, 6);
            prods = _bridge.SearchAndFilterProducts(null, null, new List<string>() { { "world" }, { "name"} }, new List<string>(), 0, 0);
            Assert.AreEqual(prods.Count, 6);
            prods = _bridge.SearchAndFilterProducts(null, null, new List<string>() { { "itay" } }, new List<string>(), 0, 0);
            Assert.AreEqual(prods.Count, 0);
            prods = _bridge.SearchAndFilterProducts(null, null, new List<string>() { { "world" } }, new List<string>(), 0, 0);
            Assert.AreEqual(prods.Count, 2);
            prods = _bridge.SearchAndFilterProducts(null, null, new List<string>() { { "inigo" } }, new List<string>(), 0, 0);
            Assert.AreEqual(prods.Count, 4);
            prods = _bridge.SearchAndFilterProducts(null, "electronics", null, new List<string>() { { "category"} }, 0, 0);
            Assert.AreEqual(prods.Count, 4);
            _bridge.cancelSearchFilters();
            prods = _bridge.SearchAndFilterProducts(null, "electronics", null, new List<string>() { { "category" }, { "price"} }, 2, 4);
            Assert.AreEqual(prods.Count, 2);
            _bridge.cancelSearchFilters();
            prods = _bridge.SearchAndFilterProducts(null, null, null, new List<string>() { { "price" } }, 2, 4);
            Assert.AreEqual(prods.Count, 4);
            _bridge.cancelSearchFilters();
            prods = _bridge.SearchAndFilterProducts(null, null, null, new List<string>() { { "price" } }, 10, 20);
            Assert.AreEqual(prods.Count, 0);
            _bridge.cancelSearchFilters();
            prods = _bridge.SearchAndFilterProducts(null, "electronics", null, new List<string>() { { "price" } }, 2, 2);
            Assert.AreEqual(prods.Count, 0);
            _bridge.cancelSearchFilters();
            prods = _bridge.SearchAndFilterProducts(null, null, new List<string>() { { "inigo" } }, new List<string>() { { "price" } }, 4, 7);
            Assert.AreEqual(prods.Count, 4);
            _bridge.cancelSearchFilters();
            prods = _bridge.SearchAndFilterProducts(null, null, new List<string>() { { "inigo" } }, new List<string>() { { "price" } }, 6, 7);
            Assert.AreEqual(prods.Count, 2);
            _bridge.cancelSearchFilters();

        }
    }
}