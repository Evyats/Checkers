using System;

namespace Ex05
{
    public class CommandNotLegalException : Exception
    {
        public CommandNotLegalException() : base("The requested operation is not legal.")
        {
        }
    }
}