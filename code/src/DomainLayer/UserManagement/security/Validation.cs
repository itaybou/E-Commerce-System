using ECommerceSystem.DomainLayer.Utilities;
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ECommerceSystem.DomainLayer.UserManagement.security
{
    public static class Validation
    {
        public static Range<int> PSWD_RANGE = new Range<int>(6, 15);

        public static bool IsValidEmail(string email, out string error)
        {
            error = null;
            try
            { 
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                error = "Email address is not valid";
                return false;
            }
        }

        public static bool isValidPassword(string pswd, out string error)
        {
            if (string.IsNullOrWhiteSpace(pswd) || !PSWD_RANGE.inRange(pswd.Length))
            {
                error = $"Password should be between {PSWD_RANGE.min} to {PSWD_RANGE.max} characters.";
                return false;
            }
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");

            if (!hasLowerChar.IsMatch(pswd))
            {
                error = "Password should contain at least one lower case letter.";
                return false;
            }
            else if (!hasUpperChar.IsMatch(pswd))
            {
                error = "Password should contain at least one upper case letter.";
                return false;
            }
            else if (!hasNumber.IsMatch(pswd))
            {
                error = "Password should contain at least one numeric value.";
                return false;
            }

            error = null;
            return true;
        }
    }
}