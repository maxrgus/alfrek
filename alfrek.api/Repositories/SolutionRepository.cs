using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alfrek.api.Models;
using alfrek.api.Models.Solutions;
using alfrek.api.Persistence;
using alfrek.api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace alfrek.api.Repositories
{
    public class SolutionRepository : ISolutionRepository
    {
        private readonly AlfrekDbContext _context;

        public SolutionRepository(AlfrekDbContext context)
        {
            _context = context;
        }

        public async Task<Solution> GetSolution(int id)
        {
            return await _context.Solutions
                .Include(s => s.Comments)
                .Include(s => s.Author)
                .Include(s => s.CoAuthors)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Solution>> GetSolutions()
        {
            return await _context.Solutions.ToListAsync();
        }

        public async Task<List<Solution>> GetSolutionsByAuthor(Author author)
        {
            return await _context.Solutions.Where(a => a.Author.Email == author.Email).ToListAsync();
        }

        public async Task SaveSolutionAsync(Solution solution)
        {
            _context.Solutions.Add(solution);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSolutionAsync(Solution solution)
        {
            _context.Solutions.Update(solution);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSolutionAsync(int id)
        {
            var solution = await _context.Solutions.FindAsync(id);
            if (solution != null)
            {
                _context.Solutions.Remove(solution);
                await _context.SaveChangesAsync();
            }
        }
    }
}