﻿using System;
using System.Linq;
using System.Reflection;
using DiabloII.Application.Filters.ErrorHandling;
using DiabloII.Domain.Repositories;
using DiabloII.Infrastructure.DbContext;
using DiabloII.Infrastructure.Helpers;
using DiabloII.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace DiabloII.Application.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddMySwagger(this IServiceCollection services) => services
            .AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Diablo II - items and suggestions API",
                    Description = "Allow to search Diablo II items and crud suggestions and votes",
                    Contact = new OpenApiContact
                    {
                        Name = "Un passionné dans la foule (alias Firefouks)",
                        Url = new Uri("https://github.com/HaddadBenjamin")
                    }
                });
                swagger.DescribeAllEnumsAsStrings();
            });

        public static IServiceCollection AddMyMvc(this IServiceCollection services)
        {
            services
                .AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;
                    options.Filters.Add(new ErrorHandlingFilter());
                })
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .AddFluentValidation();

            return services;
        }

        public static void RegisterMyDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationType = typeof(Startup);
            var infrastructureType = typeof(ErrorLogRepository);
            var domainType = typeof(IErrorLogRepository);
            var assemblyTypes = new [] { applicationType, infrastructureType, domainType };
            var assemblies = assemblyTypes.Select(assemblyType => Assembly.GetAssembly(assemblyType));
            var connectionString = DatabaseHelpers.GetMyConnectionString(configuration);

            services
                .AddDbContextPool<ApplicationDbContext>(optionsBuilder =>
                    optionsBuilder.UseSqlServer(connectionString,
                        sqlServerOptions =>
                        {
                            sqlServerOptions.MigrationsAssembly("DiabloII.Application");
                            sqlServerOptions.EnableRetryOnFailure();
                        }))
                .Scan(scan =>
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