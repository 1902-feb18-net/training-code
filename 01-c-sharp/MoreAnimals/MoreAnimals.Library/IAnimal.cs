using System;
using System.Collections.Generic;
using System.Text;

namespace MoreAnimals.Library
{
    public interface IAnimal
    {
        // in an interface, the members must have the same access level
        // as the whole interface, so, we do not say it explicitly.

        // we don't have fields in interfaces, but we do have properties
        // in an interface, this doesn't necessarily mean an auto-implemented property

        int AnimalId { get; set; }
        string Name { get; set; }

        void MakeNoise();
        void GoTo(string location);
    }
}
