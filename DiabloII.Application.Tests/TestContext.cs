using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace DiabloII.Application.Tests
{
    public class TestContext : IDisposable
    {
        public HttpClient Client;
        private TestServer _server;

        public TestContext()
        {
            var webHostBuilder = new WebHostBuilder()
                .ConfigureServices(InitializeServices)
                .UseEnvironment("Development")
                .UseStartup(typeof(TestStartup));

            _server = new TestServer(webHostBuilder);

            Client = _server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost:56205/api/v1/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void InitializeServices(IServiceCollection services)
        {
            var startupAssembly = typeof(Startup).GetTypeInfo().Assembly;

            var manager = new ApplicationPartManager
            {
                ApplicationParts = { new AssemblyPart(startupAssembly) },
                FeatureProviders = { new ControllerFeatureProvider() }
            };

            services.AddSingleton(manager);
        }   

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }

    }
}