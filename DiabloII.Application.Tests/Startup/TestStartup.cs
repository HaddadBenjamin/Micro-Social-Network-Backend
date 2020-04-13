using System;
using AutoMapper;
using DiabloII.Application.Extensions;
using DiabloII.Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using DiabloII.Application.Tests.Extensions;

namespace DiabloII.Application.Tests
{
    public class TestStartup
    {
        internal static readonly Type ApplicationType = typeof(Startup);
        internal static readonly Type DomainType = typeof(IErrorLogRepository);

        public void ConfigureServices(IServiceCollection services) => services
            .AddAutoMapper(ApplicationType, DomainType)
            .AddMySwagger()
            .AddMyMvc()
            .AddCors()
            .AddRouting(options => options.LowercaseUrls = true)
            .RegisterTestDbContDbContextDependency()
            .RegisterTheApplicationDependencies();

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment environment) => applicationBuilder
            .UseMyExceptionPages(environment)
            .UseMyCors()
            .UseMvc()
            .UseMySwagger();
    }
}
