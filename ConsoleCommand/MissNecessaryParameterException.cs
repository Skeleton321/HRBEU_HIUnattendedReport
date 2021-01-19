using System;

namespace ConsoleCommand
{
    public class MissNecessaryParameterException : Exception
    {
        public MissNecessaryParameterException(string msg) : base(msg) { }
    }
}
