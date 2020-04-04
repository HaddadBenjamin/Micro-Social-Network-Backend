using DiabloII.Items.Api.DbContext.Items.Models;
using DiabloII.Items.Api.Helpers;
using DiabloII.Items.Reader;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using DiabloII.Items.Api.Services.Items;
using DiabloII.Items.Api.Services.Suggestions;
using Newtonsoft.Json;

namespace DiabloII.Items.Generator
{
    public static class ItemsGenerator
    {
        private static readonly int CommandTimeout = 600;

        public static void Generate(GenerationEnvironment[] environments)
        {
            var diabloFilesReader = new DiabloIIFilesReader();
            var uniqueItems = diabloFilesReader.Read();

            // create the unique items file
            var uniqueItemDestinationPath = Path.Combine(Directory.GetCurrentDirectory(), "Files/Uniques.json");
            var uniqueItemsAsJson = JsonConvert.SerializeObject(uniqueItems, Formatting.Indented);

            File.WriteAllText(uniqueItemDestinationPath, uniqueItemsAsJson);

            // Empty and fill all the item tables in all the environments.
            var items = uniqueItems.Select(item =>
            {
                var itemId = Guid.NewGuid();

                return new Item
                {
                    Id = itemId,
                    Name = item.Name,
                    Quality = Enum.Parse<ItemQuality>(item.Quality),
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
            }).ToList();
            var itemProperties = items
                .SelectMany(item => item.Properties)
                .ToList();
            var configurationFilePaths = environments.Select(environment => $"appsettings.{environment.ToString()}.json");

            foreach (var configurationFilePath in configurationFilePaths)
            {
                var configuration = ConfigurationHelpers.GetMyConfiguration(configurationFilePath);
                var connectionString = DatabaseHelpers.GetMyConnectionString(configuration, "DiabloII.Items.Generator");

                using (var dbContext = DatabaseHelpers.GetMyDbContext(connectionString))
                {
                    dbContext.Database.SetCommandTimeout(CommandTimeout);
                    dbContext.Database.Migrate();

                    var itemRepository = new ItemRepository(dbContext);

                    new ItemsService(dbContext, itemRepository).ResetTheItems(items, itemProperties);
                }
            }
        }
    }
}



