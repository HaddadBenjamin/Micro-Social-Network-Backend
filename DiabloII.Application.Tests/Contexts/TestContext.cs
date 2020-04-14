using System;
using System.Net.Http;
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

        public readonly RepositoryContext RepositoryContext;

        private TestServer _testServer;

        public TestContext()
        {
            var webHostBuilder = CreateTheWebHostBuilder();

            _testServer = new TestServer(webHostBuilder);

            var httpClient = ConfigureTheHttpClient(_testServer.CreateClient());

            HttpContext = new HttpContext(httpClient);
            ApiContext = new ApiContext(HttpContext);
            DbContext = _testServer.Services.GetService<ApplicationDbContext>();
            RepositoryContext = new RepositoryContext(ApiContext);
        }

        private static IWebHostBuilder CreateTheWebHostBuilder() => new WebHostBuilder()
            .ConfigureServices(InitializeServices)
            .UseEnvironment("Development")
            .UseStartup(typeof(TestStartup));

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

        private static HttpClient ConfigureTheHttpClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("http://localhost:56205/api/v1/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        public void Dispose()
        {
            HttpContext.Dispose();
            _testServer.Dispose();
        }
    }
}