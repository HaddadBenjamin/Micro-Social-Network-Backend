using System;

namespace DiabloII.Items.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Item generation in progres...");

            var generationEnvironments = new[]
            {
                GenerationEnvironment.Development,
            };
            ItemsGenerator.Generate(generationEnvironments);
            Console.WriteLine("Item generation is done");
        }
    }
}