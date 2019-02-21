using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SerializationAndAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            var persons = new List<Person>
            {
                new Person
                {
                    Id = 1,
                    Name = "Nick",
                    Address = new Address
                    {
                        Street = "123 Main St",
                        City = "Fort Worth",
                        State = "TX"
                    }
                },
                new Person
                {
                    Id = 2,
                    Name = "Fred",
                    Address = new Address
                    {
                        Street = "123 Main St",
                        City = "Reston",
                        State = "VA"
                    }
                }
            };

            // to send this over network or to disk, we need to serialize it.
            // meaning, collecting data from across memory locations
            // into a well-defined text or binary format.
            // ideally this is reversible, we can deserialize the data back from
            // its format into memory (maybe on the other end of the network connection.)

            // normally, \ (backslash) is used as an escape character in string literals.
            // so, this string has a new line character:
            string newline = "\n";
            // when we want to treat backslashes literally, we have @ strings...
            string fileName = @"C:\revature\persons_data.xml";

            SerializeAsXMLToFile(fileName, persons);
        }

        private static void SerializeAsXMLToFile(string fileName, List<Person> persons)
        {
            // our first object, XmlSerializer
            // unfortunately does not know about generics
            var serializer = new XmlSerializer(typeof(List<Person>));

            // create mode, to overwrite file if already exists.
            FileStream fileStream = null;

            try
            {
                fileStream = new FileStream(fileName, FileMode.Create);

                serializer.Serialize(fileStream, persons);
            }
            catch (IOException ex)
            {
                Console.WriteLine("error in writing to file:");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                fileStream?.Dispose(); // all IDisposable have Dispose method.
            }
        }
    }
}
