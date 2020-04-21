namespace DiabloII.Application.Tests.Contexts.Bases
{
    public interface ITestContextUpdated<RestResource>
    {
        RestResource UpdatedResource { get; set; }
    }
}