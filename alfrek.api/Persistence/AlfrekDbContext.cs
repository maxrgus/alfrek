using alfrek.api.Models;
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
    }
}