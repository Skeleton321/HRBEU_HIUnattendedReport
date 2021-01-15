using System;

namespace UnattendedReportProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入学号: ");
            string username = Console.ReadLine();
            Console.WriteLine("请输入密码: ");
            string password = Console.ReadLine();
            Console.WriteLine("请输入想要签到的时间，24小时制。例如早上7点25分则为0725: ");
            string time;
            Time setTime = new Time();
            while ((time = Console.ReadLine()).Length != 4 || !Time.isAvailable(time, out setTime))
                Console.WriteLine("时间输入有误，请重新输入:");
            long group;
            Console.WriteLine("请注意，使用该功能时请保证图片发送组件正在运行且运行在本机.");
            Console.WriteLine("请输入需要发送图片的群号。若不需要自动发送图片请输入0:");
            group = Convert.ToInt64(Console.ReadLine());

            Proxy.instance.user = new string[] { username, password };
            UnattendedReport_PicUpload_Client.Upload.group = group;

            Proxy.instance.setTime(setTime).Run();
        }
    }
}
