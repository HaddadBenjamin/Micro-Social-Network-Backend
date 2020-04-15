using DiabloII.Application.Tests.Apis.ErrorLogs;
using DiabloII.Application.Tests.Apis.Items;
using DiabloII.Application.Tests.Apis.Suggestions;
using DiabloII.Application.Tests.Contexts.Suggestions;
using DiabloII.Application.Tests.Repositories;
using DiabloII.Application.Tests.Startup;
using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection;

namespace DiabloII.Application.Tests
{
    public static class TestDependencies
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        { 
            var testContext = new TestContext();
            var services = testContext.Services;

            services.AddSingleton(testContext.DbContext);
            services.AddSingleton<IHttpContext>(testContext.HttpContext);
            services.AddSingleton<ISuggestionsTestContext, SuggestionsTestContext>();
            services.AddSingleton<ISuggestionsRepository, SuggestionsRepository>();
            services.AddSingleton<ISuggestionsApi, SuggestionsApi>();
            services.AddSingleton<IItemsApi, ItemsApi>();
            services.AddSingleton<IErrorLogsApi, ErrorLogsApi>();

            return testContext.Services;
        }
    }
}