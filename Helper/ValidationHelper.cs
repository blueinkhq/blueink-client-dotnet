using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Blueink.Client.Net.v2.Helper
{
    public static class StringExtensions
    {
        public static string NullIfEmpty(this string s)
        {
            return string.IsNullOrEmpty(s) ? null : s;
        }
        public static string NullIfWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s) ? null : s;
        }
        public static string TruncateString(this string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str)) return str;

            return str.Substring(0, Math.Min(str.Length, maxLength));
        }  
    }

    public static class ValidationHelper
    {

        public static bool IsValidUUID(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            Regex regex = new Regex("^[{]?[0-9a-fA-F]{8}-([0-9a-fA-F]{4}-){3}[0-9a-fA-F]{12}[}]?$");
            return regex.IsMatch(str);
        }
        public static bool IsValidEmail(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            return Regex.IsMatch(email,pattern,RegexOptions.IgnoreCase);
        }

        public static string generate_UUIDkey()
        {
            return String.Format( "{0}",Guid.NewGuid().ToString());
        }

        public static string generate_key(string type, int length = 5)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            string result = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            return String.Format("{0}_{1}",type,result);
        }
    }
}
