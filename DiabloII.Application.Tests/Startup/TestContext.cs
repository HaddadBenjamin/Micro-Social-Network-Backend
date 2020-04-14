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
    public sealed class TestContext : IDisposable
    {
        public readonly HttpContext HttpContext;

        public readonly ApiContext ApiContext;

        public readonly ApplicationDbContext DbContext;

        private TestServer _testServer;

        private IServiceProvider _serviceProvider;

        public TestContext()
        {
            var webHostBuilder = new WebHostBuilder()
                .ConfigureServices(InitializeServices)
                .UseEnvironment("Development")
                .UseStartup(typeof(TestStartup));

            _testServer = new TestServer(webHostBuilder);
            _serviceProvider = _testServer.Services;

            var httpClient = _testServer.CreateClient();
            httpClient.BaseAddress = new Uri("http://localhost:56205/api/v1/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpContext = new HttpContext(httpClient);

            ApiContext = new ApiContext(HttpContext);
            DbContext = _serviceProvider.GetService<ApplicationDbContext>();

            webHostBuilder.ConfigureServices((services) => services.AddSingleton<IServiceProvider>(_serviceProvider));
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
            HttpContext.Dispose();
            _testServer.Dispose();
        }
    }
}