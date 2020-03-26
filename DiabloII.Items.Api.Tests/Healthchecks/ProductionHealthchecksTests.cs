using System.Net;
using Microsoft.Extensions.Configuration;
using DiabloII.Items.Api.Extensions;
using DiabloII.Items.Api.Helpers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Items.Api.Tests.Healthchecks
{
    [TestFixture]
    public class ProductionHealthchecksTests
    {
        private IConfiguration _configuration;

        [OneTimeSetUp]
        public void OneTimeSetup() =>
            _configuration = new ConfigurationBuilder()
                .AddAzureKeyVault()
                .AddJsonFile("appsettings.Production.json")
                .Build();

        [Test]
        public void AzureWebApp_ShouldRespond()
        {
            var azureWebAppUrl = _configuration["InfrastructureUrls:AzureWebApp"];

            Should.NotThrow(() => WebRequest.Create(azureWebAppUrl).GetResponse(), "The production azure web app don't respond");
        }

        [Test]
        public void FrontApp_ShouldRespond()
        {
            var frontAppUrl = _configuration["InfrastructureUrls:FrontApp"];

            Should.NotThrow(() => WebRequest.Create(frontAppUrl).GetResponse(), "The production front app don't respond");
        }

        [Test]
        public void Database_ShouldResponse()
        {
            var connectionString = DatabaseHelpers.GetApplicationDbContextConnectionString(_configuration, "Diablo II Documentation - Tests");

            using (var dbContext = DatabaseHelpers.GetTheDatabase(connectionString))
            {
                dbContext.Database.SetCommandTimeout(5);

                var canConnect = dbContext.Database.CanConnect();
                    
                canConnect.ShouldBe(true, "The production database don't respond.");
            }
        }
    }
}
