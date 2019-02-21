using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

            // we WOULD write this...
            //persons = await DeserializeXMLFromFileAsync(fileName);
            // except Main can't be async, so, we will synchronously wait on the results.
            persons = DeserializeXMLFromFileAsync(fileName).Result;

            persons.Add(new Person { Id = persons.Max(p => p.Id) + 1 });

            SerializeAsXMLToFile(fileName, persons);

            // we could serialize in JSON format instead of XML...
            // DataContractSerializer (built-in to .NET)
            // JSON.NET (aka Newtonsoft JSON) (third-party)

            string jsonFile = @"C:\revature\persons_data.json";

            string data = File.ReadAllTextAsync(jsonFile).Result;
            persons = JsonConvert.DeserializeObject<List<Person>>(data);

            persons.Add(new Person { Id = persons.Max(p => p.Id) + 1 });
            string newData = JsonConvert.SerializeObject(persons);
            File.WriteAllTextAsync(jsonFile, newData).Wait();
            // Wait() and .Result both wait synchronously
        }

        // async code requires async tests

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

        // when we make code async....
        // the method has to have the "async" modifier
        // the method needs to return a Task (for void-return) or
        // a Task<Something> if we wanted to return Something.
        // the method should say Async at the end of its name (for self-documenting purposes)
        // when we call async methods in our own methods, we need to "await" the tasks they
        // give us.
        private static async Task<List<Person>> DeserializeXMLFromFileAsync(string fileName)
        {
            var serializer = new XmlSerializer(typeof(List<Person>));
            // in addition to those XmlBlahBlah attributes, we can also customize
            // the format on the serializer object itself.

            // we're going to use "using statement", not to be confused
            // with "using directive" at the top of the file.

            // in place of boilerplate code with IDisposable "try finally dispose"

            using (var memoryStream = new MemoryStream())
            {
                using (var fileStream = new FileStream(fileName, FileMode.Open))
                {
                    // copy the filestream into the memorystream
                    //Task copying = fileStream.CopyToAsync(memoryStream);
                    //await copying;
                    await fileStream.CopyToAsync(memoryStream);
                    // when the method executing reaches an await statement,
                    // it allows other code to run in the meantime.

                    // the objects you're using need to support async, or you aren't
                    // able to use it
                    // XmlSerializer.DeserializeAsync doesn't exist, or else,
                    // we wouldn't need the memoryStream
                }
                // using statement automatically disposes the resource when we exit it

                // reset "cursor" of stream to beginning to read its contents
                memoryStream.Position = 0;

                return (List<Person>)serializer.Deserialize(memoryStream);
                // should be try-catching throughout this method
            }
        }
    }
}
