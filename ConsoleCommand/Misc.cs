using System;
using System.Reflection;

namespace ConsoleCommand
{
    public class Misc
    {
        public static T CreateInstance<T>()
        {
            try
            {
                return (T)Assembly.Load(typeof(T).Assembly.FullName).CreateInstance(typeof(T).FullName);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message + "===" + e.StackTrace);
                return default;
            }
        }
    }
}
