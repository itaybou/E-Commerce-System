using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.Models;
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
            _bridge.addProductInv("store1", "d", "product1", 100, 100, Category.ART, new List<string>(), -1, -1, "");
            _bridge.addProductInv("store1", "d", "product2", 100, 100, Category.ART, new List<string>(), -1, -1, "");
            _bridge.addProductInv("store1", "d", "product3", 100, 100, Category.ART, new List<string>(), -1, -1, "");

            _bridge.openStore("store2");
            _bridge.addProductInv("store2", "d", "product4", 100, 100, Category.ART, new List<string>(), -1, -1, "");
            _bridge.addProductInv("store2", "d", "product5", 100, 100, Category.ART, new List<string>(), -1, -1, "");
            _bridge.addProductInv("store2", "d", "product6", 100, 100, Category.ART, new List<string>(), -1, -1, "");

            _bridge.openStore("store3");
            _bridge.addProductInv("store3", "d", "product7", 100, 100, Category.ART, new List<string>(), -1, -1, "");

            //_bridge.openStoreWithProducts("store1", uname, new List<string>() { { "product1" }, { "product2" }, { "product3" } });
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
        public void TestViewAllStoreProducts()
        {
            var prods = _bridge.ViewProdcutStoreInfo();
            Assert.AreEqual(prods.Keys.First().Name, "store1");
            Assert.AreEqual(prods.Values.First().Count, 3);
            Assert.AreEqual(prods.Count, 3);
            Assert.AreEqual(prods.Values.SelectMany(p => p).ToList().Count, 7);
        }

        [TestCase()]
        public void TestViewAllStoreProductsAfterDeleteProduct()
        {
            _bridge.login(uname, pswd);
            _bridge.deleteProductInv("store1", "product1");
            var prods = _bridge.ViewProdcutStoreInfo();
            Assert.AreEqual(prods.Keys.First().Name, "store1");
            Assert.AreEqual(prods.Values.First().Count, 2);
            Assert.AreEqual(prods.Count, 3);
            Assert.AreEqual(prods.Values.SelectMany(p => p).ToList().Count, 6);
            _bridge.logout();
        }
    }
}