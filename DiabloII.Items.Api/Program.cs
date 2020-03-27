using DiabloII.Items.Api.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DiabloII.Items.Api
{
    public class Program
    {
        public static void Main(string[] arguments) => BuildWebHost(arguments).Run();

        public static IWebHost BuildWebHost(string[] arguments) =>
            WebHost.CreateDefaultBuilder(arguments)
                .ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) => configurationBuilder.AddAMyAzureKeyVault())
                .UseStartup<Startup>()
                .Build();
    }
}
