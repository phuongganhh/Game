using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Hash
    {
        public static string Decode(this string s)
        {
            try
            {
                byte[] data = Convert.FromBase64String(s);
                return Encoding.UTF8.GetString(data);
            }
            catch (Exception)
            {
                return s;
            }
            
        }
        public static string sha256(this string randomString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
