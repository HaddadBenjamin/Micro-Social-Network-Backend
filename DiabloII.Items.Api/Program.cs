using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace DiabloII.Items.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) =>
                {
                    //var keyVaultEndpoint = "https://benjaminvault.vault.azure.net/";
                    //var azureServiceTokenProvider = new AzureServiceTokenProvider();
                    //var defaultKeyVaultSecretManager = new DefaultKeyVaultSecretManager();
                    //var authentificationCallback = new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback);
                    //var keyVaultClient = new KeyVaultClient(authentificationCallback);
                     
                    //configurationBuilder.AddAzureKeyVault(keyVaultEndpoint, keyVaultClient, defaultKeyVaultSecretManager);
                })
                .UseStartup<Startup>()
                .Build();
    }
}
