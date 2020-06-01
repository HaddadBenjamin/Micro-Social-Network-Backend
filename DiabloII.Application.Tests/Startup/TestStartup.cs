using System;
using Autofac;
using AutoMapper;
using DiabloII.Application.Extensions;
using DiabloII.Application.Tests.Extensions;
using DiabloII.Domain.Configurations;
using DiabloII.Domain.Repositories.Domains;
using DiabloII.Infrastructure.Repositories;
using MediatR;
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
        internal static readonly Type ApplicationTestsType = typeof(TestStartup);

        private readonly IConfiguration _configuration;

        public TestStartup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services) => services
            .AddAutoMapper(ApplicationType, DomainType)
            .AddMySwagger()
            .AddMyMvc()
            .AddCors()
            .AddRouting(options => options.LowercaseUrls = true)
            .RegisterTestDbContDbContextDependency()
            .AddMySmtpServer(_configuration.GetSection("Smtp").Get<SmtpConfiguration>())
            .AddMediatR(InfrastructureType);

        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment environment) => applicationBuilder
            .UseMyExceptionPages(environment)
            .UseMyCors()
            .UseMvc()
            .UseMySwagger();

        public void ConfigureContainer(ContainerBuilder builder) =>
            builder.RegisterAllImplementedInterfaceAndSelfFromAssemblies(ApplicationType, InfrastructureType, DomainType);
    }
}
