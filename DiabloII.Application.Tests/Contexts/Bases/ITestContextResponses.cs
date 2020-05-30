using DiabloII.Application.Responses;

namespace DiabloII.Application.Tests.Contexts.Bases
{
    public interface ITestContextResponses<RestResource>
    {
        ApiResponses<RestResource> Resources { get; set; }
    }
}