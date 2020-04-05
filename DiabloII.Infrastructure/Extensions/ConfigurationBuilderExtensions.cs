using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace DiabloII.Infrastructure.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddAMyAzureKeyVault(this IConfigurationBuilder configurationBuilder)
        {
            var vaultEndpoint = "https://benjamintestvault.vault.azure.net/";
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var defaultKeyVaultSecretManager = new DefaultKeyVaultSecretManager();
            var authentificationCallback = new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback);
            var keyVaultClient = new KeyVaultClient(authentificationCallback);

            return configurationBuilder.AddAzureKeyVault(vaultEndpoint, keyVaultClient, defaultKeyVaultSecretManager);
        }
    }
}