using System.Text.RegularExpressions;
using Autofac;
using DiabloII.Application.Extensions;
using DiabloII.Application.Tests.Services.Http;
using DiabloII.Application.Tests.Startup;
using DiabloII.Domain.Repositories;
using DiabloII.Infrastructure.Repositories;
using SpecFlow.Autofac;

namespace DiabloII.Application.Tests
{
    public static class TestDependencies
    {
        /// TODO :
        /// - J'utilise dans mes tests 2 startup : Startup & TestStartup.
        /// - Je n'arrive pas à mocker les dépendances de Startup.
        [ScenarioDependencies]
        public static ContainerBuilder CreateContainerBuilder()
        {
            var testContext = new TestContext();
            var builder = new ContainerBuilder();
            var typeNameRegex = new Regex(@"(Api|Resolver|Context|Steps|Repository)$");

            builder.RegisterAllImplementedInterfaceAndSelfFromAssemblies(type =>
            {
                var typeName = type.Name;
                var mustRegisterType = typeNameRegex.IsMatch(typeName);

                return mustRegisterType;
            },
            TestStartup.ApplicationTestsType);

            builder.RegisterInstance(builder);
            builder.RegisterInstance(testContext.DbContext);
            builder.RegisterInstance(testContext.HttpService).As<IHttpService>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();

            return builder;
        }
    }
}