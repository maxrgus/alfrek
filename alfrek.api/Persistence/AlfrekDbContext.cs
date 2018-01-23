using alfrek.api.Models;
using alfrek.api.Models.ApplicationUsers;
using alfrek.api.Models.Joins;
using alfrek.api.Models.Solutions;
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
        public DbSet<PurposedRole> PurposedRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SolutionRole>()
                .HasKey(t => new { t.SolutionId, t.PurposedRoleId });

            builder.Entity<SolutionRole>()
                .HasOne(sr => sr.Solution)
                .WithMany(s => s.SolutionRoles)
                .HasForeignKey(sr => sr.SolutionId);

            builder.Entity<SolutionRole>()
                .HasOne(sr => sr.PurposedRole)
                .WithMany(r => r.SolutionRoles)
                .HasForeignKey(sr => sr.PurposedRoleId);
        }
    }
}