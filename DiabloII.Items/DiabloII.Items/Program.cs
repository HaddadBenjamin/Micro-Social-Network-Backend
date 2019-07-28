using System.IO;
using DiabloII.Items.Reader;
using Newtonsoft.Json;

namespace DiabloII.Items
{
    class Program
    {
        static void Main(string[] args)
        {
            var datasheetPath = Path.Combine(Directory.GetCurrentDirectory(), "Files/Uniques.csv");
            var jsonDestination = datasheetPath.Replace("csv", "json");
            var uniqueDatasheetPath = File.ReadAllText(datasheetPath);
            var diabloIIDatasheetReader = new DiabloIIDatasheetReader();

            var items = diabloIIDatasheetReader.Read(uniqueDatasheetPath);
            var itemsAsJson = JsonConvert.SerializeObject(items);

            File.WriteAllText(jsonDestination, itemsAsJson);
        }
    }
}