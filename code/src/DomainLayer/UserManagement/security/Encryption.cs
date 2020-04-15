﻿using System.Security.Cryptography;
using System.Text;

namespace ECommerceSystem.DomainLayer.UserManagement.security
{
    public static class Encryption
    {
        public static string encrypt(string inputString)
        {
            var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(inputString);
            var hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            var result = new StringBuilder();
            foreach (var t in hash)
                result.Append(t.ToString("X2"));
            return result.ToString();
        }
    }
}