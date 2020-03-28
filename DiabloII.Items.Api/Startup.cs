using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Helpers;
using DiabloII.Items.Api.Services.ErrorLogs;
using DiabloII.Items.Api.Services.Items;
using DiabloII.Items.Api.Services.Suggestions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace DiabloII.Items.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services) => services
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

    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddMySwagger(this IServiceCollection services) => services
            .AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Diablo II - items and suggestions API",
                    Description = "Allow to search Diablo II items and crud suggestions and votes",
                    Contact = new Contact
                    {
                        Name = "Un passionné dans la foule (alias Firefouks)",
                        Url = "https://github.com/HaddadBenjamin"
                    }
                });
                swagger.DescribeAllEnumsAsStrings();
            });

        public static IServiceCollection AddMyMvc(this IServiceCollection services)
        {
            services
                .AddMvc(options => options.Filters.Add(new ErrorHandlingFilter()))
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            return services;
        }

        public static void RegisterMyDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = DatabaseHelpers.GetMyConnectionString(configuration);

            services
                .AddDbContextPool<ApplicationDbContext>(optionsBuilder => optionsBuilder.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()))
                .AddTransient<IItemsService, ItemsService>()
                .AddTransient<ISuggestionsService, SuggestionsService>()
                .AddTransient<IErrorLogsService, ErrorLogsService>();
        }
    }

    public static class ApplicationBuilderExtensions
    {

        public static IApplicationBuilder UseMyExceptionPages(this IApplicationBuilder applicationBuilder, IHostingEnvironment environment)
        {
            if (environment.IsDevelopment())
                applicationBuilder.UseDeveloperExceptionPage();

            return applicationBuilder;
        }

        public static IApplicationBuilder PlayAllTheDatabaseMigrations(this IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }

            return applicationBuilder;
        }

        public static IApplicationBuilder UseMyCors(this IApplicationBuilder applicationBuilder) => applicationBuilder
            .UseCors(policyBuilder => policyBuilder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

        public static IApplicationBuilder UseMySwagger(this IApplicationBuilder applicationBuilder) => applicationBuilder
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                options.RoutePrefix = string.Empty;
            });
    }
}
