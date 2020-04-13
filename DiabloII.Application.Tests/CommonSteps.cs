using DiabloII.Application.Tests.Startup;
using Shouldly;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests
{
    [Binding]
    public class CommonSteps
    {
        private readonly MyHttpClient _httpClient;
        private static MyTestContext TestContext;

        public CommonSteps(MyTestContext testContext)
        {
            _httpClient = testContext.HttpClient;
        }

        [Then(@"the http status code should be (.*)")]
        public void ThenTheHttpStatusCodeShouldBe(int statusCode) =>
            _httpClient.StatusCode.ShouldBe(statusCode);
    }
}