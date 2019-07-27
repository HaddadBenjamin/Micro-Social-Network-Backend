using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DiabloII.Items.Reader;
using Newtonsoft.Json;

namespace DiabloII.Items
{
    class Program
    {
        static void Main(string[] args)
        {
            var datasheetContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Files/Uniques.csv"));
            var diabloIIDatasheetReader = new DiabloIIDatasheetReader();

            var r = diabloIIDatasheetReader.Read(datasheetContent);

            string json = JsonConvert.SerializeObject(r);
        }
    }
}
