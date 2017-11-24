using System.Collections.Generic;
using System.Threading.Tasks;
using alfrek.api.Models.ApplicationUsers;
using alfrek.api.Models.Solutions;

namespace alfrek.api.Repositories.Interfaces
{
    public interface IResourceRepository
    {
        Task<Affiliation> GetAffiliationAsync(int id);
        Task<List<Affiliation>> GetAffiliationsAsync();
    }
}