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
            int tmp;
            while ((tmp = HRBEU_HIUnattendedReport.Core.Run(instance.cmd)) > 20 && tmp < 1000)
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
