using System;
using System.Threading.Tasks;

namespace DiabloII.Items.Generator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Item generation in progres...");

            var generationEnvironments = new[]
            {
                GenerationEnvironment.Development,
            };
            await ItemsGenerator.Generate(generationEnvironments);
            Console.WriteLine("Item generation is done");
        }
    }
}