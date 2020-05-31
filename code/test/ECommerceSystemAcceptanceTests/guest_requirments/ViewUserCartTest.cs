using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;
using System.Collections.Generic;

namespace ECommerceSystemAcceptanceTests.guest_requirments
{
    // Requirment 2.7
    [TestFixture()]
    internal class ViewUserCartTest
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
                new List<string>() { { "product1" }, { "product2" }, { "product3" } });
            _bridge.logout();
        }

        [TearDown]
        public void tearDown()
        {
            _bridge.usersCleanUp();
            _bridge.storesCleanUp();
        }

        /*
        [TestCase()]
        public void TestViewUserCartTest()
        {
            Assert.IsEmpty(_bridge.ViewUserCart().ToList());
            _bridge.AddTocart(1, 10);
            Assert.AreEqual(_bridge.getUserCartDetails().Keys.Count,1);
            Assert.AreEqual(_bridge.ViewUserCart().Keys.First(),"store1");
            _bridge.AddTocart(2, 10);
            Assert.AreEqual(_bridge.ViewUserCart()["store1"].Count(), 2);
            _bridge.login(uname, pswd);
            _bridge.openStoreWithProducts("store2", uname, new List<string>() { { "product4" }, { "product5" }, { "product6" } });
            _bridge.logout();
            _bridge.AddTocart(3, 10);
            Assert.True(_bridge.ViewUserCart().ContainsKey("store2"));
            Assert.AreEqual(_bridge.ViewUserCart()["store2"].Count(), 1);
        }
        */
    }
}