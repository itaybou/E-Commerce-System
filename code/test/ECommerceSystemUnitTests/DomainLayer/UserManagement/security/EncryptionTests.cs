using NUnit.Framework;

namespace ECommerceSystem.DomainLayer.UserManagement.security.Tests
{
    [TestFixture()]
    public class EncryptionTests
    {
        private string _testString1 = "this is a sample TEST STRING with numbers 231345 and signs $#@#@%#$!@#&*()<> SmAlL AND biG lettErs!";
        private string _testString2 = "p4ssWordMOCk";

        [Test()]
        public void EncryptNotSameStringTest()
        {
            Assert.AreNotEqual(Encryption.encrypt(_testString2), Encryption.encrypt(_testString1)); // totally diefferent string
            Assert.AreNotEqual(Encryption.encrypt(_testString2), Encryption.encrypt("p4SsWordMOCk")); // changed one letter case
            Assert.AreNotEqual(Encryption.encrypt(_testString2), Encryption.encrypt("p5ssWordMOCk")); // changed one number
            Assert.AreNotEqual(Encryption.encrypt(_testString2), Encryption.encrypt("p4ssWordMcCk")); // changed one letter
            Assert.AreNotEqual(Encryption.encrypt(_testString1), Encryption.encrypt("this is a sample TEST STRING without numbers 231345 and signs $#@#@%#$!@#&*()<> SmAlL AND biG lettErs!")); // changed onw word
            Assert.AreNotEqual(Encryption.encrypt(_testString1), Encryption.encrypt("this is a sample TEST STRING without numbers 231345 and signs $#@#@%#$?@#&*()<> SmAlL AND biG lettErs!")); // changed one symbol
            Assert.AreNotEqual(Encryption.encrypt(_testString1), Encryption.encrypt("this is a sample TEST STRING withoUt numbers 231345 and signs $#@#@%#$?@#&*()<> SmAlL AND biG lettErs!")); // changed one letter case
            Assert.AreNotEqual(Encryption.encrypt(_testString1), Encryption.encrypt("this is a sample TEST STRIVG without numbers 231345 and signs $#@#@%#$?@#&*()<> SmAlL AND biG lettErs!")); // changed one letter
            Assert.AreNotEqual(Encryption.encrypt(_testString1), Encryption.encrypt("this is a sample TEST STRING without numbers 231335 and signs $#@#@%#$?@#&*()<> SmAlL AND biG lettErs!")); // changed one number
        }

        [Test()]
        public void EncryptSameStringTest()
        {
            Assert.AreEqual(Encryption.encrypt(_testString2), Encryption.encrypt("p4ssWordMOCk")); // Same string
            Assert.AreEqual(Encryption.encrypt(_testString1), Encryption.encrypt("this is a sample TEST STRING with numbers 231345 and signs $#@#@%#$!@#&*()<> SmAlL AND biG lettErs!")); // Same string
        }
    }
}