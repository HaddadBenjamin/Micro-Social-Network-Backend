namespace DiabloII.Application.Tests.Contexts.Bases
{
    public interface ITestContextCreated<RestResource>
    {
        public RestResource CreatedResource { get; set; }
    }
}