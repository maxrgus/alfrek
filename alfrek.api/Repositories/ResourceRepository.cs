using System.Collections.Generic;
using System.Threading.Tasks;
using alfrek.api.Models.ApplicationUsers;
using alfrek.api.Persistence;
using alfrek.api.Repositories.Interfaces;
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
    }
}