// <copyright file="EncryptionTest.cs">Copyright ©  2020</copyright>
using System;
using ECommerceSystem.DomainLayer.UserManagement.security;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using NUnit.Framework;

namespace ECommerceSystem.DomainLayer.UserManagement.security.Tests
{
    /// <summary>This class contains parameterized unit tests for Encryption</summary>
    [PexClass(typeof(Encryption))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestFixture]
    public partial class EncryptionTest
    {
        /// <summary>Test stub for encrypt(String)</summary>
        [PexMethod]
        public string encryptTest(string inputString)
        {
            string result = Encryption.encrypt(inputString);
            return result;
            // TODO: add assertions to method EncryptionTest.encryptTest(String)
        }
    }
}
