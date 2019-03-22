using CharacterRest;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterRestDAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>(builder =>
            {
                builder.Property(c => c.Name)
                    .HasMaxLength(100)
                    .IsRequired();
            });

            modelBuilder.Entity<Character>().HasData(new List<Character>
            {
                new Character { Id = 1, Name = "Nick" },
                new Character { Id = 2, Name = "Fred" }
            });
        }
    }
}
