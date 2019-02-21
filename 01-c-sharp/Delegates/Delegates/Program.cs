using System;

namespace Delegates
{
    class Program
    {
        static void Main(string[] args)
        {
            var moviePlayer = new MoviePlayer
            {
                CurrentMovie = Movie.StarWars4
            };

            MoviePlayer.MovieFinishedHandler handler = EjectDisc;

            // subscribe to an event
            moviePlayer.MovieFinished += handler;

            // unsubscribe from an event
            //moviePlayer.MovieFinished -= handler;

            moviePlayer.MovieFinished += EjectDisc;

            moviePlayer.MovieFinished += () =>
            {
                //for (int i = 0; i < length; i++)
                //{

                //}
                //if ()
                //    if ()

                Console.WriteLine("handle event with block-body lambda.");
            };

            // with expression body, you can only put one line in
            moviePlayer.MovieFinished += () => Console.WriteLine("expression body");

            // we can specify type on lambda function parameters...
            // but usually, they are inferred from context (like "var" does).
            //moviePlayer.DiscEjected += (string s) => Console.WriteLine($"Ejecting {s}");

            moviePlayer.DiscEjected += s => Console.WriteLine($"Ejecting {s}");

            FuncAndAction();

            Console.WriteLine("Playing movie...");

            moviePlayer.Play();

            Console.ReadLine(); // wait for me to press enter before exiting
        }

        private static void FuncAndAction()
        {
            Func<string, string, int> func = (s1, s2) => s1.Length + s2.Length;
            Action<string, string, int> action = (s1, s2, i) => Console.WriteLine(s1 + s2 + i);
        }

        public static void EjectDisc()
        {
            Console.WriteLine("Ejecting disc.");
        }
    }
}
