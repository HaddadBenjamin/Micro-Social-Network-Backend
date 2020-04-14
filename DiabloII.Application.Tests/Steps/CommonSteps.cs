using DiabloII.Application.Tests.Startup;
using Shouldly;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps
{
    [Binding]
    public class CommonSteps
    {
        private readonly HttpContext _httpContext;
       
        public CommonSteps(TestContext testContext) => _httpContext = testContext.HttpContext;

        [Then(@"the http status code should be (.*)")]
        public void ThenTheHttpStatusCodeShouldBe(int statusCode) => _httpContext.StatusCode.ShouldBe(statusCode);
    }
}