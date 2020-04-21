using System.Collections.Generic;

namespace DiabloII.Application.Tests.Contexts.Bases
{
    public interface ITestContextAll<RestResource>
    {
        IReadOnlyCollection<RestResource> AllResources { get; set; }
    }
}
