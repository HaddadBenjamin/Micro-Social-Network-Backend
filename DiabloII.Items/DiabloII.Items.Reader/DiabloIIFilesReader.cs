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
        // - Trier les attributs : stat requis / damage / armure en premier / le reste trier en mode alphabétique ?
        // to verify : 
        // Il faudra que je mette à jour les énums côté API
        // Les types h2h et 2h2 ne sont pas mapper, il semble manquer des Claw aussi ?
        // Récupérer aussi le niveau de l'objet
        public IEnumerable<Item> Read(
            string uniquesCsv,
            string weaponsCsv,
            string armorsCsv)
        {
            var weapons = ReadWeapons(weaponsCsv);
            var armors = ReadArmors(armorsCsv);
            var subCategories = ReadSubCategories(weapons, armors);
            var uniques = ReadUniques(uniquesCsv, subCategories);

            // For sanitize purpoeses and comparaison :
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
                    var type = itemData[3]
                        .ToTitleCase()
                        .Replace("Ancientarmor", "Ancient Armor")
                        .Replace("Battle Guantlets", "Battle Gauntlets")
                        .Replace("Cedarbow", "Cedar Bow")
                        .Replace("Hunter’S Bow", "Hunter’s Bow")
                        .Replace("Jo Stalf", "Jo Staff")
                        .Replace("2-Handed Sword", "Two-Handed Sword")
                        .Replace("Tresllised Armor", "Trellised Armor");
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
                        Level = itemData[1].ParseIntOrDefault(),
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
                    Name = armor.Name
                        .Replace("\r", string.Empty)
                            .Replace("(M)", "Medium")
                            .Replace("(H)", "Heavy")
                            .Replace("(L)", "Light")
                            .Replace("Hard Leather Armor", "Hard Leather")
                            .Replace("Gaunlets", "Gauntlets")
                            .Replace("Cap/hat", "Cap")
                            .Replace("Skull  Guard", "Skull Guard"),
                    SubCategory = armor.Slot.ToTitleCase(),
                    Category = "Armor"
                })
                .Where(record => record.SubCategory != string.Empty)
                .ToList();

            var weaponSubCategoriesRecord = weapons
                .Where(weapon => !string.IsNullOrWhiteSpace(weapon.Type))
                .Select(weapon => new ItemCategoryRecord
                {
                    Name = weapon.Name
                        .Replace("Bec-de-Corbin", "Bec-De-Corbin")
                        .Replace("Kriss", "Kris")
                        .Replace("Martel de Fer", "Martel De Fer")
                        .Replace("Blood Spirt", "Blood Spirit")
                        .Replace("Hunter's Bow", "Hunter’s Bow")
                        .Replace("MatriarchalJavelin", "Matriarchal Javelin"),
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
                            .Replace("Jave", "Javelin") 
                })
                .ToList();

            weaponSubCategoriesRecord.AddRange(armorSubCategoriesRecord);
            weaponSubCategoriesRecord.AddRange(new[]
            {
                    new ItemCategoryRecord { Name = "Silver-Edged Axe", Category = "Weapon", SubCategory = "Two Handed Axe"},
                    new ItemCategoryRecord { Name = "Amulet", Category = "Jewelry", SubCategory = "Amulet"},
                    new ItemCategoryRecord { Name = "Arrows", Category = "Armor", SubCategory = "Arrows"},
                    new ItemCategoryRecord { Name = "Bolts", Category = "Armor", SubCategory = "Bolts"},
                    new ItemCategoryRecord { Name = "Charm", Category = "Charm", SubCategory = "Charm"},
                    new ItemCategoryRecord { Name = "Hammer", Category = "Weapon", SubCategory = "Hammer"},
                    new ItemCategoryRecord { Name = "Jewel", Category = "Jewelry", SubCategory = "Jewel"},
                    new ItemCategoryRecord { Name = "Ring", Category = "Jewelry", SubCategory = "Ring"},
                    new ItemCategoryRecord { Name = "Staff", Category = "Weapon", SubCategory = "Staff"},
                    new ItemCategoryRecord { Name = "Conqueror Crown", Category = "Armor", SubCategory = "Barbarian Helm"},
                    new ItemCategoryRecord { Name = "Blood Spirit", Category = "Armor", SubCategory = "Druid Helm"},
                    new ItemCategoryRecord { Name = "Bracers", Category = "Armor", SubCategory = "Hands"},
                    new ItemCategoryRecord { Name = "Gloves", Category = "Armor", SubCategory = "Hands"},
                    new ItemCategoryRecord { Name = "Belt", Category = "Armor", SubCategory = "Waist"},
                    new ItemCategoryRecord { Name = "Sash", Category = "Armor", SubCategory = "Waist"},
                    new ItemCategoryRecord { Name = "Girdle", Category = "Armor", SubCategory = "Waist"},
                    new ItemCategoryRecord { Name = "Gauntlets", Category = "Armor", SubCategory = "Hands"},
            });

            return weaponSubCategoriesRecord;
        }
    }
}