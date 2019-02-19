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
        // this is the bad way, actually just hiding the other implementation with a second one.
        //public void GoTo(string location)
        //{
        //    Console.WriteLine($"I'm and eagle, flying to {location}");
        //}
        // we shouldn't use hiding, but if we do, we should use the "new" extended modifier

        // override is the counterpart to virtual.
        // override goes on the child class.
        public override void GoTo(string location)
        {
            Console.WriteLine($"I'm and eagle, flying to {location}");
        }
    }
}
