using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;
using System.Collections.Generic;

namespace ECommerceSystemAcceptanceTests.guest_requirments
{
    // Requirment 2.6
    [TestFixture()]
    internal class AddToCartTest
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

        /*
        [TestCase()]
        public void TestAddToCart()
        {
            var prod = _bridge.AddTocart(1, 20);
            Assert.AreEqual(prod["store1"][1], 20);
            _bridge.login(uname, pswd);
            _bridge.openStoreWithProducts("store2", uname, new List<string>() { { "product1" }, { "product4" }, { "product5" } });
            _bridge.logout();
            prod = _bridge.AddTocart(1, 20);
            Assert.AreEqual(prod["store1"][1], 20);
            Assert.AreEqual(prod["store2"][1], 20);
            prod = _bridge.AddTocart(1, 20);
            Assert.AreEqual(prod["store1"][1], 40);
            Assert.AreEqual(prod["store2"][1], 40);
            prod = _bridge.AddTocart(2, 20);
            Assert.AreEqual(prod["store1"][2], 20);
            Assert.AreEqual(prod["store2"][2], 20);
            prod = _bridge.AddTocart(3, 20);
            Assert.AreEqual(prod["store1"][3], 20);
            Assert.AreEqual(prod["store2"][3], 20);
        }
        */
    }
}