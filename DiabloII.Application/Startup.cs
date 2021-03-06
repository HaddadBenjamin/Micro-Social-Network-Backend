﻿using System;
using Autofac;
using AutoMapper;
using DiabloII.Application.Extensions;
using DiabloII.Domain.Configurations;
using DiabloII.Domain.Repositories.Domains;
using DiabloII.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiabloII.Application
{
    public class Startup
    {
        internal static readonly Type ApplicationType = typeof(Startup);
        internal static readonly Type InfrastructureType = typeof(ErrorLogRepository);
        internal static readonly Type DomainType = typeof(IErrorLogRepository);

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services) => services
            .AddAutoMapper(ApplicationType, DomainType)
            .AddMySwagger()
            .AddMyMvc()
            .AddCors()
            .AddRouting(options => options.LowercaseUrls = true)
            .RegisterTheDbContextDependency(_configuration)
            .AddMySmtpServer(_configuration.GetSection("Smtp").Get<SmtpConfiguration>())
            .AddMediatR(InfrastructureType);

        public void ConfigureContainer(ContainerBuilder builder) =>
            builder.RegisterAllImplementedInterfaceAndSelfFromAssemblies(ApplicationType, InfrastructureType, DomainType);

        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment environment) => applicationBuilder
            .UseMyExceptionPages(environment)
            .PlayAllTheDatabaseMigrations()
            .UseMyCors()
            .UseMvc()
            .UseMySwagger();
    }
}
