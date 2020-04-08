using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ECommerceSystem.DomainLayer.UserManagement.security
{
    internal static class Validation
    {
        public static Range<int> PSWD_RANGE = new Range<int>(6, 15);

        public static bool IsValidEmail(string email)
        {
            try {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch {
                return false;
            }
        }

        public static bool isValidPassword(string pswd, out string error)
        {
            if (string.IsNullOrWhiteSpace(pswd) || !PSWD_RANGE.inRange(pswd.Length))
            {
                throw new Exception($"Password should be between {PSWD_RANGE.min} to {PSWD_RANGE.max} characters");
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

        internal class Range<T> where T : IComparable<T>
        {
            /// <summary>Minimum value of the range.</summary>
            public T min { get; set; }

            /// <summary>Maximum value of the range.</summary>
            public T max { get; set; }

            public Range(T min, T max)
            {
                if (min.CompareTo(max) <= 0)
                {
                    this.min = min;
                    this.max = max;
                } else throw new ArgumentException();
            }

            public bool inRange(T value)
            {
                return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
            }
        }
    }
}
