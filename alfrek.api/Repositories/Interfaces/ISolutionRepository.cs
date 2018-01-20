using System.Collections.Generic;
using System.Threading.Tasks;
using alfrek.api.Models;
using alfrek.api.Models.Solutions;

namespace alfrek.api.Repositories.Interfaces
{
    public interface ISolutionRepository
    {
        Task<Solution> GetSolution(int id);
        
        Task<List<Solution>> GetSolutions();
        Task<List<Solution>> GetSolutionsByAuthor(Author author);
        Task<List<Solution>> Search(string query);
      
        Task SaveSolutionAsync(Solution solution);
        Task UpdateSolutionAsync(Solution solution);
        
        Task DeleteSolutionAsync(int id);
        
    }
}