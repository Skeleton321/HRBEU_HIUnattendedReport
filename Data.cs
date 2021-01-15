using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HRBEU_HIUnattendedReport
{
    class PostData : FormData
    {
        public new List<int> fieldMQJCRxh { get; set; }
    }

    class FormData
    {
        public string _VAR_EXECUTE_INDEP_ORGANIZE_Name { get; set; } = "";

        public string _VAR_ACTION_INDEP_ORGANIZES_Codes { get; set; } = "";

        public string _VAR_ACTION_REALNAME { get; set; } = "";

        public string _VAR_ACTION_ORGANIZE { get; set; } = "";

        public string _VAR_EXECUTE_ORGANIZE { get; set; } = "";

        public string _VAR_ACTION_INDEP_ORGANIZE { get; set; } = "";

        public string _VAR_ACTION_INDEP_ORGANIZE_Name { get; set; } = "";

        public string _VAR_ACTION_ORGANIZE_Name { get; set; } = "";

        public string _VAR_EXECUTE_ORGANIZES_Names { get; set; } = "";

        public string _VAR_OWNER_ORGANIZES_Codes { get; set; } = "";

        public string _VAR_ADDR { get; set; } = "";

        public string _VAR_OWNER_ORGANIZES_Names { get; set; } = "";

        public string _VAR_URL { get; set; } = "";

        public string _VAR_EXECUTE_ORGANIZE_Name { get; set; } = "";

        public string _VAR_RELEASE { get; set; } = "";

        public string _VAR_NOW_MONTH { get; set; } = "";

        public string _VAR_ACTION_USERCODES { get; set; } = "";

        public string _VAR_ACTION_ACCOUNT { get; set; } = "";

        public string _VAR_ACTION_INDEP_ORGANIZES_Names { get; set; } = "";

        public string _VAR_OWNER_ACCOUNT { get; set; } = "";

        public string _VAR_ACTION_ORGANIZES_Names { get; set; } = "";

        public string _VAR_STEP_CODE { get; set; } = "";

        public string _VAR_OWNER_PHONE { get; set; } = "";

        public string _VAR_OWNER_USERCODES { get; set; } = "";

        public string _VAR_EXECUTE_ORGANIZES_Codes { get; set; } = "";

        public string _VAR_NOW_DAY { get; set; } = "";

        public string _VAR_OWNER_REALNAME { get; set; } = "";

        public string _VAR_NOW { get; set; } = "";

        public string _VAR_URL_Attr { get; set; } = "";

        public string _VAR_ENTRY_NUMBER { get; set; } = "";

        public string _VAR_EXECUTE_INDEP_ORGANIZES_Names { get; set; } = "";

        public string _VAR_STEP_NUMBER { get; set; } = "";

        public string _VAR_POSITIONS { get; set; } = "";

        public string _VAR_ACTION_PHONE { get; set; } = "";

        public string _VAR_EXECUTE_INDEP_ORGANIZES_Codes { get; set; } = "";

        public string _VAR_EXECUTE_POSITIONS { get; set; } = "";

        public string _VAR_ACTION_ORGANIZES_Codes { get; set; } = "";

        public string _VAR_EXECUTE_INDEP_ORGANIZE { get; set; } = "";

        public string _VAR_NOW_YEAR { get; set; } = "";
        public List<int> groupMQJCRList { get; set; }
        public long fieldSQSJ { get; set; }

        public string fieldFLid { get; set; } = "";

        public string fieldSFLB { get; set; } = "";

        public string fieldJBXXxm { get; set; } = "";

        public string fieldJBXXxm_Name { get; set; } = "";

        public string fieldJBXXgh { get; set; } = "";

        public string fieldJBXXlxfs { get; set; } = "";

        public string fieldJBXXdw { get; set; } = "";

        public string fieldJBXXdw_Name { get; set; } = "";

        public string fieldJBXXxb { get; set; } = "";

        public string fieldJBXXxb_Name { get; set; } = "";

        public string fieldJBXXcsny { get; set; } = "";

        public string fieldCXXXjtzz { get; set; } = "";

        public string fieldCXXXjtzz_Name { get; set; } = "";

        public string fieldCXXXjtzzs { get; set; } = "";

        public string fieldCXXXjtzzs_Name { get; set; } = "";
        public string fieldCXXXjtzzs_Attr { get; set; } = "";

        public string fieldCXXXjtzzq { get; set; } = "";

        public string fieldCXXXjtzzq_Name { get; set; } = "";
        public string fieldCXXXjtzzq_Attr { get; set; } = "";

        public string fieldCXXXjtjtzz { get; set; } = "";

        public string fieldJBXXjjlxr { get; set; } = "";

        public string fieldJBXXjjlxrdh { get; set; } = "";

        public string fieldSTQKsfstbs { get; set; } = "";
        public bool fieldSTQKks { get; set; }
        public bool fieldSTQKgm { get; set; }
        public bool fieldSTQKfs { get; set; }
        public bool fieldSTQKfl { get; set; }
        public bool fieldSTQKhxkn { get; set; }
        public bool fieldSTQKfx { get; set; }
        public bool fieldSTQKqt { get; set; }

        public string fieldSTQKqtms { get; set; } = "";

        public string fieldSTQKfrtw { get; set; } = "";

        public string fieldGLFS { get; set; } = "";

        public string fieldSTQKqtqksm { get; set; } = "";

        public string fieldSTQKdqstzk { get; set; } = "";

        public string fieldSTQKglsjrq { get; set; } = "";

        public string fieldSTQKglsjsf { get; set; } = "";

        public string fieldSTQKfrsjrq { get; set; } = "";

        public string fieldSTQKfrsjsf { get; set; } = "";

        public string fieldCXXXcxzt { get; set; } = "";

        public string fieldZAZT { get; set; } = "";

        public string fieldZAsheng { get; set; } = "";

        public string fieldZAsheng_Name { get; set; } = "";

        public string fieldZAshi { get; set; } = "";

        public string fieldZAshi_Name { get; set; } = "";
        public string fieldZAshi_Attr { get; set; } = "";

        public string fieldZAqu { get; set; } = "";

        public string fieldZAqu_Name { get; set; } = "";

        public string fieldZAjtwz { get; set; } = "";

        public string fieldZXZT { get; set; } = "";

        public string fieldHelp { get; set; } = "";

        public string fieldCXXXfxxq { get; set; } = "";

        public string fieldHGCSULY { get; set; } = "";

        public string fieldHGCSULY_Name { get; set; } = "";

        public string fieldHGCZDM { get; set; } = "";

        public string fieldCXXXssh { get; set; } = "";

        public string fieldLHJH { get; set; } = "";

        public string fieldLHFrom { get; set; } = "";

        public string fieldLHTo { get; set; } = "";

        public string fieldLHTJSX { get; set; } = "";

        public string fieldCXXXdqszd { get; set; } = "";

        public string fieldCXXXdqszdshengtx { get; set; } = "";

        public string fieldCXXXdqszdshengtx_Name { get; set; } = "";

        public string fieldCXXXdqszdstx { get; set; } = "";

        public string fieldCXXXdqszdstx_Name { get; set; } = "";
        public string fieldCXXXdqszdstx_Attr { get; set; } = "";

        public string fieldCXXXdqszdqtx { get; set; } = "";

        public string fieldCXXXdqszdqtx_Name { get; set; } = "";
        public string fieldCXXXdqszdqtx_Attr { get; set; } = "";

        public string fieldCXXXdqszdjtx { get; set; } = "";

        public string fieldCXXXcqwdq { get; set; } = "";

        public string fieldFHJH { get; set; } = "";

        public string fieldCXXXfxcfsj { get; set; } = "";

        public string fieldCXXXfxcfdhsj { get; set; } = "";

        public string fieldCFDD { get; set; } = "";
        public bool fieldCXXXjtfsfj { get; set; }
        public bool fieldCXXXjtfshc { get; set; }
        public bool fieldCXXXjtfsdb { get; set; }
        public bool fieldCXXXjtfspc { get; set; }
        public bool fieldCXXXjtfslc { get; set; }
        public bool fieldCXXXjtfszj { get; set; }
        public bool fieldCXXXjtfsqt { get; set; }

        public string fieldCXXXjtfsqtms { get; set; } = "";

        public string fieldCXXXjtgjbc { get; set; } = "";

        public string fieldGLJL { get; set; } = "";

        public string fieldGLSJFrom { get; set; } = "";

        public string fieldGLSJTo { get; set; } = "";

        public string fieldYQJLjrsfczbl { get; set; } = "";

        public string fieldYQJLjrsfczbldqzt { get; set; } = "";

        public string fieldCXXXsftjwh { get; set; } = "";

        public string fieldYQJLsfjcqtbl { get; set; } = "";

        public string fieldCXXXsftjhbs1 { get; set; } = "";

        public string fieldCXXXsftjhbs1_Name { get; set; }

        public string fieldCXXXsftjhb { get; set; } = "";

        public string fieldCXXXsftjhbs { get; set; } = "";

        public string fieldCXXXsftjhbs2 { get; set; } = "";

        public string fieldCXXXsftjhbs2_Name { get; set; } = "";
        public string fieldCXXXsftjhbs2_Attr { get; set; } = "";

        public string fieldCXXXsftjhbq { get; set; } = "";
        public string fieldCXXXsftjhbq2 { get; set; } = "";
        public string fieldCXXXsftjhbq2_Name { get; set; } = "";
        public string fieldCXXXsftjhbq2_Attr { get; set; } = "";
        public string fieldCXXXsftjhbjtdz { get; set; } = "";
        public string fieldYC { get; set; } = "";
        public List<double> fieldMQJCRxh { get; set; }
        public List<string> fieldMQJCRxm { get; set; }
        public List<string> fieldMQJCRlxfs { get; set; }
        public List<string> fieldMQJCRcjdd { get; set; }
        public bool fieldCNS { get; set; }

        public string _VAR_ENTRY_NAME { get; set; } = "";

        public string _VAR_ENTRY_TAGS { get; set; } = "";
    }

    class User
    {
        public string username { get; private set; }
        public string password { get; private set; }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public User() { }

        public User ReadFromFile()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                using (StreamReader sr = new StreamReader("user.json"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        sb.Append(line.Trim());
                }
                var tmp = JsonConvert.DeserializeObject<dynamic>(sb.ToString());
                username = tmp.username;
                password = tmp.password;
            }
            catch (Exception)
            {
                return null;
            }
            return this;
        }
    }

    class Parent
    {
        public string _parent { get; set; } = "";

        public Parent() { }
        public Parent(string _parent)
        {
            this._parent = _parent;
        }

        public string toString()
        {
            return $"{{\"_parent\":\"{_parent}\"}}";
        }
    }

    struct BinKVGroup
    {
        public string k1 { get; }
        public string k2 { get; }
        public string v1 { get; }
        public string v2 { get; }
        public BinKVGroup(string k1, string v1, string k2, string v2)
        {
            this.k1 = k1;
            this.v1 = v1;
            this.k2 = k2;
            this.v2 = v2;
        }
        public BinKVGroup(string k1, string v1, string k2)
        {
            this.k1 = k1;
            this.v1 = v1;
            this.k2 = k2;
            v2 = null;
        }
    }

}
