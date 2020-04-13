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
        public static MyHttpClient HttpClient;

        private static MyTestContext instance = null;

        private static readonly object padlock = new object();

        public MyApis Apis { get; private set; }

        public MyTestsContexts Contexts { get; private set; }

        public ApplicationDbContext DbContext { get; private set; }

        private TestServer _server;

        private IWebHost _webHost;

        public static MyTestContext Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            var webHostBuilder = new WebHostBuilder()
                                .ConfigureServices(InitializeServices)
                                .UseEnvironment("Development")
                                .UseStartup(typeof(MyTestStartup));

                            var server = new TestServer(webHostBuilder);

                            var httpClient = server.CreateClient();
                            httpClient.BaseAddress = new Uri("http://localhost:56205/api/v1/");
                            httpClient.DefaultRequestHeaders.Accept.Clear();
                            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            HttpClient = new MyHttpClient(httpClient);

                            var webHost = webHostBuilder.Build();

                            instance = new MyTestContext
                            {
                                _webHost = webHost,
                                _server = server,
                                Apis = new MyApis(HttpClient),
                                Contexts = new MyTestsContexts(),
                                DbContext = GetDbContext(webHost)
                            };
                        }
                    }
                }

                return instance;
            }
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