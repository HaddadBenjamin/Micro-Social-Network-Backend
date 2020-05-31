using DiabloII.Application.Resolvers.UserId;
using DiabloII.Application.Tests.Mocks;
using DiabloII.Application.Tests.Services.Http;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Steps
{
    [Binding]
    public class CommonSteps
    {
        private readonly IHttpService _httpService;
        private readonly IServiceCollection _services;

        public CommonSteps(IHttpService httpService, IServiceCollection services)
        {
            _httpService = httpService;
            _services = services;
        }

        [Given(@"I am ""(.*)""")]
        public void WhenIAm(string userId) =>
            _services.AddSingleton<IUserIdResolver>(new UserIdResolverMock(userId));


        [Then(@"the http status code should be (.*)")]
        public void ThenTheHttpStatusCodeShouldBe(int statusCode) => _httpService.StatusCode.ShouldBe(statusCode);
    }
}