using MyWcfConsumer.MyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWcfConsumer
{
    class Program
    {
        // when we make anything async, if it returns void, we make it return Task
        // but if it returns T, we make it return Task<T>.

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (var client = new Service1Client())
            {
                client.Open();

                var version = await client.GetServiceVersionAsync();

                Console.WriteLine(version);

                Console.WriteLine("Enter number: ");
                if (int.TryParse(Console.ReadLine(), out var num))
                {
                    var doubled = await client.DoubleNumberAsync(num);

                    Console.WriteLine($"Doubled: {doubled}");
                }
                else
                {
                    Console.WriteLine("Not an number!");
                }

                Question question = client.GetQuestion(1);
                Console.WriteLine(question.QuestionId);
                // can't access DateModified because not exposed with [DataMember]

                Question question2 = client.GetQuestion(2);
                Console.WriteLine(question2.QuestionId);
            }

            Console.ReadKey();
        }
    }
}
