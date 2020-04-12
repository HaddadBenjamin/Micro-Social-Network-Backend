using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using DiabloII.Application.Tests;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TechTalk.SpecFlow;

namespace DiabloII.Application.Tests.Suggestions
{
    [Binding]
    public class CreateASuggestionSteps
    {

        public static string GetProjectPath(string projectRelativePath, Assembly startupAssembly)
        {
            var projectName = startupAssembly.GetName().Name;

            var applicationBasePath = AppContext.BaseDirectory;

            var directoryInfo = new DirectoryInfo(applicationBasePath);

            do
            {
                directoryInfo = directoryInfo.Parent;

                var projectDirectoryInfo = new DirectoryInfo(Path.Combine(directoryInfo.FullName, projectRelativePath));

                if (projectDirectoryInfo.Exists)
                    if (new FileInfo(Path.Combine(projectDirectoryInfo.FullName, projectName, $"{projectName}.csproj")).Exists)
                        return Path.Combine(projectDirectoryInfo.FullName, projectName);
            }
            while (directoryInfo.Parent != null);

            throw new Exception($"Project root could not be located using the application root {applicationBasePath}.");
        }

        private TestServer Server;


        public HttpClient Client { get; }

        public void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
        }

        protected virtual void InitializeServices(IServiceCollection services)
        {
            var startupAssembly = typeof(Startup).GetTypeInfo().Assembly;

            var manager = new ApplicationPartManager
            {
                ApplicationParts =
                {
                    new AssemblyPart(startupAssembly)
                },
                FeatureProviders =
                {
                    new ControllerFeatureProvider(),
                    new ViewComponentFeatureProvider()
                }
            };

            services.AddSingleton(manager);
        }

        private WebApplicationFactory<TestStartup> _factory;

        private HttpClient _client { get; set; }

        public CreateASuggestionSteps(WebApplicationFactory<TestStartup> factory)
        {
            _factory = factory;
        }

        [Given(@"I am a client")]
        public async Task GivenIAmAClient()
        {
            var webHostBuilder = new WebHostBuilder()
                .ConfigureServices(InitializeServices)
                .UseEnvironment("Development")
                .UseStartup(typeof(TestStartup));

            // Create instance of test server
            var Server = new TestServer(webHostBuilder);

            // Add configuration for client
            var Client = Server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost:56205");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



            var xxxx1 = await Client.GetAsync("/");
            var xxxx2 = await Client.GetAsync("/api");
            var xxxx3 = await Client.GetAsync("/api/v1");
            var xxxx4 = await Client.GetAsync("/api/v1/suggestions");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<TestStartup>();
    }
}
