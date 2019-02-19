using System;
using System.Collections.Generic;
using System.Text;

namespace MoreAnimals.Library
{
    // in C#, we extend classes and implement interfaces with :
    public class Eagle : ABird
    {
        // in C#, by default, overriding inherited members is not allowed!
        // only adding new members
        public override void MakeNoise()
        {
            Console.WriteLine("Caw");
        }

        // attempt to override inherited "GoTo"
        public void GoTo(string location)
        {
            Console.WriteLine($"I'm and eagle, flying to {location}");
        }
    }
}
