using DiabloII.Items.Api.Services.Items;
using DiabloII.Items.Api.Services.Suggestions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DiabloII.Items.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
      
        public Startup(IConfiguration configuration)
        {
            //ItemsGenerator.Generate();
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
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

            services.AddMvc(options => options.Filters.Add(new ErrorHandlingFilter()))
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddCors();

            services.AddRouting(options => options.LowercaseUrls = true);


            var connectionString = DatabaseHelpers.GetTheDbContextConnectionString(_configuration);
          
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<IItemsService, ItemsService>();
            services.AddTransient<ISuggestionsService, SuggestionsService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }

            app.UseCors(builder => builder  // UseCors doit être au dessus de UseMvc;
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.UseMvc(); 

            app.UseSwagger();
            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                swagger.RoutePrefix = string.Empty;
            });
        }
    }
}
