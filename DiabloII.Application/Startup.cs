using System;
using System.Text.Json;
using AutoMapper;
using DiabloII.Domain.Handlers;
using DiabloII.Domain.Mappers.Suggestions;
using DiabloII.Domain.Readers;
using DiabloII.Domain.Repositories;
using DiabloII.Domain.Validations.Suggestions.Comment;
using DiabloII.Domain.Validations.Suggestions.Create;
using DiabloII.Domain.Validations.Suggestions.Delete;
using DiabloII.Domain.Validations.Suggestions.DeleteAComment;
using DiabloII.Domain.Validations.Suggestions.Vote;
using DiabloII.Infrastructure.DbContext;
using DiabloII.Infrastructure.Handlers;
using DiabloII.Infrastructure.Helpers;
using DiabloII.Infrastructure.Readers;
using DiabloII.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

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
                .AddNewtonsoftJson()
                //.AddJsonOptions(options => options.JsonSerializerOptions = new DefaultContractResolver())
                .AddFluentValidation();

            return services;
        }

        public static void RegisterMyDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = DatabaseHelpers.GetMyConnectionString(configuration);

            services
                .AddDbContextPool<ApplicationDbContext>(optionsBuilder =>
                    optionsBuilder.UseSqlServer(connectionString,
                        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()))
                .AddScoped<IItemReader, ItemReader>()
                .AddScoped<ISuggestionRepository, SuggestionRepository>()
                .AddScoped<IErrorLogRepository, ErrorLogRepository>()
                .AddScoped<IItemRepository, ItemRepository>()
                .AddScoped<ISuggestionReader, SuggestionReader>()
                .AddScoped<IErrorLogReader, ErrorLogReader>()
                .AddScoped<ISuggestionCommandHandler, SuggestionCommandHandler>()
                .AddScoped<CreateASuggestionValidator>()
                .AddScoped<VoteToASuggestionValidator>()
                .AddScoped<CommentASuggestionValidator>()
                .AddScoped<DeleteASuggestionValidator>()
                .AddScoped<DeleteASuggestionCommentValidator>();

            //var builder = new ContainerBuilder();
            //builder.Populate(services);

            //var assemblieTypes = new[] { assemblyTypeFromApplication, assemblyTypeFromInfrastructure, assemblyTypeFromDomain};
            //var assemblies = assemblieTypes.Select(assemblyType => Assembly.GetAssembly(assemblyType));
            //builder.RegisterAssemblyTypes(assemblies.Toarr)
            //    .AsImplementedInterfaces()
            //    .AsSelf();

            //var autofacContainer = builder.Build();

            //// this will be used as the service-provider for the application!
            //return new AutofacServiceProvider(autofacContainer);
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
                .AllowAnyHeader());

        public static IApplicationBuilder UseMySwagger(this IApplicationBuilder applicationBuilder) => applicationBuilder
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                options.RoutePrefix = string.Empty;
            });
    }
}
