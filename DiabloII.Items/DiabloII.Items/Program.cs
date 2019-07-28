using System.IO;
using DiabloII.Items.Reader;
using Newtonsoft.Json;

namespace DiabloII.Items
{
    class Program
    {
        static void Main(string[] args)
        {
            var uniquesPath = Path.Combine(Directory.GetCurrentDirectory(), "Files/Uniques.csv");
            var weaponsPath = Path.Combine(Directory.GetCurrentDirectory(), "Files/Weapons.csv");
            var armorsPath = Path.Combine(Directory.GetCurrentDirectory(), "Files/Armors.csv");
            var uniquesDestination = uniquesPath.Replace("csv", "json");
            var uniquesContent = File.ReadAllText(uniquesPath);
            var weaponsContent = File.ReadAllText(weaponsPath);
            var ArmorsContent = File.ReadAllText(armorsPath);
            var reader = new DiabloIIFilesReader();

            var uniques = reader.Read(uniquesContent, weaponsContent, ArmorsContent);
            var uniquesAsJson = JsonConvert.SerializeObject(uniques);

            File.WriteAllText(uniquesDestination, uniquesAsJson);
        }
    }
}