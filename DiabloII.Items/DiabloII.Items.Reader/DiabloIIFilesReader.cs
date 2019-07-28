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

        // - Mapper types to a SubCategory
        // - Add those data in a database : 1) front ask items for a type. 2) back service is call that ask the db those items. 3) it will be faster than generate all items each time.
        // - Trier les attributs : stat requis / damage / armure en premier / le reste trier en mode alphabétique ?
        // to verify : 
        // Il faudra que je mette à jour les énums côté API
        public IEnumerable<Item> Read(
            string uniquesCsv, 
            string weaponsCsv, 
            string armorsCsv)
        {
            var weapons = ReadWeapons(weaponsCsv);
            var armors = ReadArmors(armorsCsv);

            var armorCategories = armors
                .Select(armor => armor.Slot)
                .Distinct()
                .ToList();

            var weaponCategories = weapons
                .Select(weapon =>
                    weapon.Slot == "1h" ? weapon.Type :
                    weapon.Slot == "2h" ? $"Two Handed {weapon.Type}" :
                    $"Two And One Handed {weapon.Type}"
                )
                .Distinct()
                .ToList();


            // generate items sub category
            return ReadUniques(uniquesCsv, weapons, armors);
        }

        public IEnumerable<Item> ReadUniques(
            string uniquesCsv,
            List<WeaponRecord> weapons,
            List<ArmorReord> armors)
            => uniquesCsv
                .Split('\n')
                .Skip(1)
                .Select(line =>
                {
                    var itemData = line.Split(';');

                    if (itemData.Length < 4)
                        return null;

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
                        Quality = "Unique",
                        Properties = properties
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
    }
}