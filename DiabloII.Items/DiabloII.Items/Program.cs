using System.IO;
using DiabloII.Items.Reader;
using Newtonsoft.Json;
using System.Linq;

namespace DiabloII.Items
{
    class Program
    {
        static void Main(string[] args)
        {
            var uniquesPath = Path.Combine(Directory.GetCurrentDirectory(), "Files/Uniques.csv");
            var weaponsPath = Path.Combine(Directory.GetCurrentDirectory(), "Files/Weapons.csv");
            var armorsPath = Path.Combine(Directory.GetCurrentDirectory(), "Files/Armors.csv");
            var propertiessPath = Path.Combine(Directory.GetCurrentDirectory(), "Files/Properties.csv");
            var skillsPath = Path.Combine(Directory.GetCurrentDirectory(), "Files/Skills.csv");
			var uniquesDestination = @"C:\Users\hadda\Desktop\Travaux\Projets\Diablo-II-Items\DiabloII.Items\DiabloII.Items.Api\Files\Uniques.json";

			var uniquesContent = File.ReadAllText(uniquesPath);
            var weaponsContent = File.ReadAllText(weaponsPath);
            var armorsContent = File.ReadAllText(armorsPath);
            var propertiesContent = File.ReadAllText(propertiessPath);
            var skillsContent = File.ReadAllText(skillsPath);
			var reader = new DiabloIIFilesReader();

            var uniques = reader.Read(uniquesContent, weaponsContent, armorsContent, propertiesContent, skillsContent);
            var uniquesAsJson = JsonConvert.SerializeObject(uniques, Formatting.Indented);
			var uniqueTests = JsonConvert.SerializeObject(uniques.Where(e => e.SubCategory == "Javelin").Take(10), Formatting.Indented);

			File.WriteAllText(uniquesDestination, uniquesAsJson);
        }
    }
}