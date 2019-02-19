using System;

namespace Animals.Library
{
    public class Dog
    {
        // field
        internal string Noise = "Woof!";

        // getter
        public string GetNoise()
        {
            return Noise + "!";
        }

        // setter
        public void SetNoise(string newValue)
        {
            if (newValue == null)
            {
                // throwing an exception
                throw new ArgumentNullException("newValue");
            }
            if (newValue.Length == 0)
            {
                // throwing an exception
                throw new ArgumentException("value must not be empty", "newValue");
            }
            Noise = newValue;
        }

        // instead of using getters and setters,
        // in C# we have properties where other languages would just
        // use fields on their own.

        // simplest property is "auto-implemented" property.
        // a field is generated behind the
        // scenes to back this property
        // usually properties have some "backing field".
        public int Id { get; set; } = 0;
        // this here is manual version of
        // auto-property.
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                // inside "set"
                // we have implicit argument "value"
                // could do null/empty-checks, etc.
                _name = value;
            }
        }
        // property syntax provides getters and setters pretending
        // to be a field

        // we can have properties without set
        // (readonly)
        public string Color { get; } = "brown";
        public string Breed { get; set; }

        // methods
        public void GoTo(string location)
        {
            // simple way to put a string together
            // Console.WriteLine("Walking to " + location);

            // string interpolation syntax
            // $ in front turns braces into "switch back to C#"
            Console.WriteLine($"Walking to {location}");
        }

        public void MakeNoise()
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
