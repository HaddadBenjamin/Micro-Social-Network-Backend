using System;
namespace DiabloII.Items.Api.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string argumentName, string reason) : base($"[{argumentName}] {reason}") { }
        
        public BadRequestException(string argumentName) : base($"[{argumentName}] is not valid") { }
    }
}
