using NUnit.Framework;
using System;

namespace ECommerceSystem.DomainLayer.UserManagement.security.Tests
{
    [TestFixture()]
    public class ValidationTests
    {
        private string valid_pswd1 = "Hell0World";
        private string valid_pswd2 = "aaaaaaaaaAaaa4a";
        private string valid_pswd3 = "H3llo5";
        private string invalid_pswd1 = "H3llo"; // Too short (length necessary 6-15)
        private string invalid_pswd2 = "H3lloWorlllllllllllllld"; // Too long (length necessary 6-15)
        private string invalid_pswd3 = "h3lloworld"; // missing uppercase character
        private string invalid_pswd4 = "Helloworld"; // missing numeric character
        private string invalid_pswd5 = "helloworld"; // missing numeric and uppercase characters
        private string invalid_pswd6 = ""; // empty string
        private string invalid_pswd7 = "                 "; // whitespace string
        private string invalid_pswd8 = null; // null password

        private string valid_email1 = "email@google.com";
        private string valid_email2 = "email@email.com";
        private string invalid_email1 = "email";
        private string invalid_email2 = "NotAnEmail";
        private string invalid_email3 = "@email";

        [Test()]
        public void IsValidEmailTest()
        {
            string error;
            Assert.True(Validation.IsValidEmail(valid_email1, out error));
            Assert.True(Validation.IsValidEmail(valid_email2, out error));
            Assert.False(Validation.IsValidEmail(invalid_email1, out error));
            Assert.False(Validation.IsValidEmail(invalid_email2, out error));
            Assert.False(Validation.IsValidEmail(invalid_email3, out error));
        }

        [Test()]
        public void isValidPasswordTest()
        {
            string error;
            Assert.True(Validation.isValidPassword(valid_pswd1, out error));
            Assert.True(Validation.isValidPassword(valid_pswd2, out error));
            Assert.False(Validation.isValidPassword(invalid_pswd1, out error));
            Assert.False(Validation.isValidPassword(invalid_pswd2, out error));
            Assert.False(Validation.isValidPassword(invalid_pswd3, out error));
            Assert.False(Validation.isValidPassword(invalid_pswd4, out error));
            Assert.False(Validation.isValidPassword(invalid_pswd5, out error));
            Assert.False(Validation.isValidPassword(invalid_pswd6, out error));
            Assert.False(Validation.isValidPassword(invalid_pswd7, out error));
            Assert.False(Validation.isValidPassword(invalid_pswd8, out error));
        }
    }
}