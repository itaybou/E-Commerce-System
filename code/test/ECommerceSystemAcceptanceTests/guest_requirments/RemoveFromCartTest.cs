using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;
using System.Collections.Generic;

namespace ECommerceSystemAcceptanceTests.guest_requirments
{
    // Requirment 2.7.1
    [TestFixture()]
    internal class RemoveFromCartTest
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
        public void TestRemoveFromCart()
        {
            Assert.False(_bridge.RemoveFromCart(1));
            _bridge.AddTocart(1, 10);
            Assert.True(_bridge.RemoveFromCart(1));
            _bridge.AddTocart(1, 10);
            _bridge.AddTocart(2, 10);
            Assert.True(_bridge.RemoveFromCart(2));
            Assert.False(_bridge.RemoveFromCart(3));
        }
        */
    }
}