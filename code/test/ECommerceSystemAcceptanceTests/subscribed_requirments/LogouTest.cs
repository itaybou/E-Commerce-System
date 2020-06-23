using ECommerceSystem.DataAccessLayer;
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
            DataAccess.Instance.SetTestContext();
            _bridge.initSessions();

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
            _bridge.initSessions();
        }


        [OneTimeTearDown]
        public void oneTimetearDown()
        {
            DataAccess.Instance.DropTestDatabase();
        }

        [TestCase()]
        public void TestLogoutNotLoggedIn()
        {
            try
            {
                Assert.IsFalse(_bridge.logout());
                Assert.Fail();
            }
            catch(Exception goodException)
            {

            }
        }

        [TestCase()]
        public void TestLogoutIfLoggedIn()
        {
            Assert.IsTrue(_bridge.login(uname, pswd)); // login
            Assert.IsTrue(_bridge.logout());  // logout
        }

    }
}