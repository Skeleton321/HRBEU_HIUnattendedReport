using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRBEU_HIUnattendedReport
{
    static class StringBuilderEx
    {
        public static int IndexOf(this StringBuilder stringBuilder, string value)
        {
            return stringBuilder.ToString().IndexOf(value);
        }

        public static int IndexOf(this StringBuilder stringBuilder, string value, int startIndex)
        {
            return stringBuilder.ToString().IndexOf(value, startIndex);
        }
    }
}
