using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static ExceptionHandle.Misc;

namespace ExceptionHandle
{
    public class Logger
    {
        private Logger()
        {

        }

        public static Logger GetLogger()
        {
            return new Logger();
        }

        public void Log(Level level, string msg, int dph = 2)
        {
            string time = GetFormatTime();
            string methodName = GetCallerMethodName(dph);
            string log = Format(level, time, ref methodName, ref msg);
            #if DEBUG
            Console.WriteLine(log);
            #endif
            FileLogger.Instance.Log(log);
        }

        public void Debug(string msg)
        {
            Log(Level.DEBUG, msg, 3);
        }
        
        public void Info(string msg)
        {
            Log(Level.INFO, msg, 3);
        }
        
        public void Warn(string msg)
        {
            Log(Level.WARN, msg, 3);
        }

        public void Error(string msg)
        {
            Log(Level.ERROR, msg, 3);
        }
        
        public static string Format(Level level, string time, ref string methodName, ref string message)
        {
            return $"{time} - [{level}] - [{methodName}] : {message}";
        }

        public static void Close()
        {
            FileLogger.Close();
        }
    }

    public enum Level
    {
        DEBUG, INFO, WARN, ERROR
    }
}
