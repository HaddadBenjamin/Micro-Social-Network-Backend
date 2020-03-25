//using DiabloII.Items.Api.Items.Services;

using System.Data.SqlClient;
using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Services.Items;
using DiabloII.Items.Api.Services.Suggestions;
using DiabloII.Items.Generator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace DiabloII.Items.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
			//ItemsGenerator.Generate();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var dbPassword = Configuration["connectionstrings:documentation:password"];
            var dbConnection = Configuration.GetConnectionString("Documentation");
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(dbConnection);// { Password = dbPassword };

            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(sqlConnectionStringBuilder.ConnectionString));

            services.AddCors(); // AddCors doit-être au dessus de AddMvc.
            services.AddMvc(options => options.Filters.Add(new ErrorHandlingFilter()))
                    .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
    
            services.AddSwaggerGen(swagger =>
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
            
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddTransient<IItemsService, ItemsService>();
            //services.AddTransient<ISuggestionsService, SuggestionsService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            //    context.Database.Migrate();
            //}
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }

            app.UseSwagger();
            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                swagger.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

           
            app.UseCors(builder => builder  // UseCors doit être au dessus de UseMvc;
                .WithOrigins
                (
                    "http://localhost:3000", // Local
                    "https://diablo-2-enriched-documentation.netlify.com/" // Production
                )
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.UseMvc();
        }
    }
}
