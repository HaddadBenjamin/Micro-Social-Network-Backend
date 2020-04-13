using System;
using System.Net.Http.Headers;
using System.Reflection;
using DiabloII.Infrastructure.DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace DiabloII.Application.Tests.Startup
{
    public sealed class MyTestContext : IDisposable
    {
        public readonly MyHttpClient HttpClient;

        public readonly MyApis Apis;

        public readonly ApplicationDbContext DbContext;

        private TestServer _server;

        private IServiceProvider _serviceProvider;

        public MyTestContext()
        {
            var webHostBuilder = new WebHostBuilder()
                .ConfigureServices(InitializeServices)
                .UseEnvironment("Development")
                .UseStartup(typeof(MyTestStartup));

            _server = new TestServer(webHostBuilder);
            _serviceProvider = _server.Services;

            var httpClient = _server.CreateClient();
            httpClient.BaseAddress = new Uri("http://localhost:56205/api/v1/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpClient = new MyHttpClient(httpClient);

            Apis = new MyApis(HttpClient);
            DbContext = _serviceProvider.GetService<ApplicationDbContext>();
        }

        private static void InitializeServices(IServiceCollection services)
        {
            var startupAssembly = typeof(Application.Startup).GetTypeInfo().Assembly;

            var manager = new ApplicationPartManager
            {
                ApplicationParts = {new AssemblyPart(startupAssembly)},
                FeatureProviders = {new ControllerFeatureProvider()}
            };

            services.AddSingleton(manager);
        }

        private static ApplicationDbContext GetDbContext(IWebHost webHost)
        {
            using (var serviceScope = webHost.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                return services.GetService<ApplicationDbContext>();
            }
        }

        public void Dispose()
        {
            HttpClient.Dispose();
            _server.Dispose();
        }
    }
}