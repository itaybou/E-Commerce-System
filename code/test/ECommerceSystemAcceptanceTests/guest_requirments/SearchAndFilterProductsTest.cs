using ECommerceSystem.DataAccessLayer;
using ECommerceSystem.Models;
using ECommerceSystem.Utilities;
using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;
using System.Collections.Generic;

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
            DataAccess.Instance.DropTestDatabase();
            DataAccess.Instance.SetTestContext();
            _bridge = Driver.getAcceptanceBridge();
            

            uname = "test_user1";
            pswd = "Hell0World";

            _bridge.register(uname, pswd, "user", "userlname", "mymail@mail.com");
            _bridge.login(uname, pswd);
            //_bridge.openStoreWithProducts("store1", uname, new List<string>() { { "product1" }, { "product2" }, { "product3" } });
            //_bridge.openStoreWithProducts("store2", uname, new List<string>() { { "product2" }, { "product5" }, { "product4" } });
            _bridge.openStore("store1");
            _bridge.addProductInv("store1", "d", "product1", 100, 100, Category.ART, new List<string> {  "art"  }, -1, -1, "");
            _bridge.addProductInv("store1", "d", "product2", 50, 100, Category.AUTOMOTIVE, new List<string> {  "auto" ,  "prod2"  }, -1, -1, "");
            _bridge.addProductInv("store1", "d", "product3", 10, 100, Category.BABIES, new List<string> {  "baby" , "prod3"  }, -1, -1, "");

            _bridge.openStore("store2");
            _bridge.addProductInv("store2", "d", "product4", 20, 100, Category.CELLPHONES, new List<string>(), -1, -1, "");
            _bridge.addProductInv("store2", "d", "product5", 100, 100, Category.BABIES, new List<string> {  "baby"  }, -1, -1, "");
            _bridge.addProductInv("store2", "d", "product2", 40, 100, Category.AUTOMOTIVE, new List<string> {  "prod2"  }, -1, -1, "");

            _bridge.logout();
        }

        [SetUp]
        public void setUp()
        {
            _bridge.initSessions();
        }

        //[TearDown]
        //public void tearDown()
        //{
        //    _bridge.initSessions();
        //}

        [OneTimeTearDown]
        public void onetimetearDown()
        {
            DataAccess.Instance.DropTestDatabase();
        }

        [TestCase()]
        public void TestSearchByCategory()
        {
            SearchResultModel prods = _bridge.searchProductsByCategory(Category.BABIES.ToString(), new Range<double>(0, 500), new Range<double>(0, 500), new Range<double>(0, 500));
            Assert.AreEqual(2, prods.ProductResults.Count);
            prods = _bridge.searchProductsByCategory(Category.ART.ToString(), new Range<double>(0, 500), new Range<double>(0, 500), new Range<double>(0, 500));
            Assert.AreEqual(1, prods.ProductResults.Count);
            prods = _bridge.searchProductsByCategory(Category.BOOKS.ToString(), new Range<double>(0, 500), new Range<double>(0, 500), new Range<double>(0, 500));
            Assert.AreEqual(0, prods.ProductResults.Count);
            prods = _bridge.searchProductsByCategory("", new Range<double>(0, 500), new Range<double>(0, 500), new Range<double>(0, 500));
            Assert.AreEqual(0, prods.ProductResults.Count);
            //price is low then exist
            prods = _bridge.searchProductsByCategory(Category.ART.ToString(), new Range<double>(0, 50), new Range<double>(0, 500), new Range<double>(0, 500));
            Assert.AreEqual(0, prods.ProductResults.Count);
            //just one exist below the price
            prods = _bridge.searchProductsByCategory(Category.BABIES.ToString(), new Range<double>(0, 50), new Range<double>(0, 500), new Range<double>(0, 500));
            Assert.AreEqual(1, prods.ProductResults.Count);
        }

        [TestCase()]
        public void TestSearchByKeywords()
        {
            SearchResultModel prods = _bridge.searchProductsByKeyword(new List<string>() {"baby"}, Category.BABIES.ToString(), new Range<double>(0, 500), new Range<double>(0, 500), new Range<double>(0, 500));
            Assert.AreEqual(2, prods.ProductResults.Count);
            prods = _bridge.searchProductsByKeyword(new List<string>() { "prod3" }, Category.BABIES.ToString(), new Range<double>(0, 500), new Range<double>(0, 500), new Range<double>(0, 500));
            Assert.AreEqual(1, prods.ProductResults.Count);
            prods = _bridge.searchProductsByKeyword(new List<string>() { "prod2" }, "", new Range<double>(0, 500), new Range<double>(0, 500), new Range<double>(0, 500));
            Assert.AreEqual(2, prods.ProductResults.Count);
            prods = _bridge.searchProductsByKeyword(new List<string>() { "prod2" , "prod3" }, "", new Range<double>(0, 500), new Range<double>(0, 500), new Range<double>(0, 500));
            Assert.AreEqual(3, prods.ProductResults.Count);
            //just one exist below the price
            prods = _bridge.searchProductsByKeyword(new List<string>() { "baby" }, "", new Range<double>(0, 50), new Range<double>(0, 500), new Range<double>(0, 500));
            Assert.AreEqual(1, prods.ProductResults.Count);
        }

        [TestCase()]
        public void TestSearchByName()
        {
            SearchResultModel prods = _bridge.searchProductsByName("product4", "", new Range<double>(0, 500), new Range<double>(0, 500), new Range<double>(0, 500));
            Assert.AreEqual(1, prods.ProductResults.Count);
            prods = _bridge.searchProductsByName("product2", "", new Range<double>(0, 500), new Range<double>(0, 500), new Range<double>(0, 500));
            Assert.AreEqual(2, prods.ProductResults.Count);
            prods = _bridge.searchProductsByName("product5", Category.BABIES.ToString(), new Range<double>(0, 500), new Range<double>(0, 500), new Range<double>(0, 500));
            Assert.AreEqual(1, prods.ProductResults.Count);
            prods = _bridge.searchProductsByName("", Category.BABIES.ToString(), new Range<double>(0, 500), new Range<double>(0, 500), new Range<double>(0, 500));
            Assert.AreEqual(0, prods.ProductResults.Count);
            //just one exist below the price
            prods = _bridge.searchProductsByName("product2", "", new Range<double>(0, 50), new Range<double>(0, 500), new Range<double>(0, 500));
            Assert.AreEqual(2, prods.ProductResults.Count);

        }
    }
}