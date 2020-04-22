using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using DiabloII.Application.Tests.Services.Http;
using DiabloII.Application.Tests.Startup;
using DiabloII.Infrastructure.DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiabloII.Application.Tests
{
    public sealed class TestContext : IDisposable
    {
        public readonly HttpService HttpService;

        public readonly ApplicationDbContext DbContext;

        public IServiceCollection Services;

        public readonly TestServer TestServer;

        public TestContext()
        {
            TestServer = new TestServer(CreateTheWebHostBuilder());

            var httpClient = ConfigureTheHttpClient(TestServer.CreateClient());

            HttpService = new HttpService(httpClient);
            DbContext = TestServer.Services.GetService<ApplicationDbContext>();
        }

        private IWebHostBuilder CreateTheWebHostBuilder() => new WebHostBuilder()
            .ConfigureServices(InitializeServices)
            .UseEnvironment("Test")
            .ConfigureAppConfiguration((builder => builder.AddJsonFile("appsettings.Test.json")))
            .UseStartup(typeof(TestStartup));

        private void InitializeServices(IServiceCollection services)
        {
            var startupAssembly = typeof(Application.Startup).GetTypeInfo().Assembly;

            var manager = new ApplicationPartManager
            {
                ApplicationParts = { new AssemblyPart(startupAssembly) },
                FeatureProviders = { new ControllerFeatureProvider() }
            };

            services.AddSingleton(manager);

            Services = services;
        }

        private static HttpClient ConfigureTheHttpClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("http://localhost:56205/api/v1/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        public void Dispose()
        {
            HttpService.Dispose();
            TestServer.Dispose();
        }
    }
}