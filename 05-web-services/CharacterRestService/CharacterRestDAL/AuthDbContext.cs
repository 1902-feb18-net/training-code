using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CharacterRestDAL
{
    public class AuthDbContext : IdentityDbContext
    {
        // the parent class already has all the right dbsets etc

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        { }
    }
}
