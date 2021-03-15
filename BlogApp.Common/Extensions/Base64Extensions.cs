using System;
using System.Text;

namespace BlogApp.Common.Extensions
{
    public static class Base64Extensions
    {
        public static string Base64Decode(this string str64Encoded)
        {
            return Encoding.ASCII.GetString(Convert.FromBase64String(str64Encoded));
        }

        public static string Base64Encode(this string plainText)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(plainText));
        }
    }
}
