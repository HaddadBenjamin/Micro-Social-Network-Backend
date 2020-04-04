using System;

namespace DiabloII.Items.Api.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string resourceName) : base($"{resourceName} not found") { }
    }
}