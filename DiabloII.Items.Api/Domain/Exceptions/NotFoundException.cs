using System;

namespace DiabloII.Items.Api.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string resourceName) : base($"{resourceName} not found") { }
    }
}