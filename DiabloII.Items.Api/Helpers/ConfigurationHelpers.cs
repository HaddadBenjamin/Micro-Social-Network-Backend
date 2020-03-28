using DiabloII.Items.Api.Extensions;
using Microsoft.Extensions.Configuration;

namespace DiabloII.Items.Api.Helpers
{
    public static class ConfigurationHelpers
    {
        public static IConfiguration GetMyConfiguration(string configurationFilePath = "appsettings.Development.json") => new ConfigurationBuilder()
            .AddAMyAzureKeyVault()
            .AddJsonFile(configurationFilePath)
            .Build();
    }
}