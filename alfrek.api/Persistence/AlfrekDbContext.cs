using alfrek.api.Models;
using Microsoft.EntityFrameworkCore;

namespace alfrek.api.Persistence
{
    public class AlfrekDbContext : DbContext
    {
        public AlfrekDbContext(DbContextOptions<AlfrekDbContext> options)
            : base(options)
        {
            
        }
        
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}