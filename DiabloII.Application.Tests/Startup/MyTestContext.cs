using System;
using System.Net.Http.Headers;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace DiabloII.Application.Tests.Startup
{
    public sealed class MyTestContext : IDisposable
    {
        public static MyHttpClient HttpClient;

        private static MyTestContext instance = null;

        private static readonly object padlock = new object();

        public MyApis Apis { get; private set; }

        public MyTestsContexts Contexts { get; private set; }

        private TestServer _server;

        public MyTestContext()
        {
            var webHostBuilder = new WebHostBuilder()
                .ConfigureServices(InitializeServices)
                .UseEnvironment("Development")
                .UseStartup(typeof(TestStartup));

            _server = new TestServer(webHostBuilder);

            var httpClient = _server.CreateClient();
            httpClient.BaseAddress = new Uri("http://localhost:56205/api/v1/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpClient = new MyHttpClient(httpClient);
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

        public static MyTestContext Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                            instance = new MyTestContext
                            {
                                Apis = new MyApis(HttpClient),
                                Contexts = new MyTestsContexts()
                            };
                    }
                }

                return instance;
            }
        }

        public void Dispose()
        {
            HttpClient.Dispose();
            _server.Dispose();
        }
    }
}