using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

using UnattendedReport_PicUpload_Client;

namespace HRBEU_HIUnattendedReport
{
    class Misc
    {
        public static readonly string BOUNDFIELDS = "fieldCXXXdqszdjtx,fieldCXXXjtgjbc,fieldGLJL,fieldMQJCRxh,fieldCXXXsftjhb,fieldSTQKqt,fieldSTQKglsjrq,fieldGLFS,fieldYQJLjrsfczbldqzt,fieldCXXXjtfsqtms,fieldCXXXjtfsfj,fieldFHJH,fieldJBXXjjlxrdh,fieldJBXXxm,fieldZXZT,fieldCXXXsftjhbq2,fieldSTQKfrtw,fieldMQJCRxm,fieldCXXXsftjhbq,fieldSTQKqtms,fieldCXXXjtfslc,fieldJBXXlxfs,fieldJBXXxb,fieldCXXXjtfspc,fieldYQJLsfjcqtbl,fieldHGCZDM,fieldCXXXssh,fieldLHTJSX,fieldCXXXfxcfdhsj,fieldZAsheng,fieldJBXXgh,fieldCNS,fieldYC,fieldSTQKfl,fieldCXXXsftjwh,fieldCXXXfxxq,fieldSTQKdqstzk,fieldSTQKhxkn,fieldSTQKqtqksm,fieldLHFrom,fieldHelp,fieldFLid,fieldYQJLjrsfczbl,fieldGLSJTo,fieldJBXXjjlxr,fieldCXXXfxcfsj,fieldMQJCRcjdd,fieldSQSJ,fieldZAjtwz,fieldSTQKfrsjrq,fieldSTQKks,fieldJBXXcsny,fieldCXXXdqszdshengtx,fieldSTQKgm,fieldCXXXjtzzq,fieldLHJH,fieldCXXXdqszd,fieldCXXXjtzzs,fieldSTQKfx,fieldSTQKfs,fieldCXXXjtfsdb,fieldCXXXcxzt,fieldCXXXdqszdqtx,fieldCXXXdqszdstx,fieldCXXXjtfshc,fieldCXXXjtjtzz,fieldCXXXsftjhbs,fieldCXXXsftjhbs2,fieldSTQKsfstbs,fieldCXXXsftjhbs1,fieldCXXXcqwdq,fieldGLSJFrom,fieldCXXXjtfszj,fieldSFLB,fieldZAqu,fieldZAZT,fieldCXXXjtzz,fieldLHTo,fieldCXXXjtfsqt,fieldSTQKfrsjsf,fieldZAshi,fieldHGCSULY,fieldSTQKglsjsf,fieldJBXXdw,fieldCFDD,fieldCXXXsftjhbjtdz,fieldMQJCRlxfs";

        public static void GetScreenShot(string url, System.Net.Cookie JESSIONID)
        {
            var cdSvc = ChromeDriverService.CreateDefaultService();
            cdSvc.HideCommandPromptWindow = true;
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-gpu");
            options.BinaryLocation = Environment.CurrentDirectory + @"\chrome\chrome.exe";
            IWebDriver driver = new ChromeDriver(cdSvc, options);
            driver.Manage().Window.Size = new System.Drawing.Size(Client.WIDTH, Client.WIDTH / 9 * 16);
            driver.Url = "http://jkgc.hrbeu.edu.cn";
            driver.Manage().Cookies.AddCookie(
                new OpenQA.Selenium.Cookie(JESSIONID.Name, JESSIONID.Value, JESSIONID.Domain, JESSIONID.Path, null));
            driver.Navigate().GoToUrl(url);
            string img_url = Environment.CurrentDirectory + @"\screenshot.jpg";
            WebDriverWait explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement res = null;
            int count = 0;
            while (res == null && count < 10)
            {
                res = explicitWait.Until(d =>
                {
                    try
                    {
                        return d.FindElement(By.ClassName("remark_display_time"));
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                });
                count++;
            }
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            System.Threading.Thread.Sleep(500);
            res.Click();

            Screenshot screenShotFile = ((ITakesScreenshot)driver).GetScreenshot();

            screenShotFile.SaveAsFile(img_url, ScreenshotImageFormat.Jpeg);
            Console.WriteLine("截图已保存至本地。");
            Upload.img = img_url;
            driver.Close();
            driver.Quit();
        }

        public static PostData GetPostFormData(string json, string url)
        {
            dynamic j = JsonConvert.DeserializeObject<dynamic>(json);
            string tmp = JsonConvert.SerializeObject(j.entities[0].data);
            FormData form = JsonConvert.DeserializeObject<FormData>(tmp);
            form._VAR_URL = url;
            form.fieldCNS = true;

            form.fieldCXXXjtzzs_Attr = new Parent(form.fieldCXXXjtzz).toString();
            form.fieldCXXXjtzzq_Attr = new Parent(form.fieldCXXXjtzzs).toString();
            form.fieldZAshi_Attr = new Parent(form.fieldZAsheng).toString();
            form.fieldCXXXdqszdstx_Attr = new Parent(form.fieldCXXXdqszdshengtx).toString();
            form.fieldCXXXdqszdqtx_Attr = new Parent(form.fieldCXXXdqszdstx).toString();
            form.fieldCXXXsftjhbs2_Attr = new Parent(form.fieldCXXXsftjhbs1).toString();
            form.fieldCXXXsftjhbq2_Attr = new Parent(form.fieldCXXXsftjhbs2).toString();

            form._VAR_ENTRY_NAME = j.entities[0].step.instanceName;
            form._VAR_ENTRY_TAGS = j.entities[0].step.instanceTags;

            if (form.groupMQJCRList == null)
            {
                form.groupMQJCRList = new List<int>();
                form.groupMQJCRList.Add(0);
            }

            PostData post = new PostData();

            foreach (var i in form.GetType().GetProperties())
            {
                if (i.Name != "fieldMQJCRxh")
                {
                    var _tmp = post.GetType().GetProperty(i.Name);
                    _tmp.SetValue(post, i.GetValue(form));
                }

            }

            post.fieldMQJCRxh = new List<int>();
            post.fieldMQJCRxh.Clear();
            foreach (var i in form.fieldMQJCRxh)
                post.fieldMQJCRxh.Add(Convert.ToInt32(i));

            return post;
        }

        public static FormUrlEncodedContent GetListNextStepsUsersContent(string stepId, FormData postData, string csrfToken)
        {
            return new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("stepId", stepId),
                new KeyValuePair<string, string>("actionId", "1"),
                new KeyValuePair<string, string>("formData", JsonConvert.SerializeObject(postData)),
                new KeyValuePair<string, string>("timestamp", Timestamp().ToString()),
                new KeyValuePair<string, string>("rand", Rand()),
                new KeyValuePair<string, string>("boundFields", BOUNDFIELDS),
                new KeyValuePair<string, string>("csrfToken", csrfToken),
                new KeyValuePair<string, string>("lang", "zh")
            });
        }

