using ECommerceSystem.DataAccessLayer;
using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;

namespace ECommerceSystemAcceptanceTests.guest_requirments
{
    // Requirment 2.2
    [TestFixture()]
    internal class RegisterTest
    {
        private string uname, fname, lname, email, wrongemail;
        private IBridgeAdapter _bridge;

        [OneTimeSetUp]
        public void oneTimeSetup()
        {
            DataAccess.Instance.SetTestContext();
            _bridge = Driver.getAcceptanceBridge();
            uname = "test_user1";
            fname = "user";
            lname = "user_last";
            email = "user@mail.com";
            wrongemail = "useremaildotcom";
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
        public void TestRegistrationNotAllowingBadPasswords()
        {
            var pswd = "";  // Empty password fails
            Assert.False(_bridge.register(uname, pswd, fname, lname, email));
            pswd = "H3llo"; // Short password fails (6 - 15 characters)
            Assert.False(_bridge.register(uname, pswd, fname, lname, email));
            Assert.False(_bridge.login(uname, pswd));
            pswd = "aaaaaaaaaaaaaaaaaA34dc"; // Long password fails (6 - 15 characters)
            Assert.False(_bridge.register(uname, pswd, fname, lname, email));
            Assert.False(_bridge.login(uname, pswd));
            pswd = "helloWorld"; // Missing numeric character
            Assert.False(_bridge.register(uname, pswd, fname, lname, email));
            Assert.False(_bridge.login(uname, pswd));
            pswd = "hello4orld"; // Missing uppercase character
            Assert.False(_bridge.register(uname, pswd, fname, lname, email));
            Assert.False(_bridge.login(uname, pswd));
        }

        [TestCase()]
        public void TestRegistrationNotAllowingBadEmail()
        {
            var pswd = "H3lloWorld";
            Assert.False(_bridge.register(uname, pswd, fname, lname, wrongemail));
            Assert.False(_bridge.login(uname, pswd));
            Assert.False(_bridge.register(uname, pswd, fname, lname, "@mail.com"));
            Assert.False(_bridge.login(uname, pswd));
        }

        [TestCase()]
        public void TestRegistrationSuccess()
        {
            var pswd = "H3lloWorld"; // valid password
            Assert.True(_bridge.register(uname, pswd, fname, lname, email)); // valid username, email and password
            Assert.True(_bridge.login(uname, pswd));
            Assert.True(_bridge.logout());

        }

        [TestCase()]
        public void TestRegistrationUserAlreadyExists()
        {
            var pswd = "H3lloWorld";
            Assert.True(_bridge.register(uname + "1", pswd, fname, lname, email));
            Assert.False(_bridge.register(uname + "1", "V4lidPass", "user2", "lname2", "mail2@mail.com"));    // try to register again with the same username
            Assert.False(_bridge.login(uname + "1", "V4lidPas"));
        }
    }
}