using System;
using System.Globalization;
using System.Reflection;

namespace ExceptionHandle
{
    class Misc
    {
        public static string GetFormatTime(string split = " ")
        {
            return DateTime.Now.ToString($"yyyy-MM-dd{split}HH.mm.ss.fff", DateTimeFormatInfo.InvariantInfo);
        }

        public static string GetCallerMethodName(int dph = 2)
        {
            MethodBase method = new System.Diagnostics.StackTrace().GetFrame(dph).GetMethod();
            return $"{method.ReflectedType.FullName}.{method.Name}";
        }
    }
}
