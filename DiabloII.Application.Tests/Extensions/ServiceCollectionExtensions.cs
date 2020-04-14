using System.Linq;
using System.Reflection;
using DiabloII.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiabloII.Application.Tests.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterTestDbContDbContextDependency(this IServiceCollection services, string databaseName = "Application.Tests") => services
            .AddDbContextPool<ApplicationDbContext>(optionsBuilder => optionsBuilder.UseInMemoryDatabase(databaseName));

        public static void RegisterTheTestApplicationDependencies(this IServiceCollection services)
        {
            var assemblyTypes = new[]  {TestStartup.ApplicationType, TestStartup.InfrastructureType, TestStartup.DomainType, TestStartup.ApplicationType};
            var assemblies = assemblyTypes.Select(Assembly.GetAssembly);

            services.Scan(scan =>
            {
                scan.FromAssemblies(assemblies)
                    .AddClasses()
                    .AsMatchingInterface()
                    .AsSelf()
                    .WithScopedLifetime();
            });
        }
    }
}