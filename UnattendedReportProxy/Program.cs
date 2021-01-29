using ConsoleCommand;
using System;

namespace UnattendedReportProxy
{
    class Program
    {
        static void Main(string[] args)
        {
            Time setTime;
            if (args.Length == 0)
            {
                Console.WriteLine("请输入学号: ");
                string username = Console.ReadLine();
                Console.WriteLine("请输入密码: ");
                string password = Console.ReadLine();
                Console.WriteLine("请输入想要签到的时间，24小时制。例如早上7点25分则为0725: ");
                string time;
                while ((time = Console.ReadLine()).Length != 4 || !Time.isAvailable(time, out setTime))
                    Console.WriteLine("时间输入有误，请重新输入:");
                long group;
                Console.WriteLine("请注意，使用该功能时请保证图片发送组件正在运行且运行在本机.");
                Console.WriteLine("请输入需要发送图片的群号。若不需要自动发送图片请输入0:");
                group = Convert.ToInt64(Console.ReadLine());

                Proxy.instance.cmd = new HRBEU_HIUnattendedReport.Cmd()
                {
                    username = username,
                    password = password,
                    group = group
                };
            }
            else
            {
                try
                {
                    Cmd cmd = Command.DeserializeObject<Cmd>(args);
                    if (!Time.isAvailable(cmd.time, out setTime))
                    {
                        Console.WriteLine("时间格式错误。");
                        return;
                    }
                    if (cmd.help)
                    {
                        Command.PrintHelpInfo<Cmd>();
                        return;
                    }
                    if (cmd.debug)
                    {
                        Console.WriteLine("当前参数: ");
                        Console.WriteLine($"  username: {cmd.username}");
                        Console.WriteLine($"  password: {cmd.password}");
                        Console.WriteLine($"  time: {cmd.time}");
                        Console.WriteLine($"  group: {cmd.group}");
                        Console.WriteLine($"  port: {cmd.port}");
                        Console.WriteLine($"  disableScreenShot: {cmd.disableScreenShot}");
                    }
                    Proxy.instance.cmd = cmd;
                }
                catch (IllegalArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
                catch (MissNecessaryParameterException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }

            Proxy.instance.setTime(setTime).Run();
        }
    }

    class Cmd : HRBEU_HIUnattendedReport.Cmd
    {
        [Alias("t")]
        [Necessary()]
        [Description("设置签到时间")]
        public string time { get; set; }
        [Description("显示调试信息，例如传入的参数等")]
        public bool debug { get; set; } = false;
    }
}
