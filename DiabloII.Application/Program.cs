using Autofac;
using Autofac.Extensions.DependencyInjection;
using DiabloII.Application.Extensions;
using DiabloII.Infrastructure.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DiabloII.Application
{
    public class Program
    {
        public static void Main(string[] arguments) => BuildHost(arguments).Run();

    public static IHost BuildHost(string[] arguments) => Host
        .CreateDefaultBuilder(arguments)
        .ConfigureContainer<ContainerBuilder>(builder =>  builder.RegisterAllImplementedInterfaceAndSelfFromAssemblies(Startup.ApplicationType, Startup.InfrastructureType, Startup.DomainType))
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .ConfigureWebHostDefaults(webHostBuilder =>
        {
            webHostBuilder
                .ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) => configurationBuilder.AddAMyAzureKeyVault())
                .UseStartup<Startup>();
        })
        .Build();
    }
}
