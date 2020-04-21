﻿using System;
using AutoMapper;
using DiabloII.Application.Extensions;
using DiabloII.Application.Tests.Extensions;
using DiabloII.Domain.Configurations;
using DiabloII.Domain.Repositories;
using DiabloII.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiabloII.Application.Tests.Startup
{
    public class TestStartup
    {
        internal static readonly Type ApplicationType = typeof(Application.Startup);
        internal static readonly Type InfrastructureType = typeof(ErrorLogRepository);
        internal static readonly Type DomainType = typeof(IErrorLogRepository);
       
        private readonly IConfiguration _configuration;

        public TestStartup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services) => services
            .AddAutoMapper(ApplicationType, DomainType)
            .AddMySwagger()
            .AddMyMvc()
            .AddCors()
            .AddRouting(options => options.LowercaseUrls = true)
            .RegisterTestDbContDbContextDependency()
            .RegisterTheTestApplicationDependencies()
            .AddMySmtpServer(_configuration.GetSection("Smtp").Get<SmtpConfiguration>());

        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment environment) => applicationBuilder
            .UseMyExceptionPages(environment)
            .UseMyCors()
            .UseMvc()
            .UseMySwagger();
    }
}
