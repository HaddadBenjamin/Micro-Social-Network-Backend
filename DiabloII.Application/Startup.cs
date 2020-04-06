using AutoMapper;
using DiabloII.Application.Extensions;
using DiabloII.Domain.Mappers.Suggestions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiabloII.Application
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services) => services
            .AddAutoMapper(typeof(Startup), typeof(SuggestionCommandToDataLayer))
            .AddMySwagger()
            .AddMyMvc()
            .AddCors()
            .AddRouting(options => options.LowercaseUrls = true)
            .RegisterMyDependencies(_configuration);

        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment environment) => applicationBuilder
            .UseMyExceptionPages(environment)
            .PlayAllTheDatabaseMigrations()
            .UseMyCors()
            .UseMvc()
            .UseMySwagger();
    }
}
