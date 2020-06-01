namespace DiabloII.Application.Tests.Contexts.Bases
{
    public interface ITestContextGet<RestResource>
    {
        RestResource GetResource { get; set; }
    }
}