using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;
using System;

namespace ECommerceSystemAcceptanceTests.guest_requirments
{
    // Requirment 2.3
    [TestFixture()]
    internal class LogoutTest
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
        }

        [TearDown]
        public void tearDown()
        {
            _bridge.usersCleanUp();
        }

        [TestCase()]
        public void TestLogoutNotLoggedIn()
        {
            Assert.False(_bridge.logout()); // not logged
        }

        [TestCase()]
        public void TestLogoutIfLoggedIn()
        {
            Assert.True(_bridge.login(uname, pswd)); // login
            Assert.True(_bridge.IsUserLogged(uname));   // user logged
            Assert.True(_bridge.logout());  // logout
            Assert.False(_bridge.IsUserLogged(uname));  // no user supposed to be logged off
        }
    }
}