using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceSystemAcceptanceTests.guest_requirments
{
    // Requirment 2.4
    [TestFixture()]
    internal class ViewStoreProductInfoTest
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
            _bridge.openStoreWithProducts("store1", uname, new List<string>() { { "product1" }, { "product2" }, { "product3" } });
            _bridge.logout();
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
            var prods = _bridge.ViewProdcutStoreInfo();
            Assert.AreEqual(prods.Keys.First(), "store1");
            Assert.AreEqual(prods.Values.First().Count, 3);
            _bridge.login(uname, pswd);
            _bridge.openStoreWithProducts("store2", uname, new List<string>() { { "product4" }, { "product5" }, { "product6" } });
            _bridge.logout();
            prods = _bridge.ViewProdcutStoreInfo();
            Assert.AreEqual(prods.Count, 2);
            Assert.AreEqual(prods.Values.SelectMany(p => p).ToList().Count, 6);
            _bridge.login(uname, pswd);
            _bridge.openStoreWithProducts("store3", uname, new List<string>() { { "product0" } });
            _bridge.logout();
            prods = _bridge.ViewProdcutStoreInfo();
            Assert.AreEqual(prods.Count, 3);
            Assert.AreEqual(prods.Values.SelectMany(p => p).ToList().Count, 7);
        }
    }
}