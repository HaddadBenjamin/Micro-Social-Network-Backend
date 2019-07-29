using DiabloII.Items.Reader.Extensions;
using DiabloII.Items.Reader.Records;
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

        // + Le mappign des noms est à vérifier pour ces noms la :
        // - 2-Handed Sword
        //- Amulet
        //- Ancientarmor
        //- Arrows
        //- Battle Guantlets
        //- Bec-De-Corbin
        //- Belt
        //- Blood Spirit
        //- Bolts
        //- Bracers
        //- Cap
        //- Cedarbow
        //- Charm
        //- Conqueror Crown
        //- Gauntlets
        //- Girdle
        //- Gloves
        //- Hammer
        //- Hard Leather
        //- Hunter’S Bow
        //- Jewel
        //- Jo Stalf
        //- Kris
        //- Martel De Fer
        //- Matriarchal Javelin
        //- Ring
        //- Sash
        //- Silver-Edged Axe
        //- Skull Guard
        //- Staff
        //- Tresllised Armor
        // - Trier les attributs : stat requis / damage / armure en premier / le reste trier en mode alphabétique ?
        // to verify : 
        // Il faudra que je mette à jour les énums côté API
        // Les types h2h et 2h2 ne sont pas mapper, il semble manquer des Claw aussi ?
        public IEnumerable<Item> Read(
            string uniquesCsv,
            string weaponsCsv,
            string armorsCsv)
        {
            var weapons = ReadWeapons(weaponsCsv);
            var armors = ReadArmors(armorsCsv);
            var subCategories = ReadSubCategories(weapons, armors);
            var uniques = ReadUniques(uniquesCsv, subCategories);

            var sub = string.Join("\n- ", subCategories.Select(s => s.Name).OrderBy(x => x));
            var uni = string.Join("\n- ", MissingItemTypes.Distinct().OrderBy(x => x));

            return uniques;
        }

        public IEnumerable<Item> ReadUniques(string uniquesCsv, List<ItemCategoryRecord> itemCategories)
            => uniquesCsv
                .Split('\n')
                .Skip(1)
                .Select(line =>
                {
                    var itemData = line.Split(';');

                    if (itemData.Length < 4)
                        return null;

                    var properties = new List<ItemProperty>();
                    var name = itemData[0];
                    var type = itemData[3].ToTitleCase();
                    var itemCategory = itemCategories.FirstOrDefault(x => x.Name == type);

                    if (itemCategory == null)
                        MissingItemTypes.Add(type);

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
                        Name = name,
                        LevelRequired = itemData[2].ParseIntOrDefault(),
                        Quality = "Unique",
                        Properties = properties,
                        Category = itemCategory?.Category,
                        SubCategory = itemCategory?.SubCategory,
                    };
                })
                .Where(item => item != null)
                .ToList();

        private List<ArmorReord> ReadArmors(string armorsCsv)
            => armorsCsv
                .Split('\n')
                .Skip(1)
                .Select(line =>
                {
                    var itemData = line.Split(';');

                    if (itemData.Length < 2)
                        return null;

                    return new ArmorReord
                    {
                        Name = itemData[0],
                        Slot = itemData[1]
                    };
                })
                .Where(item => item != null)
                .ToList();

        private List<WeaponRecord> ReadWeapons(string weaponsCsv) 
            => weaponsCsv
                .Split('\n')
                .Skip(1)
                .Select(line =>
                {
                    var itemData = line.Split(';');

                    if (itemData.Length < 3)
                        return null;

                    return new WeaponRecord
                    {
                        Name = itemData[0],
                        Type = itemData[1],
                        Slot = itemData[2]
                    };
                })
                .Where(item => item != null)
                .ToList();

        private List<ItemCategoryRecord> ReadSubCategories(
            List<WeaponRecord> weapons,
            List<ArmorReord> armors)
        {
            var armorSubCategoriesRecord = armors
                .Select(armor => new ItemCategoryRecord
                {
                    Name = armor.Name,
                    SubCategory = armor.Slot.ToTitleCase().Replace("\r", string.Empty),
                    Category = "Armor"
                })
                .Where(record => record.SubCategory != string.Empty)
                .ToList();

            var weaponSubCategoriesRecord = weapons
                .Where(weapon => !string.IsNullOrWhiteSpace(weapon.Type))
                .Select(weapon => new ItemCategoryRecord
                {
                    Name = weapon.Name,
                    Category = "Weapon",
                    SubCategory =
                        (weapon.Slot == "1h\r" ? weapon.Type :
                         weapon.Slot == "2h\r" ? $"Two Handed {weapon.Type}" :
                         $"Two And One Handed {weapon.Type}")
                            .ToTitleCase()
                            .Replace("Scep", "Scepter")
                            .Replace("Hamm", "Hammer")
                            .Replace("Swor", "Sword")
                            .Replace("Knif", "Knife")
                            .Replace("Jave", "Javelin")
                            .Replace("Jave", "Jave")
                            .Replace("Spea", "Spear")
                            .Replace("Pole", "Polearm")
                            .Replace("Staf", "Staff")
                            .Replace("Xbow", "Crossbow")
                            .Replace("Tpot", "Throwing Potions")
                            .Replace("Taxe", "Throwing Axe")
                            .Replace("Tkni", "Thorwing knife")
                            .Replace("Abow", "Amazon bow")
                            .Replace("Aspe", "Amazon spear")
                            .Replace("Ajav", "Amazon Javelin")
                            .Replace("H2h2", "Hand To Hand Two Handed")
                            .Replace("H2h", "Hand To Hand")
                            .Replace("Jave", "Jave")
                })
                .ToList();

                weaponSubCategoriesRecord.AddRange(armorSubCategoriesRecord);

                return weaponSubCategoriesRecord;
        }
    }
}