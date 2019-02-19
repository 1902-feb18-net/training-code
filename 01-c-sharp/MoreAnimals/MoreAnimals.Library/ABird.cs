using System;
using System.Collections.Generic;
using System.Text;

namespace MoreAnimals.Library
{
    // an abstract class is like a mix of class and interface
    // we can provide some implementations, while leaving other things unimplmented.
    public abstract class ABird : IAnimal
    {
        // exercise: implement class Bird for interface IAnimal

        // "expression body" methods/properties
        // in contrast to "block body"
        // string GetNoise() => "noise";
        // string GetNoise() { return "noise"; }

        public int AnimalId { get; set; }
        public string Name { get; set; }

        // virtual is an extended modifier
        // meaning "opt-in to derived classes being allowed to override it"
        public virtual void GoTo(string location)
        {
            Console.WriteLine($"Flying to {location.ToLower()}");
        }

        // like in an interface, we don't have to implement this one,
        // it's "abstract", the sub-classes of this class will have to implement it.
        public abstract void MakeNoise();

        // because we don't have all implementations, it's impossible to
        // construct a object from an abstract class.
    }
}
