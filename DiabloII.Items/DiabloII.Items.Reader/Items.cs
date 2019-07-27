using System.Collections.Generic;

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
        Weapon,
        Armor,
        Jewelry,
        Charm
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

    }

    public class DiabloIIDatasheetReader
    {
        public IEnumerable<Item> Read()
        {

        }
    }
}
