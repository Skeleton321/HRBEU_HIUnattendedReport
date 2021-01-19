using ConsoleCommand;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using UnattendedReport_PicUpload_Client;

namespace HRBEU_HIUnattendedReport
{
    public class Core
    {
        public static FileLogger logger = null;
        public static readonly Client client = new Client();

        public static int Run(string[] args)
        {
            try
            {
                Cmd cmd = Command.DeserializeObject<Cmd>(args);
                if (cmd.help)
                {
                    Command.PrintHelpInfo<Cmd>();
                    return 0;
                }
                return Run(cmd);
            }
            catch (IllegalArgumentException e)
            {
                Console.WriteLine(e.Message);
                return 191;
            }
            catch (MissNecessaryParameterException e)
            {
                Console.WriteLine(e.Message);
                return 192;
            }
        }
        public static int Run(Cmd cmd)
        {
            User user = new User(cmd.username, cmd.password);
            Upload.group = cmd.group;

            logger = new FileLogger();

            Dictionary<string, string> vals;

            var result = client.Get(Client.JKGC);

            if (result.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("服务器连接失败。");
                Console.ReadLine();
                logger.Log(Level.ERROR, "服务器连接失败");
                logger.Info(Misc.GetRequestUri(result));
                logger.Info(Misc.GetStringContent(result));
                logger.Close();
                return 21;
            }

            string uri = Misc.GetRequestUri(result);
            Console.WriteLine("登录中");
            logger.Info($"url={uri}");

            if (uri.Contains("cas.hrbeu.edu.cn/cas/login"))
            {
                using (StreamReader sr = Misc.GetStreamContent(result))
                {
                    vals = Misc.GetArgs(sr, new[] {
                        new BinKVGroup("name", "lt", "value"),
                        new BinKVGroup("name", "execution", "value")
                    }, true);
                }
                result = client.Post(uri, Misc.GetLoginContent(vals["lt"], vals["execution"], user));
            }

            uri = Misc.GetRequestUri(result);
            logger.Info(uri);

            using (StreamReader sr = Misc.GetStreamContent(result))
            {
                logger.Info(Misc.GetRequestUri(result));
                vals = Misc.GetArgs(sr, new[]
                {
                    new BinKVGroup("itemscope", "csrfToken", "content"),
                    new BinKVGroup("id", "idc", "value"),
                    new BinKVGroup("id", "release", "value")
                });
            }
            result = client.Post("http://jkgc.hrbeu.edu.cn/infoplus/interface/start", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("idc", vals["idc"]),
                new KeyValuePair<string, string>("release", vals["release"]),
                new KeyValuePair<string, string>("csrfToken", vals["csrfToken"]),
                new KeyValuePair<string, string>("formData", "{\"_VAR_URL\":\"http://jkgc.hrbeu.edu.cn/infoplus/form/JSXNYQSBtest/start\",\"_VAR_URL_Attr\":\"{}\"}")
            }));

            {
                string html = Misc.GetStringContent(result);
                logger.Info(html);
                var j = JsonConvert.DeserializeObject<dynamic>(html);
                if (j.errno != 0)
                {
                    Console.WriteLine(j.error);
                    Console.ReadLine();
                    logger.Log(Level.ERROR, j.error.ToString());
                    logger.Close();
                    return 100;
                }
                result = client.Get(j.entities[0].Value);
            }
            Console.WriteLine("签到中...");
            logger.Info(Misc.GetRequestUri(result));

            using (StreamReader sr = Misc.GetStreamContent(result))
            {
                logger.Info(Misc.GetRequestUri(result));
                vals = Misc.GetArgs(sr, new[]
                {
                    new BinKVGroup("itemscope", "csrfToken", "content"),
                    new BinKVGroup("stepId", "formStepId = \"?(.*?)\"?;", null),
                    new BinKVGroup("instanceId", "instanceId = \"?(.*?)\"?;", null)
                });
            }
            uri = Misc.GetRequestUri(result);
            result = client.Post("http://jkgc.hrbeu.edu.cn/infoplus/interface/render",
                new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("stepId", vals["stepId"]),
                    new KeyValuePair<string, string>("instanceId", vals["instanceId"]),
                    new KeyValuePair<string, string>("admin", "false"),
                    new KeyValuePair<string, string>("rand", Misc.Rand(200)),
                    new KeyValuePair<string, string>("width", "1920"),
                    new KeyValuePair<string, string>("lang", "zh"),
                    new KeyValuePair<string, string>("csrfToken", vals["csrfToken"])
                }), uri);
            PostData pd;
            using (StreamReader sr = Misc.GetStreamContent(result))
            {
                string line = sr.ReadToEnd();
                pd = Misc.GetPostFormData(line, uri);
                logger.Info(line);
            }
            logger.Info(JsonConvert.SerializeObject(pd));
            result = client.Post("http://jkgc.hrbeu.edu.cn/infoplus/interface/listNextStepsUsers",
                Misc.GetListNextStepsUsersContent(vals["stepId"], pd, vals["csrfToken"]),
                uri);

            logger.Info(Misc.GetStringContent(result));


            result = client.Post("http://jkgc.hrbeu.edu.cn/infoplus/interface/doAction",
                Misc.GetDoActionContent(vals["stepId"], pd, vals["csrfToken"]),
                uri);

            logger.Info(Misc.GetStringContent(result));

            Console.WriteLine("签到完成。");
            int code = 0;
            if (!cmd.disableScreenShot)
            {
                Misc.GetScreenShot(uri,
                    client.cookies.GetCookies(new Uri(uri)).Cast<Cookie>().ToList()[0]);
                if (Upload.group > 0)
                    code = Upload.DoAction();
            }
            logger.Close();
            return code;
        }

        public static int Main(string[] args)
        {
            return Run(args);
        }
    }

    public class Cmd
    {
        [Alias("u")]
        [Necessary()]
        [Description("设置用户名")]
        public string username { get; set; }

        [Alias("p")]
        [Necessary()]
        [Description("设置密码")]
        public string password { get; set; }

        [Alias("g")]
        [Description("设置要发送图片的群号")]
        public long group { get; set; } = -1;

        [Alias("s")]
        [Description("禁用截图功能。该功能禁用时，图片发送功能也被禁用。")]
        public bool disableScreenShot { get; set; } = false;

        [Alias("h")]
        [Description("显示帮助信息")]
        public bool help { get; set; } = false;
    }
}
