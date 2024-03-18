using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Infrastructure
{
    public static class Extensions
    {

        public static bool IsNumeric(this string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsValidEmail(this string s)
        {
            var trimmedEmail = s.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(s);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidDateTime(this string s)
        {
            try
            {
                Convert.ToDateTime(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static IEnumerable<Exception> GetInnerExceptions(this Exception ex)
        {
            if (ex == null)
            {
                throw new ArgumentNullException("ex");
            }

            var innerException = ex;
            do
            {
                yield return innerException;
                innerException = innerException.InnerException;
            }
            while (innerException != null);
        }

        public static string ToMD5Hash(this string input)
        {            
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
