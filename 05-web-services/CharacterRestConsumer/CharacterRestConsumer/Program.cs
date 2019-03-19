using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CharacterRestConsumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var url = "https://localhost:44345/api/character";
            using (var httpClient = new HttpClient())
            {
                // get all characters
                await PrintCharsAsync(url, httpClient);
                // add a character
                await AddCharacterAsync(url, httpClient);
                // get all characters
                await PrintCharsAsync(url, httpClient);

                Console.ReadLine();
            }
        }

        static async Task AddCharacterAsync(string url, HttpClient httpClient)
        {
            var character = new Character { Id = 3, Name = "Bill" };

            var json = JsonConvert.SerializeObject(character);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(url, content);
            // throw an exception if status code indicates failure
            response.EnsureSuccessStatusCode();
        }

        static async Task PrintCharsAsync(string url, HttpClient httpClient)
        {
            // await the headers of the response
            HttpResponseMessage response = await httpClient.GetAsync(url);
            // throw an exception if status code indicates failure
            response.EnsureSuccessStatusCode();
            // await the whole body of the response
            string responseText = await response.Content.ReadAsStringAsync();
            // deserialize the body
            var characters = JsonConvert.DeserializeObject<List<Character>>(responseText);

            foreach (var item in characters)
            {
                Console.WriteLine($"ID {item.Id}, name {item.Name}");
            }
            Console.WriteLine();
        }
    }
}
