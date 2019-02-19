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

            // exception handling
            try
            {
                Console.WriteLine("What should the dog say? ");
                string input = Console.ReadLine();
                if (input == "null") // just so we can test an argumentnullexception
                {
                    input = null;
                }
                dog.SetNoise(input);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("caught ArgumentNullException! using fallback value");
                dog.SetNoise("woof");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("caught ArgumentException! using fallback value");
                dog.SetNoise("woof");
            }
            catch (Exception e)
            {
                // would catch ANY exception at all
                // this is sloppy... UNLESS....
                Console.WriteLine("write log of exception");
                //throw; // re-throws the exception
                throw e;

                // throw vs throw e
                // using "throw" by itself leaves the stack trace of the exception unchanged.
                // using "throw e" overwrites its stack trace with the current line

                // BUT in reality... implementations of this differ and sometimes the stack trace
                // doesn't follow that specification
                //throw new Exception("error message", e);
            }
            //catch
            //{
            //    // this does the same thing
            //}
            finally
            {
                // finally block contains code that runs after the try / catch code,
                // whether or not there was an exception handled or unhandled.

                // here is where we release resources that should be released regardless of
                // error or success
            }
            // why not just put that code here?
            // (close the file)

            Console.WriteLine(dog.GetNoise());
        }
    }
}
