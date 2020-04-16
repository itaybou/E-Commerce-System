using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;
using System;

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
            _bridge.usersCleanUp();
        }

        [TestCase()]
        public void TestRegistrationNotAllowingBadPasswords()
        {
            var pswd = "";  // Empty password fails
            Assert.False(_bridge.register(uname, pswd, fname, lname, email));
            pswd = "H3llo"; // Short password fails (6 - 15 characters)
            Assert.False(_bridge.register(uname, pswd, fname, lname, email));
            pswd = "aaaaaaaaaaaaaaaaaA34dc"; // Long password fails (6 - 15 characters)
            Assert.False(_bridge.register(uname, pswd, fname, lname, email));
            pswd = "helloWorld"; // Missing numeric character
            Assert.False(_bridge.register(uname, pswd, fname, lname, email));
            pswd = "hello4orld"; // Missing uppercase character
            Assert.False(_bridge.register(uname, pswd, fname, lname, email));
        }

        [TestCase()]
        public void TestRegistrationNotAllowingBadEmail()
        {
            var pswd = "H3lloWorld";
            Assert.False(_bridge.register(uname, pswd, fname, lname, wrongemail));
            Assert.False(_bridge.register(uname, pswd, fname, lname, "@mail.com"));
        }

        [TestCase()]
        public void TestRegistrationSuccess()
        {
            var pswd = "H3lloWorld"; // valid password
            Assert.True(_bridge.register(uname, pswd, fname, lname, email)); // valid username, email and password
        }

        [TestCase()]
        public void TestRegistrationUserAlreadyExists()
        {
            var pswd = "H3lloWorld";
            Assert.True(_bridge.register(uname, pswd, fname, lname, email));    
            Assert.False(_bridge.IsUserLogged(uname));  // not logged after registration
            Assert.True(_bridge.IsUserSubscribed(uname));  // Registration succeded, user is subscribed
            Assert.False(_bridge.register(uname, "V4lidPass", "user2", "lname2", "mail2@mail.com"));    // try to register again with the same username
        }
    }
}