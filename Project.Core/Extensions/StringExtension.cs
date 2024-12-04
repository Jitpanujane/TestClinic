using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Extensions
{
    public static class StringExtension
    {
        public static string ToNullIfWhiteSpace(this string value)
        {
            if (value?.Trim() == "")
            {
                return null;
            }

            return value;
        }
    }
}
