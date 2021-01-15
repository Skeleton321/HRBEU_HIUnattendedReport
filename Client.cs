using System;
using System.Net;
using System.Net.Http;

namespace HRBEU_HIUnattendedReport
{
    public class Client
    {
        public static readonly string USERAGENT = "Mozilla/5.0 (Linux; Android 4.2.1; AMOI N828 Build/JOP40D) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.59 Mobile Safari/537.36";
        public static readonly string JKGC = "http://jkgc.hrbeu.edu.cn/infoplus/form/JSXNYQSBtest/start";
        public static readonly int WIDTH = 600;

        public CookieContainer cookies = new CookieContainer();
        private HttpClient _client;
        
        public Client()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookies;
            var c = new System.Net.Cookie("MESSAGE_TICKET", "%7B%22times%22%3A0%7D");
            handler.CookieContainer.Add(new Uri("https://cas.hrbeu.edu.cn"), c);
            _client = new HttpClient(handler);
            _client.Timeout = new TimeSpan(0, 0, 0, 3);
            _client.DefaultRequestHeaders.Add("User-Agent", USERAGENT);
            _client.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.8,zh-TW;q=0.7,zh-HK;q=0.5,en-US;q=0.3,en;q=0.2");
            _client.DefaultRequestHeaders.Add("Connection", "keep-alive");
        }

        public HttpResponseMessage Post(string url, FormUrlEncodedContent content, string referer = null)
        {
            Core.logger.Debug("post:");
            Core.logger.Debug($"  url={url}");
            Core.logger.Debug($"  content={content}");
            if(string.IsNullOrWhiteSpace(referer))
                Core.logger.Debug($"  referer=null");
            else
                Core.logger.Debug($"  referer={referer}");

            if (referer != null)
                _client.DefaultRequestHeaders.Referrer = new Uri(referer);
            var result = _client.PostAsync(url, content);
            try
            {
                result.Wait();
            }
            catch (Exception e)
            {
                Core.logger.Error(e.ToString());
                Environment.Exit(200);
            }
            return result.Result;
        }

        public HttpResponseMessage Get(string url, string referer = null)
        {
            Core.logger.Debug("post:");
            Core.logger.Debug($"  url={url}");
            if (string.IsNullOrWhiteSpace(referer))
                Core.logger.Debug($"  referer=null");
            else
                Core.logger.Debug($"  referer={referer}");

            if (referer != null)
                _client.DefaultRequestHeaders.Referrer = new Uri(referer);
            var result = _client.GetAsync(url);
            try
            {
                result.Wait();
            }catch(Exception e)
            {
                Core.logger.Error(e.ToString());
                Environment.Exit(201);
            }
            return result.Result;
        }
    }
}
