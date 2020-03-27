using System;
using System.IO;
using System.Linq;
using DiabloII.Items.Api.DbContext.Items.Models;
using DiabloII.Items.Api.Extensions;
using DiabloII.Items.Api.Helpers;
using DiabloII.Items.Reader;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DiabloII.Items.Generator
{
    public static class ItemsGenerator
    {
        public static void Generate()
        {
            var uniqueItemDestinationPath = Path.Combine(Directory.GetCurrentDirectory(), "Files/Uniques.json");
            var diabloFilesReader = new DiabloIIFilesReader();

            var uniqueItems = diabloFilesReader.Read();
            var dbItems = uniqueItems.Select(item =>
            {
                var itemId = Guid.NewGuid();

                return new Item
                {
                    Id = itemId,
                    Name = item.Name,
                    Category = Enum.Parse<ItemCategory>(item.Category),
                    SubCategory = item.SubCategory,
                    Type = item.Type,
                    ImageName = item.ImageName,

                    Level = item.Level,
                    LevelRequired = item.LevelRequired,

                    MinimumDefenseMinimum = item.MinimumDefenseMinimum.GetValueOrDefault(),
                    MaximumDefenseMinimum = item.MaximumDefenseMinimum.GetValueOrDefault(),
                    MinimumDefenseMaximum = item.MinimumDefenseMaximum.GetValueOrDefault(),
                    MaximumDefenseMaximum = item.MaximumDefenseMaximum.GetValueOrDefault(),

                    MinimumOneHandedDamageMinimum = item.MinimumOneHandedDamageMinimum.GetValueOrDefault(),
                    MaximumOneHandedDamageMinimum = item.MaximumOneHandedDamageMinimum.GetValueOrDefault(),
                    MinimumTwoHandedDamageMinimum = item.MinimumTwoHandedDamageMinimum.GetValueOrDefault(),
                    MaximumTwoHandedDamageMinimum = item.MaximumTwoHandedDamageMinimum.GetValueOrDefault(),
                    MinimumOneHandedDamageMaximum = item.MinimumOneHandedDamageMaximum.GetValueOrDefault(),
                    MaximumOneHandedDamageMaximum = item.MaximumOneHandedDamageMaximum.GetValueOrDefault(),
                    MinimumTwoHandedDamageMaximum = item.MinimumTwoHandedDamageMaximum.GetValueOrDefault(),
                    MaximumTwoHandedDamageMaximum = item.MaximumTwoHandedDamageMaximum.GetValueOrDefault(),

                    DexterityRequired = item.DexterityRequired.GetValueOrDefault(),
                    StrengthRequired = item.StrengthRequired.GetValueOrDefault(),

                    Properties = item.Properties
                        .Select(itemProperty => new ItemProperty
                        {
                            Id = Guid.NewGuid(),
                            ItemId = itemId,

                            FormattedName = itemProperty.FormattedName,
                            Name = itemProperty.Name,

                            Par = itemProperty.Par,
                            Minimum = itemProperty.Minimum,
                            Maximum = itemProperty.Maximum,
                            IsPercent = itemProperty.IsPercent,
                            FirstChararacter = itemProperty.FirstChararacter,
                            OrderIndex = itemProperty.OrderIndex,
                        }).ToList()
                };
            });
            var configurationFilePaths = new[] { "appsettings.Development.json", "appsettings.Production.json" };

            foreach (var configurationFilePath in configurationFilePaths)
            {
                var configuration = new ConfigurationBuilder()
                    .AddAMyAzureKeyVault()
                    .AddJsonFile(configurationFilePath)
                    .Build();
                var connectionString =
                    DatabaseHelpers.GetTheConnectionString(configuration, "Diablo II Documentation - Tests");

                using (var dbContext = DatabaseHelpers.GetTheDbContext(connectionString))
                {
                    dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE [Items]");

                    dbContext.Items.AddRange(dbItems);
                    dbContext.SaveChanges();
                }
            }
            //var uniqueItemsAsJson = JsonConvert.SerializeObject(uniqueItems, Formatting.Indented);

            //File.WriteAllText(uniqueItemDestinationPath, uniqueItemsAsJson);
        }
    }
}



