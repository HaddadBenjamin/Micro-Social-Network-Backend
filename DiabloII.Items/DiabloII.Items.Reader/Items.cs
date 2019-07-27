using System.Collections.Generic;
using System.Linq;

namespace DiabloII.Items.Reader
{
    public class Item
    {
        public string Name { get; set; }
        public int LevelRequired { get; set; }
        public ItemQuality Quality { get; set; }
        public ItemCategory Category { get; set; }
        public ItemSubCategory SubCategory { get; set; }
        public IEnumerable<ItemProperty> Properties { get; set; }
        public string Type
        {
            set
            {
                Category =
                   new[]
                   {
                        ItemSubCategory.Armor,
                        ItemSubCategory.Glove,
                        ItemSubCategory.Belt,
                        ItemSubCategory.Boot,
                        ItemSubCategory.Shield,
                        ItemSubCategory.Helm
                   }.Contains(SubCategory) ? ItemCategory.Armor :
                   new[]
                   {
                        ItemSubCategory.Bow,
                        ItemSubCategory.Crossbow,
                        ItemSubCategory.Staff,
                        ItemSubCategory.Wand,
                        ItemSubCategory.Sword,
                        ItemSubCategory.Dagger,
                        ItemSubCategory.Axe,
                        ItemSubCategory.AxeWeapon, // to verify
                        ItemSubCategory.Lance,
                        ItemSubCategory.Masse,
                        ItemSubCategory.Scepter,
                        ItemSubCategory.ThrowWeapon,
                   }.Contains(SubCategory) ? ItemCategory.Weapon :
                   new[]
                   {
                        ItemSubCategory.Ring,
                        ItemSubCategory.Amulet,
                        ItemSubCategory.Jewel,
                   }.Contains(SubCategory) ? ItemCategory.Jewelry :
                   SubCategory == ItemSubCategory.Charm ? ItemCategory.Charm :
               ItemCategory.UNKNOWN;
            }
        }
    }

    public enum ItemQuality
    {
        Normal,
        Magical,
        Rare,
        Unique,
        Set,
        Crafted
    }

    public enum ItemCategory
    {
        UNKNOWN,
        Weapon,
        Armor,
        Jewelry,
        Charm,
    }

    public class ItemProperty
    {
        public string Name { get; set; }
        public int Par { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }
    }

    public enum ItemSubCategory
    {
        UNKNOWN,
        // Armor
        Armor,
        Glove,
        Belt,
        Boot,
        Shield,
        Helm,
        // Weapon
        Bow,
        Crossbow,
        Staff,
        Wand,
        Sword,
        Dagger,
        Axe,
        AxeWeapon, // to verify
        Lance,
        Masse,
        Scepter,
        ThrowWeapon,
        // Jewelry
        Ring,
        Amulet,
        Jewel,
        // Charm
        Charm
        // google it
    }

    public class DiabloIIDatasheetReader
    {
        public IEnumerable<Item> Read(string datasheetCsv)
        {
            // Mapper types vers un subCategory
            // Maper subCategory vers une category
            var types = datasheetCsv
                .Split('\n')
                .Skip(1)
                .Select(line => string.IsNullOrEmpty(line) ? string.Empty : line.Split(';')[3])
                .ToList()
                .Distinct();


            return datasheetCsv
                .Split('\n')
                .Skip(1)
                .Select(line =>
                {
                    var a = line.Split(';');
                    var type = string.IsNullOrEmpty(line) ? string.Empty : line.Split(';')[3];
                    return new Item
                    {
                        Name = a[0],
                        LevelRequired = int.Parse(a[2]),
                        Type = type
                    };
                })
                .ToList();
        }
    }
}
