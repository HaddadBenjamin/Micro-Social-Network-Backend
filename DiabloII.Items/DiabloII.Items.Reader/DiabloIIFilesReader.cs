using DiabloII.Items.Reader.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiabloII.Items.Reader
{
    public class DiabloIIFilesReader
    {
        private List<string> MissingItemTypes = new List<String>();

        // TODO : 
        // - DEMANDER LE NOUVEAU DATASHEET A ASCENDED, IL A RAJOUTER DES COLONNES (SLOTS et chez pas quoi).

        // - Mapper types to a SubCategory
        // - Add those data in a database : 1) front ask items for a type. 2) back service is call that ask the db those items. 3) it will be faster than generate all items each time.
        // - Trier les attributs : stat requis / damage / armure en premier / le reste trier en mode alphabétique ?
        // to verify : 
        //            mon update subcategories
        //            les types que j'ai commenté 
        //Type: Archon_soul_shard => Archon_Soul_Shard
        //Découper eppe une main avec 2 mains, il faudra aussi qu'ascended vérifie la data
        //découpage spear / lance
        //demander aux users de me remonter tout les problèmes sur le discord / chanell website
        //split between helm / tiara / druid helm
        //shards / wand
        //Rajouter ces différences dans le mapping des catégories.
        // Il faudra que je mette à jour les énums côté API
        public IEnumerable<Item> Read(string uniquesCsv, string weaponsCsv, string armorsCsv)
        {
            var weapons = ReadWeapons();

            return uniquesCsv
                    .Split('\n')
                    .Skip(1)
                    .Select(line =>
                    {
                        var itemData = line.Split(';');

                        if (itemData.Length < 4)
                            return null;

                        var type = TypeToItemType(itemData[3]);
                        var properties = new List<ItemProperty>();

                        for (var index = 4; index < itemData.Length; index += 4)
                        {
                            if (string.IsNullOrEmpty(itemData[index]))
                                continue;

                            properties.Add(new ItemProperty
                            {
                                Name = itemData[index],
                                Par = itemData[index + 1].ParseIntOrDefault(),
                                Minimum = itemData[index + 2].ParseIntOrDefault(),
                                Maximum = itemData[index + 3].ParseIntOrDefault(),
                                IsPercent = itemData[index].Contains("%")
                            });
                        }

                        return new Item
                        {
                            Name = itemData[0],
                            LevelRequired = itemData[2].ParseIntOrDefault(),
                            Type = type,
                            Quality = "Unique",
                            Properties = properties
                        };
                    })
                    .ToList();
        }

        private List<WeaponRecord> ReadWeapons(string weaponCsv) 
            => weaponCsv
                .Split('\n')
                .Skip(1)
                .Select(line =>
                {
                    var itemData = line.Split(';');

                    return new WeaponRecord
                    {
                        Name = itemData[0],
                        Type = itemData[1],
                        Slot = itemData[2]
                    };
                })
                .ToList();

        private ItemType TypeToItemType(string type)
        {
            if (string.IsNullOrEmpty(type))
                return ItemType.UNKNOWN;

            var formattedItemType = FormatItemType(type);

            if (Enum.TryParse<ItemType>(formattedItemType, out _))
                return (ItemType)Enum.Parse(typeof(ItemType), formattedItemType);

            MissingItemTypes.Add(formattedItemType);
            return ItemType.UNKNOWN;
        }

        private static string GenerateItemTypes(string datasheetCsv)
            => string.Join(",\n",
                datasheetCsv
                .Split('\n')
                .Skip(1)
                .Select(line => FormatItemType(line))
                .Distinct()
                .OrderBy(_ => _)
                .Skip(1));

        private static string FormatItemType(string type)
            => string.IsNullOrEmpty(type) ? string.Empty :
                        type
                        .Replace("2", "two")
                        .Replace(' ', '_')
                        .Replace('’', '_')
                        .Replace('-', '_')
                        .FirstCharToUpper();
    }

    public class WeaponRecord
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Slot { get; set; }
    }

    public class ArmorReord
    {
        public string Name { get; set; }
        public string Slot { get; set; }
    }
}