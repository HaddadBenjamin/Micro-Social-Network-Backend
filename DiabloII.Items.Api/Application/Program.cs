using DiabloII.Items.Api.Infrastructure.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DiabloII.Items.Api.Application
{
    public class Program
    {
        public static void Main(string[] arguments) => BuildWebHost(arguments).Run();

        public static IWebHost BuildWebHost(string[] arguments) => WebHost
            .CreateDefaultBuilder(arguments)
            .ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) => configurationBuilder.AddAMyAzureKeyVault())
            .UseStartup<Startup>()
            .Build();
    }
}
