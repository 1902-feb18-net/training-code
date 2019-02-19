using System;

namespace Animals.UI
{
    internal class Dog
    {
        // field
        internal string Noise = "Woof!";

        // methods
        internal void GoTo(string location)
        {
            // simple way to put a string together
            // Console.WriteLine("Walking to " + location);

            // string interpolation syntax
            // $ in front turns braces into "switch back to C#"
            Console.WriteLine($"Walking to {location}");
        }

        internal void MakeNoise()
        {
            Console.WriteLine(Noise);
        }

        // access modifiers
        // in C#, every class/interface/etc has some access modifier
        // and every class/etc _member_ also has some access modifier

        // public: everyone can access this member.
        // (nothing): same as private
        // private: only the current class can access this member.
        // internal: anything in the same assembly (project) can access,
        //      but nothing from outside the assembly.

        // there's no such thing as a private class
        // on classes, interfaces, etc. we have public and internal.
        // (the default for them is internal.)
    }
}
