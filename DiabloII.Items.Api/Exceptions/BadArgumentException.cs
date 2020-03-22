using System;
namespace DiabloII.Items.Api.Exceptions
{
    public class BadArgumentException : Exception
    {
        public BadArgumentException(string argumentName, string reason) : base($"[{argumentName}] {reason}") { }
        
        public BadArgumentException(string argumentName) : base($"[{argumentName}] is not valid") { }
    }
}
