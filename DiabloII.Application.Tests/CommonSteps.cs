using Shouldly;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests
{
    [Binding]
    [Scope(Tag = "common")]
    public class CommonSteps
    {
        [Then(@"the http status code should be (.*)")]
        public void ThenTheHttpStatusCodeShouldBe(int statusCode) =>
            TestContext.Instance.HttpClient.StatusCode.ShouldBe(statusCode);
    }
}