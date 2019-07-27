using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DiabloII.Items.Reader;

namespace DiabloII.Items
{
    class Program
    {
        static void Main(string[] args)
        {
            var datasheetContent = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Files/Uniques2.csv"));
            var diabloIIDatasheetReader = new DiabloIIDatasheetReader();

            diabloIIDatasheetReader.Read(datasheetContent);
        }
    }
}
