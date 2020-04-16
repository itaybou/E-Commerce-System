using System;
using System.Collections.Generic;
using System.Text;
using ECommerceSystemAcceptanceTests.adapters;
using NUnit.Framework;

namespace ECommerceSystemAcceptanceTests.guest_requirments
{
    // Requirment 2.2
    [TestFixture()]
    class RegisterTest
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

        [TestCase()]
        public void TestRegistrationNotAllowingBadPasswords()
        {
            var pswd = "";  // Empty password fails
            Assert.Throws<Exception>(() => _bridge.register(uname, pswd, fname, lname, email));
            pswd = "H3llo"; // Short password fails (6 - 15 characters)
            Assert.Throws<Exception>(() => _bridge.register(uname, pswd, fname, lname, email));
        }

        [TestCase()]
        public void TestRegistrationNotAllowingBadEmail()
        {
            var pswd = "H3lloWorld";
            Assert.False(_bridge.register(uname,pswd,fname,lname,wrongemail));
        }

        [TestCase()]
        public void TestRegistrationSuccess()
        {
            var pswd = "H3lloWorld";
            Assert.True(_bridge.register(uname, pswd, fname, lname, email));
        }

    }
}
