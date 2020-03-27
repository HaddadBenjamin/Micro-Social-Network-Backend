using System.IO;
using DiabloII.Items.Reader;
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
            var uniqueItemsAsJson = JsonConvert.SerializeObject(uniqueItems, Formatting.Indented);

            File.WriteAllText(uniqueItemDestinationPath, uniqueItemsAsJson);
        }
    }
}