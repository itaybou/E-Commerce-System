using ECommerceSystem.DataAccessLayer;
using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;

namespace ECommerceSystemAcceptanceTests.guest_requirments
{
    // Requirment 2.3
    [TestFixture()]
    internal class LoginTest
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
        public void TestNotRegisteredLogin()
        {
            Assert.False(_bridge.login(uname + "not", pswd + " not")); // different username and password
            Assert.False(_bridge.login(uname + "not", pswd));   // same password different username
        }

        [TestCase()]
        public void TestBadUserPassword()
        {
            Assert.False(_bridge.login(uname, pswd + " not")); // registered username, bad password
            Assert.False(_bridge.login(uname, "")); // registered username, bad password
        }

        [TestCase()]
        public void TestLoginSuccess()
        {
            Assert.True(_bridge.login(uname, pswd));
        }
    }
}