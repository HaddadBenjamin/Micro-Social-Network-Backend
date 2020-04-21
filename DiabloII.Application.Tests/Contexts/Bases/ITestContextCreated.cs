namespace DiabloII.Application.Tests.Contexts.Bases
{
    public interface ITestContextCreated<RestResource>
    {
        RestResource CreatedResource { get; set; }
    }
}