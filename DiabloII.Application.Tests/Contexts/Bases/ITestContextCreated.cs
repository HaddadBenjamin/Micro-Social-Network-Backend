using System;

namespace DiabloII.Application.Tests.Contexts.Bases
{
    public interface ITestContextCreated : ITestContextCreated<Guid>
    {
    }

    public interface ITestContextCreated<RestResourceId>
    {
        RestResourceId CreatedResourceId { get; set; }
    }
}