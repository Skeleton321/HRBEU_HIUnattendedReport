using System;
using System.Globalization;
using System.IO;

namespace HRBEU_HIUnattendedReport
{
    public enum Level
    {
        DEBUG, INFO, WARN, ERROR
    }

    public class FileLogger
    {
        StreamWriter sw;
        public FileLogger()
        {
            string path = $".\\{GetFormatTime("_")}.log";
            sw = new StreamWriter(path);
        }

        public static string GetFormatTime(string split = " ")
        {
            return DateTime.Now.ToString($"yyyy-MM-dd{split}HH-mm-ss.fff", DateTimeFormatInfo.InvariantInfo);
        }

        public string Log(Level level, string text)
        {
            string f = $"[{GetFormatTime()}] - [{level.ToName()}] - {text}";
            sw.WriteLine(f);
            sw.Flush();
            return f;
        }

        public string Info(string text)
        {
            return Log(Level.INFO, text);
        }

        public string Debug(string text)
        {
            return Log(Level.DEBUG, text);
        }

        public string Error(string text)
        {
            return Log(Level.ERROR, text);
        }

        public void Close()
        {
            sw.Flush();
            sw.Close();
        }
    }

}
