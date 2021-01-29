using System;
using System.Threading;

namespace UnattendedReportProxy
{
    class Proxy
    {
        private Time time;
        private bool timeBlock;

        public static readonly Proxy instance = new Proxy();

        private Thread daemon = null;
        private bool Running = true;

        public HRBEU_HIUnattendedReport.Cmd cmd = null;

        private Proxy()
        {
            timeBlock = false;
            cmd = new HRBEU_HIUnattendedReport.Cmd();
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
            while (HRBEU_HIUnattendedReport.Core.Run(instance.cmd) != 0)
            {
                Time now = Time.Now;
                int offset = 60;
                Console.Write("本次签到失败，{0,2:D}s后重试", offset);
                while (offset != 0)
                {
                    while (Time.Now.Second == now.Second) Thread.Sleep(Convert.ToInt32((1000 - now.Millisecond) * 0.9));
                    now = Time.Now;
                    offset--;
                    Console.Write("\r本次签到失败，{0,2:D}s后重试", offset);
                }
                Console.WriteLine();
            }
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
                    Console.Write("{0,4:D}分钟({1,8:D}ms)后开始签到", offset / 1000 / 60, offset);
                    while (offset != 0)
                    {
                        while (Time.Now.Second == now.Second) Thread.Sleep(Convert.ToInt32((1000 - now.Millisecond) * 0.9));
                        now = Time.Now;
                        offset = time - now;
                        Console.Write("\r{0,4:D}分钟({1,8:D}ms)后开始签到", offset / 1000 / 60, offset);
                    }
                    Console.WriteLine();
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
