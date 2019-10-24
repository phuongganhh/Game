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
            byte[] data = Convert.FromBase64String(s);
            return Encoding.UTF8.GetString(data);
        }
    }
}
