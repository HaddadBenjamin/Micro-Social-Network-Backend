namespace DiabloII.Application.Tests.Contexts.Bases
{
    public interface ITestContextUpdated<RestResource>
    {
        public RestResource UpdatedResource { get; set; }
    }
}