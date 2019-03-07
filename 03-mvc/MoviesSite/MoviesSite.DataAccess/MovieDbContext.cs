using Microsoft.EntityFrameworkCore;
using MoviesSite.BLL;
using System;

namespace MoviesSite.DataAccess
{
    // three ways to configure EF:
    //   1. conventions (e.g. property named "Id" is primary key)
    //   2. fluent API (DbContext.OnModelCreating) (preferred way, keep entity classes
    //         clean, most powerful)
    //   3. DataAnnotations attributes on entity class properties (e.g. [Key]).

    // steps for code-first EF:
    // 1. have separate data access class library project.
    // 2. add NuGet package Microsoft.EntityFrameworkCore.SqlServer
    //      to both MVC app and data access library. also 
    //        Microsoft.EntityFrameworkCore.Tools on the data access library.
    // 3. implement your context class (inheriting from DbContext).
    //    a. need constructor receiving DbContextOptions and zero-param constructor
    //    b. need DbSets
    //    c. need OnModelCreating
    // 4. register the DbContext in Startup.ConfigureServices (using
    //      connection string from user secrets)
    // 5. add migration
    //     (just like with the db-first scaffolding, there is "Package Manager Console" way
    //     and dotnet CLI ("dotnet ef") way also.
    //      both require nuget package Microsoft.EntityFrameworkCore.Tools (.DotNet?)
    //     Add-Migration (with "project" dropdown in Pack. Man. Console set to data access)
    // 6. update database (Update-Database).
    // 7. every time we want to change data model, we DON'T write SQL, we change the C#
    //      classes, and goto step 5.
    public class MovieDbContext : DbContext
    {
        public MovieDbContext()
        { }

        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        { }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<Genre> Genre { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(builder =>
            {
                builder.HasKey(m => m.Id);

                builder.Property(m => m.Id)
                       .UseSqlServerIdentityColumn();

                builder.Property(m => m.Title)
                       .IsRequired()  // (column will be NOT NULL)
                       .HasMaxLength(128); // (column will be NVARCHAR(128)

                builder.Property(m => m.DateReleased)
                       .HasColumnType("DATETIME2");

                // we can configure most everything about what SQL DB will be made
                // (schema name, table names, column names)

                // configuring the relationships is important
                builder.HasOne(m => m.Genre)
                       .WithMany(g => g.Movies); // here we configure "both directions"
                                                 // of navigation property.
                                                 // if we don't have an explicit foreign key property (e.g. GenreId)
                                                 //   that's perfectly fine (under the hood, a "shadow property" will
                                                 //   be made for it)
            });

            modelBuilder.Entity<Movie>().HasData(new
            {
                Id = 1,
                Title = "Star Wars VI",
                DateReleased = new DateTime(1970, 1, 1),
                GenreId = 1 // action?
            });

            modelBuilder.Entity<Genre>(builder =>
            {
                // defaults probably fine
            });

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Drama" }
            );
        }
    }
}
