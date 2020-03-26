using DiabloII.Items.Api.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DiabloII.Items.Api
{
    public class Program
    {
        public static void Main(string[] args) => BuildWebHost(args).Run();

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) => configurationBuilder.AddAzureKeyVault())
                .UseStartup<Startup>()
                .Build();
    }
}
