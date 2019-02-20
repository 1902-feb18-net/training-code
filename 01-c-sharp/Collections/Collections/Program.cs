using System;
using System.Collections;
using System.Collections.Generic;

namespace Collection
{
    class Program
    {
        static void Main(string[] args)
        {
            Arrays();
            Lists();
            Sets();
            StringEquality();

            // couple other collections:
            new Stack<int>(); // last-in, first-out
            new Queue<int>(); // first-in, first-out
        }

        static void Arrays()
        {
            // we can make fixed-length lists of things, arrays
            int[] ints = new int[5];
            // we can also use array initializer syntax
            int[] ints2 = new int[] { 1, 2, 3, 9, 50 };

            // we can go through arrays with for loop, or foreach loop 
            // if we have no need of the index.

            // we can have arrays of any type, even other arrays

            int[][] twoDArray = new int[9][];
            twoDArray[0] = new int[4];
            twoDArray[1] = new int[4];
            // etc.
            // a 9 by 4 two-d array
            // this is called a "jagged array"
            // (each row could have different length if we wanted)

            // c# has multidimensional array
            int[,] multiDArray = new int[5, 5];
            // 5 by 5 multi-d array
            multiDArray[0, 0] = 8;
            int[,,,] fourDArray = new int[5, 5, 4, 2]; // four D array
            // comma instead of extra brackets

            //
            int[,][] crazyThing = new int[2, 2][];

            // we rarely have any use for arrays.
            // for performance is really the only reason.

            // in practice we use other objects.
        }

        static void Lists()
        {
            // use .Add, or, just initialization syntax for the initial contents.
            var list = new ArrayList { 5, 8, 1 };
            list.AddRange(new int[] { 4, 5, 6, 7, 8 });
            list.Remove(8);
            list.Add("asdf");

            for (int i = 0; i < list.Count; i++)
            {
                // in C#, we can index into the list just as if it were an array.
                Console.WriteLine(list[i]);
                //list[i] /= 2;
            }

            //foreach (var item in list)
            //{

            //}

            // early in C#'s history, we got generics, and stopped using ArrayList as well

            var genericList = new List<int> { 1, 2, 3 };
            // this list doesn't upcast everything to object, it only allows ints.
            //genericList.Add("abc"); // not allowed, this list instance is tied to int type.

            foreach (var item in genericList)
            {
                Console.WriteLine(item * 2); // works because we know this is an int
            }
        }

        static void Sets()
        {
            var set = new HashSet<string>();
            set.Add("abc");
            set.Add("abc"); // this line does effectively nothing
            set.Add("abcdef");
            // we take our idea of sets from mathematics
            // a set has no concept of duplicates, something is either in it or not.
            // a set also has no concept of order
            Console.WriteLine(set.Count); // return 2

            // sets are useful when we aren't interesting in storing any order,
            // the main thing we want to do is later on
            // check if some thing is or is not inside the set.

            // checking membership in the set is very fast.
            var list = new List<int> { 1, 2, 2, 2, 3 };
            var withoutDupes = new List<int>(new HashSet<int>(list));
        }

        static void Maps()
        {
            var dictionary = new Dictionary<string, string>();
            dictionary["classroom"] = "room where classes are held.";

            var grades = new Dictionary<string, double>();
            // we also have an initializer syntax for dictionaries.
            grades["Nick"] = 80;
            // helpful members: Keys, Values, ContainsKey, ContainsValue, TryGetValue

            foreach (KeyValuePair<string, double> item in grades)
            {
                //item.Key;
                //item.Value;
            }

            // dictionary objects let you use any type you want to index into it
            // and any type to use for the value stored for that key.


        }

        static void StringEquality()
        {
            string a = "asdf";
            string b = "asdf";
            Console.WriteLine(a == b); // returns true

            // value types and reference types.
            // value type variables store their values directly.
            // reference type variables store a reference to their value.

            // in C#, many of our basic types are value types:
            // int, double, bool, float, long

            int n1 = 5;
            int n2 = n1; // int is value type, so n2 is a copy of n1

            var dummy1 = new Dummy();
            var dummy2 = dummy1;

            dummy1.Data = 10;
            if (dummy2.Data == 10)
            {
                Console.WriteLine("reference type");
            }
            else
            {
                Console.WriteLine("value type");
            }
            // Dummy is a reference type, so dummy2 is a copy of the reference,
            // i.e. a new reference to the same object.

            // objects made from classes are reference types, always
            // objects made from structs are value types.
            // all the built-in value types are "structs" in C#.


            var vDummy1 = new ValueDummy();
            var vDummy2 = vDummy1;

            vDummy1.Data = 10;
            if (vDummy2.Data == 10)
            {
                Console.WriteLine("reference type");
            }
            else
            {
                Console.WriteLine("value type");
            }

            // structs are copied entirely every time we pass it to a new method
            // or assign it to a new variable.
            // value types are deleted from memory as soon as the one variable that contains
            // them passes out of scope.

            // reference types, we get a new copy of a reference,
            // but to the same underlying object.
            // reference types need to be "garbage collected" because we don't know right away
            // when the LAST variable pointing to it has passed out of scope.

            // in C# we have the idea of "managed" vs "unmanaged" code -
            // in unmanaged code, you have to manually write the code to
            // delete reference type objects from memory, source of many bugs.

            // in managed code, there is garbage collection the runs periodically
            // to search for objects that are unreachable by any running part of the code.

            // our tradeoff is, the computer should work harder so the developer can solve
            // real problems

            // back to strings......

            // NORMALLY "==" compares value types by value, and reference types by reference.

            Console.WriteLine(new Dummy() == new Dummy()); // false... reference types.
            // for value types like structs, they don't have to be the same object
            // just have the same values.

            // BUT we make an exception for strings because it's awkward to have to
            // do string.Equals() for comparing strings.

            // in C#, all value types do derive ultimately from object.
            // so, we can always upcast them to object variables.

            int i1 = 5;
            object o2 = i1; // implicit upcasting
            // this is called "boxing" - the int is wrapped inside a reference type
            // to give that value reference type semantics
            // "unboxing"... the reverse, with downcasting.
            int i2 = (int)o2; // extract that value from inside the object wrapper.

            i1 = 8;
            Console.WriteLine((int)o2); // should print 5

            // java has awkward Integer vs int distinctions
            // we have this as boxing and unboxing with object.

            // sometimes we want to see if two objects have the same values in them
            // for that, we make use of .Equals
        }
    }

    class Dummy
    {
        public int Data { get; set; }
    }

    struct ValueDummy {
        public int Data { get; set; }
    }
}
