using System;

namespace DiabloII.Application.Tests.Exceptions
{
    public class NotEqualsException : Exception
    {
        public NotEqualsException(string message) : base(message) { }
    }
}