        public static FormUrlEncodedContent GetDoActionContent(string stepId, FormData postData, string csrfToken)
        {
            return new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("actionId", "1"),
                new KeyValuePair<string, string>("formData", JsonConvert.SerializeObject(postData)),
                new KeyValuePair<string, string>("remark", ""),
                new KeyValuePair<string, string>("rand", Rand(700)),
                new KeyValuePair<string, string>("nextUsers", "{}"),
                new KeyValuePair<string, string>("stepId", stepId),
                new KeyValuePair<string, string>("timestamp", Timestamp().ToString()),
                new KeyValuePair<string, string>("boundFields", BOUNDFIELDS),
                new KeyValuePair<string, string>("csrfToken", csrfToken),
                new KeyValuePair<string, string>("lang", "zh")
            });
        }

        public static FormUrlEncodedContent GetLoginContent(string lt, string execution, User user)
        {
            return new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", user.username),
                new KeyValuePair<string, string>("password", user.password),
                new KeyValuePair<string, string>("captcha", ""),
                new KeyValuePair<string, string>("lt", lt),
                new KeyValuePair<string, string>("execution", execution),
                new KeyValuePair<string, string>("_eventId", "submit"),
                new KeyValuePair<string, string>("submit", "登+录")
            });
        }

        public static string GetRequestUri(HttpResponseMessage response)
        {
            return response.RequestMessage.RequestUri.ToString();
        }

        public static Dictionary<string, string> GetArgs(StreamReader stream, IEnumerable<BinKVGroup> regexGroup, bool isInForm = false)
        {
            StringBuilder html = new StringBuilder();
            string line;
            while ((line = stream.ReadLine()) != null)
            {
                line = line.Trim();
                if (string.IsNullOrEmpty(line)) continue;
                if (line.ElementAt(0) != '<')
                    html.Append(' ');
                html.Append(line);
            }

            Core.logger.Debug("------------------------原始网页------------------------");
            Core.logger.Debug(html.ToString());
            Core.logger.Debug("--------------------------------------------------------");

            if (isInForm)
            {
                html = html.Remove(0, html.IndexOf("<form"));
                int pos = html.IndexOf("</form>") + 7;
                html = html.Remove(pos, html.Length - pos);
            }

            Dictionary<string, string> pairs = new Dictionary<string, string>();

            Core.logger.Debug("------------------------正则表达式------------------------");
            foreach (BinKVGroup item in regexGroup)
            {
                string reg;
                if (item.k2 == null)
                    reg = item.v1;
                else
                    reg = $"{item.k1}=\"{item.v1}\".*?{item.k2}=\"(.*?)\"";
                var ans = Regex.Match(html.ToString(), reg);
                if (item.k2 == null)
                    pairs[item.k1] = ans.Groups[1].Value;
                else
                    pairs[item.v1] = ans.Groups[1].Value;
                Core.logger.Debug($"  regex={reg}, value={ans.Groups[1].Value}");
            }

            Core.logger.Debug("----------------------------------------------------------");
            return pairs;
        }

        public static StreamReader GetStreamContent(HttpResponseMessage response)
        {
            var res = response.Content.ReadAsStreamAsync();
            res.Wait();
            return new StreamReader(res.Result);
        }

        public static string GetStringContent(HttpResponseMessage response)
        {
            var res = response.Content.ReadAsStringAsync();
            res.Wait();
            return res.Result;
        }

        public static string Rand(int size = 1000)
        {
            double d = new Random().NextDouble() * size;
            return d.ToString();
        }

        public static long Timestamp()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }
    }
}
