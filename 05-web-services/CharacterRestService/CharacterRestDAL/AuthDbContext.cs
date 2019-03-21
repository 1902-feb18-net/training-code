using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CharacterRestDAL
{
    public class AuthDbContext : IdentityDbContext
    {
        // the parent class already has all the right dbsets etc
        
        public AuthDbContext(DbContextOptions options) : base(options)
        { }
    }
}
