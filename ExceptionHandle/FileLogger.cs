using System.IO;

using static ExceptionHandle.Misc;
using static ExceptionHandle.Logger;

namespace ExceptionHandle
{
    class FileLogger
    {
        private readonly StreamWriter sw;
        public static FileLogger Instance { get; } = new FileLogger();

        private FileLogger()
        {
            string path = $".\\{GetFormatTime("_")}.log";
            sw = new StreamWriter(path);

        }

        public void Log(Level level, string msg)
        {
            string name = GetCallerMethodName(1);
            sw.WriteLine(Format(level, GetFormatTime(), ref name, ref msg));
        }

        public void Log(string log)
        {
            sw.WriteLine(log);
            sw.Flush();
        }
        public static void Close()
        {
            Instance.sw.Flush();
            Instance.sw.Close();
        }
    }
}
