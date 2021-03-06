﻿using DiabloII.Application.Resolvers.Implementations.Ip;
using DiabloII.Application.Tests.Mocks;
using DiabloII.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiabloII.Application.Tests.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterTestDbContDbContextDependency(this IServiceCollection services, string databaseName = "Application.Tests") => services
            .AddDbContextPool<ApplicationDbContext>(optionsBuilder => optionsBuilder.UseInMemoryDatabase(databaseName));

        public static IServiceCollection RegisterTheApplicationMocks(this IServiceCollection services) => services
            .AddSingleton<IRequestIpV4Resolver, RequestIpV4ResolverMock>();
    }
}