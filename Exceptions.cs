using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRBEU_HIUnattendedReport
{
    class URBaseException : Exception
    {
        public URBaseException(string msg) : base(msg) { }

        public URBaseException(string msg, string pos) : base($"{msg}\n\t发生于 {pos}") { }
    }


}
