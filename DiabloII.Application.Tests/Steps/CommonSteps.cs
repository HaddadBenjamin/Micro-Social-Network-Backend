using DiabloII.Application.Tests.Startup;
using Shouldly;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps
{
    [Binding]
    public class CommonSteps
    {
        private readonly IHttpContext _httpContext;
       
        public CommonSteps(IHttpContext httpContext) => _httpContext = httpContext;

        [Then(@"the http status code should be (.*)")]
        public void ThenTheHttpStatusCodeShouldBe(int statusCode) => _httpContext.StatusCode.ShouldBe(statusCode);
    }
}