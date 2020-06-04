using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DiabloII.Domain.Commands.Domains.Items;
using DiabloII.Domain.Models.Items;
using DiabloII.Infrastructure.Handlers.Domains;
using DiabloII.Infrastructure.Helpers;
using DiabloII.Infrastructure.Repositories;
using DiabloII.Items.Reader;
using DiabloII.Items.Reader.Items;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ItemCategory = DiabloII.Domain.Models.Items.ItemCategory;
using ItemQuality = DiabloII.Domain.Models.Items.ItemQuality;

namespace DiabloII.Items.Generator
{
    public static class ItemsGenerator
    {
        private static readonly int CommandTimeout = 600;

        public static void Generate(GenerationEnvironment[] environments)
        {
            var uniqueItems = new DiabloIIFilesReader().Read();

            InsertTheUniqueItemsInAJsonFile(uniqueItems);
            UpdateTheItemsFromDatabase(environments, uniqueItems);
        }

        private static void InsertTheUniqueItemsInAJsonFile(IEnumerable<ItemFromFile> uniqueItems)
        {
            var uniqueItemDestinationPath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Uniques.json");
            var uniqueItemsAsJson = JsonConvert.SerializeObject(uniqueItems, Formatting.Indented);

            File.WriteAllText(uniqueItemDestinationPath, uniqueItemsAsJson);
        }

        private static void UpdateTheItemsFromDatabase(GenerationEnvironment[] environments, IEnumerable<ItemFromFile> uniqueItems)
        {
            var mapper = GetMapper();
            var items = uniqueItems.Select(item => mapper.Map<Item>(item)).ToList();
            var itemProperties = items.SelectMany(item => item.Properties).ToList();
            var configurationFilePaths = environments.Select(environment => $"appsettings.{environment.ToString()}.json");

            foreach (var configurationFilePath in configurationFilePaths)
            {
                var configuration = ConfigurationHelpers.GetMyConfiguration(configurationFilePath);
                var connectionString = DatabaseHelpers.GetMyConnectionString(configuration, nameof(DiabloII.Items.Generator));

                using (var dbContext = DatabaseHelpers.GetMyDbContext(connectionString))
                {
                    dbContext.Database.SetCommandTimeout(CommandTimeout);
                    dbContext.Database.Migrate();

                    var itemRepository = new ItemRepository(dbContext);
                    var command = new ResetItemsCommand
                    {
                        Items = items,
                        ItemProperties = itemProperties
                    };

                    new ItemCommandHandler(itemRepository, dbContext).Handle(command);
                }
            }
        }

        private static IMapper GetMapper()
        {
            var mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<Reader.Items.ItemFromFile, Item>()
                    .AfterMap((source, destination) =>
                    {
                        destination.Id = Guid.NewGuid();
                        destination.Quality = Enum.Parse<ItemQuality>(source.Quality);
                        destination.Category = Enum.Parse<ItemCategory>(source.Category);

                        destination.Properties = destination.Properties.Select(_ =>
                        {
                            _.ItemId = destination.Id;

                            return _;
                        }).ToList();
                    });

                configuration.CreateMap<Reader.Items.ItemPropertyFromFile, ItemProperty>()
                    .AfterMap((source, destination) => destination.Id = Guid.NewGuid());
            });
            var mapper = mapperConfiguration.CreateMapper();

            return mapper;
        }
    }
}



