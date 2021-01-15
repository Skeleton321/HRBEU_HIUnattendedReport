using System;
using System.Threading;

namespace UnattendedReportProxy
{
    class Proxy
    {
        private Time time;
        private bool timeBlock;

        public static readonly Proxy instance = new Proxy();
        private static readonly Mutex mutex = new Mutex();

        private Thread daemon = null;
        private bool Running = true;

        public string[] user { get; set; }

        private Proxy()
        {
            timeBlock = false;
            user = new string[0];
        }

        public Proxy setTime(Time dateTime)
        {
            if (!timeBlock)
            {
                time = dateTime;
                timeBlock = true;
            }
            return instance;
        }

        public void Run()
        {
            if (daemon != null) return;
            daemon = new Thread(new ThreadStart(DaemonThread));
            daemon.Start();
            Console.WriteLine("等待中");
        }

        public static void SingleRun()
        {
            int tmp;
            while ((tmp = HRBEU_HIUnattendedReport.Core.Run(instance.user)) > 20 && tmp < 1000)
                Thread.Sleep(1000 * 60);
        }

        public void Abort()
        {
            if (daemon == null) return;
            Running = false;
            daemon.Abort();
        }

        private void DaemonThread()
        {
            try
            {
                while (Running)
                {
                    Time now = Time.Now;
                    long offset = time - now;
                    Console.WriteLine($"{offset / 1000 / 60}分钟({offset}ms)后开始签到");
                    if (offset == 0)
                    {
                        SingleRun();
                        Console.WriteLine("本轮签到完成。");
                        Thread.Sleep(1000 * 60);
                        continue;
                    }

                    Thread.Sleep(Convert.ToInt32(offset * 0.8));
                }
            }
            catch (ThreadAbortException)
            {
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
