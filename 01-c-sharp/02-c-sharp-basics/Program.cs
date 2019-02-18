using System;
using System.Collections.Generic;

// generally we like our namespaces to match our folder structure
// with dots for subfolders
namespace _02_c_sharp_basics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello C#!");

            // variables
            // variables in C# are locked down to one data type
            // here we declare and initialize the variable in one line.
            string myString = ".NET";

            // here, declaring...
            string myString2;
            // and then initializing.
            myString2 = ".NET";

            Console.WriteLine(myString2);

            // basic data types
            int integer; // for integers (32-bit)
            double numberWithDecimals; // floating-point number (64-bit)
            string s; // string of characters
            bool trueOrFalse; // true or false

            // basic control structures
            if (1 == 3)
            {
                // this won't run, condition is false
                Console.WriteLine("1 is equal to 3");
            }
            else if (1 == 2)
            {
                // this also won't run
                Console.WriteLine("1 is equal to 2");
            }
            else
            {
                // this will run because neither of those were true.
                Console.WriteLine("1 isn't equal to either of those.");
            }

            // loops

            int max = 10;
            for (int i = 0; i < max; i++)
            {
                // some implicit type conversion/casting
                Console.Write(i + " ");
            }
            Console.WriteLine();

            // while loops
            int number = 4;
            while (number > 0)
            {
                number -= 10;
                Console.WriteLine(number);
            }
            // also, do-while

            // also, switch statement
            switch (number)
            {
                case 3:
                    number = 4;
                    break;
                case 4:
                    number = 3;
                    break;
                default: // this one will run, because number is -6
                    Console.WriteLine("unexpected condition");
                    break;
            }
            // "format document" shortcut in VS code.
            // Alt+Shift+F.

            // "//" for comments in C#

            /*
            multiline comments like this
            */

            // editor shortcut: Ctrl+/ will toggle comment/uncomment
            // of current line or selected lines

            // call functions
            PrintStuff("string sent from Main function");

            // functions can have return values and
            // we can pass those values to other functions
            PrintStuff(Reverse("Nick Escalona"));

            // we have compile-time type inference for variables
            var data = "asdf";
            // we can declare variables and have the type
            // decided based upon what we give it as initial value.

            // we can't have the compiler predict the future,
            // it really does just copy the type of whatever the right-hand-side is.
            // (this is an error)
            // var data2;

            // we use var when the type is obvious from context and/or obnoxiously long.
            // for example:
            var myList = new List<List<List<string>>>();
            // if we just type e.g. "List", and the right namespace isn't already imported,
            // then Ctrl+. will give us the right "using statement" for the top of the file.

            // in C# apart from the basic types we spend most of our time with objects
            // that we create with "new" keyword and constructor.

            // e.g. here is the current class we are in,
            // making an instance of that object.
            var program = new Program();
        }

        // separate functions/methods

        // first we have some modifiers ("static" in this case)
        // second we have return type ("void" in this case - return nothing)
        // third we have name of function
        // last we have parameter list.
        static void PrintStuff(string stuff)
        {
            Console.WriteLine(stuff);
        }

        static string Reverse(string s)
        {
            string result = "";
            foreach (char ch in s) // for each character in the string...
            {
                // prepend the character to the result string
                result = ch + result;
            }
            return result;
        }
    }
}
