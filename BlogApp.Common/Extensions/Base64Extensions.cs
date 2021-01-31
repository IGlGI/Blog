using System;
using System.IO;
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

        public static string Base64EncodeFile(string path)
        {
            try
            {
                var bytes = File.ReadAllBytes(path);
                return Convert.ToBase64String(bytes);
            }
            catch
            {
                return null;
            }
        }
    }
}
