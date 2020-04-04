using System;

namespace DiabloII.Items.Api.Domain.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
