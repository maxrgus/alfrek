using alfrek.api.Models;
using alfrek.api.Models.ApplicationUsers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace alfrek.api.Persistence
{
    public class AlfrekDbContext : IdentityDbContext<ApplicationUser>
    {
        public AlfrekDbContext(DbContextOptions<AlfrekDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<Affiliation> Affiliations { get; set; }
    }
}