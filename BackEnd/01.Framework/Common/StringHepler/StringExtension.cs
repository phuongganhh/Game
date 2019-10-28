using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.StringHepler
{
    public static class StringExtension
    {
        public static bool IsNull(this string input)
        {
            return string.IsNullOrEmpty(input);
        }
        public static string TrimSpace(this string input)
        {
            return input.Replace(" ", "");
        }
    }
}
