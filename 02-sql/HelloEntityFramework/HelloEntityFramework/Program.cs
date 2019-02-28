using HelloEntityFramework.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;

namespace HelloEntityFramework
{
    class Program
    {
        // EF database-first approach steps:
        //
        // 1. have startup project, and data access library project.
        // 2. reference data access from startup project.
        // 3. add NuGet packages to the startup project:
        //     - Microsoft.EntityFrameworkCore.Tools
        //     - Microsoft.EntityFrameworkCore.SqlServer
        //    and to the data access project:
        //     - Microsoft.EntityFrameworkCore.SqlServer
        // 4. open Package Manager Console in VS
        //     ( View -> Other Windows -> Package Manager Console
        // 5. run command (your solution needs to be able to compile) (one-line):
        //      Scaffold-DbContext "<your-connection-string>"
        //             Microsoft.EntityFrameworkCore.SqlServer
        //             -Project <name-of-data-project> -Force
        // (alternate 4/5. run in git bash/terminal:
        //      dotnet ef dbcontext scaffold "<your-connection-string>"
        //               Microsoft.EntityFrameworkCore.SqlServer
        //               --project <name-of-data-project> -Force
        // 6. delete the OnConfiguring override in the DbContext, to prevent
        //       committing your connection string to git.

        // (7. any time we change the database (add a new column, etc.), go to step 4.)


        // by default, the scaffolding will configure the models in OnModelCreating
        //     with the fluent API. this is the right way to do it - strongest separation
        //     of concerns, more flexibility.
        //   if we scaffold with option "-DataAnnotations" we'll put the configuration
        //   on the Movie and Genre classes themselves with attributes.
        //   third "way to configure" is convention-based
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MoviesContext>();
            optionsBuilder.UseSqlServer(SecretConfiguration.ConnectionString);
            var options = optionsBuilder.Options;

            using (var dbContext = new MoviesContext(options))
            {
                // lots of complex setup... here is where the payoff begins
                PrintMovies(dbContext);
            }

            Console.ReadLine();
        }

        static void PrintMovies(MoviesContext dbContext)
        {
            foreach (var movie in dbContext.Movie)
            {
                Console.WriteLine($"Movie #{movie.MovieId}: {movie.Title}" +
                    $" ({movie.ReleaseDate.Year})");
            }
        }
    }
}
