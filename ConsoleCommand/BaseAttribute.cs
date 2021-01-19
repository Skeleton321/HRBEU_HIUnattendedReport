using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCommand
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BaseAttribute : Attribute
    {
        private readonly string _val;
        public BaseAttribute(string value)
        {
            _val = value;
        }

        public string Val
        {
            get
            {
                return _val;
            }
        }
    }
}
