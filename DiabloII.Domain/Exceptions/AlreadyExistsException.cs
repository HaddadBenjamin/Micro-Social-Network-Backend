using System;

namespace DiabloII.Domain.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string resourceName) : base($"{resourceName} already exists") { }
    }
}