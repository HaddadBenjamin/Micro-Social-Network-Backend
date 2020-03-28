using System;

namespace DiabloII.Items.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Item generation in progres...");

            ItemsGenerator.Generate();

            Console.WriteLine("Item generation is done");
        }
    }
}