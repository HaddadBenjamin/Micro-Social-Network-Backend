using System.Net;
using DiabloII.Items.Api.Extensions;
using DiabloII.Items.Api.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Shouldly;

namespace DiabloII.Items.Api.Tests.HealthChecks
{
    [Ignore("Those tests can't run before the deploy process.")]
    [TestFixture]
    public class ProductionHealthCheckTests
    {
        private IConfiguration _configuration;

        [OneTimeSetUp]
        public void OneTimeSetup() =>
            _configuration = new ConfigurationBuilder()
                .AddAMyAzureKeyVault()
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
            var connectionString = DatabaseHelpers.GetTheConnectionString(_configuration, "Diablo II Documentation - Tests");

            using (var dbContext = DatabaseHelpers.GetTheDbContext(connectionString))
            {
                dbContext.Database.SetCommandTimeout(5);

                var canConnectToTheDatabase = dbContext.Database.CanConnect();
                    
                canConnectToTheDatabase.ShouldBe(true, "The production database don't respond.");
            }
        }
    }
}
