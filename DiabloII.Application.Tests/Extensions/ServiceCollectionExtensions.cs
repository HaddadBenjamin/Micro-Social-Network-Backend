using System.Linq;
using System.Reflection;
using DiabloII.Application.Services.IpResolver;
using DiabloII.Application.Tests.Mocks;
using DiabloII.Application.Tests.Startup;
using DiabloII.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiabloII.Application.Tests.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterTestDbContDbContextDependency(this IServiceCollection services, string databaseName = "Application.Tests") => services
            .AddDbContextPool<ApplicationDbContext>(optionsBuilder => optionsBuilder.UseInMemoryDatabase(databaseName));

        public static IServiceCollection RegisterTheTestApplicationDependencies(this IServiceCollection services)
        {
            var assemblyTypes = new[] { TestStartup.ApplicationType, TestStartup.InfrastructureType, TestStartup.DomainType, TestStartup.ApplicationType };
            var assemblies = assemblyTypes.Select(Assembly.GetAssembly);

            return services.Scan(scan =>
            {
                scan.FromAssemblies(assemblies)
                    .AddClasses()
                    .AsMatchingInterface()
                    .AsSelf()
                    .WithScopedLifetime();
            });
        }

        public static IServiceCollection RegisterTheApplicationMocks(this IServiceCollection services) => services
            .AddSingleton<IIpV4Resolver, IpV4ResolverMock>();
    }
}