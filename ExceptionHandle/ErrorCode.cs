using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandle
{
    public class ErrorCode : Exception
    {
        public int Code { get; internal set; }
        public ErrorCode(string name, string process) : base($"{name}\n\t发生于 {process}") { }
    }

    public class HttpOperationFailed : ErrorCode
    {
        public HttpOperationFailed(string process) : base("Http操作失败。", process)
        {
            Code = 1;
        }
    }

    public class CannotConnectServer : ErrorCode
    {
        public CannotConnectServer(string process) : base("无法连接至服务器", process)
        {
            Code = 2;
        }
    }

    public class APIArgumentIllegal : ErrorCode
    {
        public APIArgumentIllegal(string content, string process) : base(content, process)
        {
            Code = 11;
        }
    }
}
