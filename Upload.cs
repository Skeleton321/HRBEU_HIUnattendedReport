﻿using HRBEU_HIUnattendedReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UnattendedReport_PicUpload_Client
{
    public class Upload
    {
        public static string img { get; set; }
        public static long group { get; set; } = -1;

        public static int DoAction()
        {
            var result = Core.client.Post("http://localhost:6291/send_img", new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("group", group.ToString()),
                new KeyValuePair<string, string>("url", $"file:///{img.Replace('\\', '/')}")
            }
                ));

            string json = Misc.GetStringContent(result);
            Report jsonObj = JsonConvert.DeserializeObject<Report>(json);
            Console.WriteLine(jsonObj.content[0]);
            return jsonObj.code;
        }

    }
    
}
