using System;
using Animals.Library;

namespace Animals.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var dog = new Dog();
            dog.Name = "Fido";
            Console.WriteLine($"Dog's name is {dog.Name}");
            dog.GoTo("door");
            dog.MakeNoise();
        }
    }
}
