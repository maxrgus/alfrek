using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public async Task<Solution> GetSolutionBySlug(string slug)
        {
            return await _context.Solutions
                .Include(s => s.Comments)
                .Include(s => s.Author)
                    .ThenInclude(a => a.Affiliation)
                .Include(s => s.CoAuthors)
                .Include(s => s.Tags)
                .SingleOrDefaultAsync(x => x.Slug == slug);
        }

        public async Task<List<Solution>> GetSolutions()
        {
            return await _context.Solutions
                .Include(s => s.Author)
                    .ThenInclude(a => a.Affiliation)
                .Include(s => s.SolutionRoles)
                    .ThenInclude(sr => sr.PurposedRole)
                .ToListAsync();
        }

        public async Task<List<Solution>> GetLatestSolutions()
        {
            return await _context.Solutions
                .Include(s => s.Author)
                    .ThenInclude(a => a.Affiliation)
                .Include(s => s.SolutionRoles)
                    .ThenInclude(sr => sr.PurposedRole)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Solution>> Search(string query)
        {
            return await _context.Solutions.Where(s =>
                s.Title.Contains(query) ||
                s.ByLine.Contains(query)).ToListAsync();
        }

        public async Task<List<Solution>> GetSolutionsByAuthor(ApplicationUser author)
        {
            return await _context.Solutions
                .Include(s => s.Author)
                    .ThenInclude(a => a.Affiliation)
                .Include(s => s.SolutionRoles)
                    .ThenInclude(sr => sr.PurposedRole)
                .Where(a => a.Author.Email == author.Email)
                .ToListAsync();
        }

        public async Task SaveSolutionAsync(Solution solution)
        {
            var slug = ToSlug(solution.Title);
            int count = 1;
            while (true)
            {
                if (!SlugExists(slug)) {
                    break;
                }

                slug = ToSlug(solution.Title) + "-" + count;
                count++;
            }
            solution.Slug = slug;

            solution.Tags = new List<MetaTag>();
            solution.Tags.Add(new MetaTag("title", solution.Title));
            solution.Tags.Add(new MetaTag("description", solution.ByLine));

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

        private static string ToSlug(string str)
        {
            str = str.ToLowerInvariant();

            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(str);
            str = Encoding.ASCII.GetString(bytes);

            str = Regex.Replace(str, @"\s", "-", RegexOptions.Compiled);
            str = Regex.Replace(str, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);

            str = str.Trim('-', '_');

            str = Regex.Replace(str, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return str;

        }

        private bool SlugExists(string slug)
        {
            var solution = _context.Solutions.Where(s => s.Slug == slug).SingleOrDefault();
            if (solution == null)
            {
                return false;
            } else
            {
                return true;
            }

        }
    }
}