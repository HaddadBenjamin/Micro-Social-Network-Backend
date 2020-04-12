using System;
using AutoMapper;
using DiabloII.Application.Extensions;
using DiabloII.Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiabloII.Application.Tests
{
    public class TestStartup
    {
        internal static readonly Type ApplicationType = typeof(Startup);
        internal static readonly Type DomainType = typeof(IErrorLogRepository);

        private readonly IConfiguration _configuration;
        
        public TestStartup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services) => services
            .AddAutoMapper(ApplicationType, DomainType)
            .AddMySwagger()
            .AddMyMvc()
            .AddCors()
            .AddRouting(options => options.LowercaseUrls = true)
            .RegisterTheApplicationDependencies()
            .RegisterTestDbContDbContextDependency();

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment environment) => applicationBuilder
            .UseMyExceptionPages(environment)
            .UseMyCors()
            .UseMvc()
            .UseMySwagger();
    }
}
