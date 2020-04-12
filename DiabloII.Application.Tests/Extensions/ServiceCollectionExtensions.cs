using DiabloII.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiabloII.Application.Tests.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterTestDbContDbContextDependency(this IServiceCollection services, string databaseName = "Application.Tests") => services
            .AddDbContextPool<ApplicationDbContext>(optionsBuilder => optionsBuilder.UseInMemoryDatabase(databaseName));

    }
}