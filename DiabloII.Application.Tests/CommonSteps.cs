using DiabloII.Application.Tests.Startup;
using Shouldly;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests
{
    [Binding]
    public class CommonSteps
    {
        [Then(@"the http status code should be (.*)")]
        public void ThenTheHttpStatusCodeShouldBe(int statusCode) =>
            MyTestContext.HttpClient.StatusCode.ShouldBe(statusCode);
    }
}