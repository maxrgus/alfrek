using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alfrek.api.Migrations.UserDb;
using alfrek.api.Models.ApplicationUsers;
using alfrek.api.Models.Solutions;
using alfrek.api.Persistence;
using alfrek.api.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace alfrek.api.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly AlfrekDbContext _context;

        public ResourceRepository(AlfrekDbContext context)
        {
            _context = context;
        }

        public async Task<Affiliation> GetAffiliationAsync(int id)
        {
            return await _context.Affiliations.FindAsync(id);
        }

        public async Task<List<Affiliation>> GetAffiliationsAsync()
        {
            return await _context.Affiliations.ToListAsync();
        }
        
        public async Task<List<PurposedRole>> GetPurposedRolesAsync()
        {
            return await _context.PurposedRoles.ToListAsync();
        }

        public async Task<List<PurposedRole>> GetPurposedRolesAsync(int[] ids)
        {
            return await _context.PurposedRoles.Where(r => ids.Contains(r.Id)).ToListAsync();
        }
    }
}