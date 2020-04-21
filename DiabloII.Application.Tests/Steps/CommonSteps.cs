using DiabloII.Application.Tests.Services.Http;
using Shouldly;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps
{
    [Binding]
    public class CommonSteps
    {
        private readonly IHttpService _httpService;

        public CommonSteps(IHttpService httpService) => _httpService = httpService;

        [Then(@"the http status code should be (.*)")]
        public void ThenTheHttpStatusCodeShouldBe(int statusCode) => _httpService.StatusCode.ShouldBe(statusCode);
    }
}