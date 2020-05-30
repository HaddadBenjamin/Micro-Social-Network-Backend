using DiabloII.Application.Responses;

namespace DiabloII.Application.Tests.Contexts.Bases
{
    public interface ITestContextAll<RestResource>
    {
        ApiResponses<RestResource> Resources { get; set; }
    }
}
