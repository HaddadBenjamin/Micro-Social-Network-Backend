using System.Text.RegularExpressions;
using Autofac;
using DiabloII.Application.Extensions;
using DiabloII.Application.Tests.Services.Http;
using DiabloII.Application.Tests.Startup;
using DiabloII.Domain.Repositories.Domains;
using DiabloII.Infrastructure.Repositories;
using SpecFlow.Autofac;

namespace DiabloII.Application.Tests
{
    public static class TestDependencies
    {
        /// TODO :
        /// - Ce containeur builder est utilisé pour mes tests alors que la méthode RegisterContainer du TestStartup enregistre un autre containeur.
        /// - Je n'arrive pas à mocker les dépendances de Startup.
        [ScenarioDependencies]
        public static ContainerBuilder CreateContainerBuilder()
        {
            // Il faudrait que je n'ai qu'un seul containeur de dépendances mais je ne vois pas du tout comment faire encore.
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