using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using DiabloII.Application.Extensions;
using DiabloII.Application.Tests.Apis.Domains.ErrorLogs;
using DiabloII.Application.Tests.Apis.Domains.Items;
using DiabloII.Application.Tests.Apis.Domains.Notifications;
using DiabloII.Application.Tests.Apis.Domains.Suggestions;
using DiabloII.Application.Tests.Apis.Domains.Users;
using DiabloII.Application.Tests.Contexts.Domains.Notifications;
using DiabloII.Application.Tests.Contexts.Domains.Suggestions;
using DiabloII.Application.Tests.Contexts.Domains.Users;
using DiabloII.Application.Tests.Repositories.Suggestions;
using DiabloII.Application.Tests.Services.Http;
using DiabloII.Application.Tests.Startup;
using SpecFlow.Autofac;

namespace DiabloII.Application.Tests
{
    public static class TestDependencies
    {
        [ScenarioDependencies]
        public static ContainerBuilder CreateContainerBuilder()
        {
            var testContext = new TestContext();
            var builder = new ContainerBuilder();

            builder.RegisterAllImplementedInterfaceAndSelfFromAssemblies(type =>
            {
                var typeName = type.Name;

                return typeName.EndsWith("Api") ||
                       typeName.EndsWith("Context") ||
                       typeName.EndsWith("Repository") ||
                       typeName.EndsWith("Steps");
            },
            TestStartup.ApplicationTestsType);

            builder.RegisterInstance(testContext.DbContext);
            builder.RegisterInstance(testContext.HttpService).As<IHttpService>();

            //HttpResponseMessage response = await testContext.HttpClient.GetAsync("/suggestions");

            //// Fail the test if non-success result
            //response.EnsureSuccessStatusCode();
            return builder;
        }
    }
}