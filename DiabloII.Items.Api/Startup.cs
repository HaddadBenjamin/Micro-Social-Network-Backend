using DiabloII.Items.Api.Services.Items;
using DiabloII.Items.Api.Services.Suggestions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using DiabloII.Items.Api.DbContext;
using Microsoft.EntityFrameworkCore;

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
            var dbUsername = Configuration["connectionstrings:documentation:username"];
            var dbPassword = Configuration["connectionstrings:documentation:password"];
            var dbConnection = Configuration.GetConnectionString("Documentation");
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(dbConnection)
            {
                UserID = dbUsername,
                Password = dbPassword,
                ApplicationName = "Diablo II Documentation",
            };

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(sqlConnectionStringBuilder.ConnectionString));

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

            services.AddScoped<IItemsService, ItemsService>();
            services.AddScoped<ISuggestionsService, SuggestionsService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.UseMvc();
        }
    }
}
