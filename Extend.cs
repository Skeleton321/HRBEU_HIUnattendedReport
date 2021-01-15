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
    static class LevelEx
    {
        public static string ToName(this Level level)
        {
            switch (level)
            {
                case Level.DEBUG:
                    return "调试";
                case Level.INFO:
                    return "信息";
                case Level.WARN:
                    return "警告";
                case Level.ERROR:
                    return "错误";
                default:
                    return "未知";
            }
        }
    }
}
