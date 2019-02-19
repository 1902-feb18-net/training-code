using MoreAnimals.Library;
using System;

namespace MoreAnimals.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Dog fido1 = new Dog();
            fido1.AnimalId = 1;
            fido1.Name = "Fido";
            fido1.Breed = "Doberman";

            // C# has "property initializer" syntax.
            Dog fido2 = new Dog
            {
                AnimalId = 1,
                Name = "Fido",
                Breed = "Doberman"
            };

            fido1.GoTo("park");
            fido1.MakeNoise();

            // IAnimal is a parent type of Dog
            // Dog is a subtype of IAnimal
            IAnimal animal = fido1;
            // converting from Dog variable to IAnimal variable is "upcasting"
            // upcasting is guaranteed to succeed, so it's implicit

            // Ctrl+K, Ctrl+C to comment lines
            // Ctrl+K, Ctrl+U to uncomment lines
            // when the Dog object is contained in a IAnimal variable,
            // we can't see the Dog-specific stuff anymore.
            //animal.Breed = ""; // error

            // converting the OTHER way, from IAnimal down to Dog, is "downcasting"
            // NOT guaranteed to succeed, so it must be explicit with () casting
            //Bird bird = (Bird)animal;
            Dog dog3 = (Dog)animal;

            // not all casting is upcasting or downcasting, e.g. int to double and back
            // double to int loses dats, "unsafe", so, it must be explicit.
            int integer = (int)3.4;
            // int to double cannot lose data, "safe", so, we can do that with implicit cast.
            double num = integer;

            var animals = new IAnimal[2];
            animals[0] = fido1;
            animals[1] = new Eagle
            {
                AnimalId = 3,
                Name = "Bill"
            };

            // a class can implement as many interfaces as it likes
            // but, a class may only have one direct parent class

            // this code doesn't care how the members are implemented,
            // only that they can do the job specified by the interface.
            foreach (IAnimal item in animals)
            {
                Console.WriteLine(item.Name);
                item.MakeNoise();
                item.GoTo("park"); // here, when we weren't using vrtiaul/override,
                                   // we can't see Eagle.GoTo, which only hides ABird.GoTo
                                   // without truly overriding it.

                // once we use virtual/override, it really does replace the method implementation
                // on the object itself
            }

            Eagle eagle1 = (Eagle)animals[1];
            eagle1.GoTo("park");

            MakeNoise(dog3); // upcasting here

            // we use camelCase for local variables and private fields
            // TitleCase aka PascalCase for everything else
        }

        static void MakeNoise(IAnimal animal)
        {
            animal.MakeNoise();
        }
    }
}
