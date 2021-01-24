using System;

namespace DiabloII.Application.Tests.Contexts.Bases
{
    public interface ITestContextUpdated : ITestContextUpdated<Guid>
    {
    }

    public interface ITestContextUpdated<RestResourceId>
    {
        RestResourceId UpdatedResourceId { get; set; }
    }
}