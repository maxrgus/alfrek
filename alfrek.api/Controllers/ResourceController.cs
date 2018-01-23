using System.Threading.Tasks;
using alfrek.api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace alfrek.api.Controllers
{
    [Route("[controller]")]
    public class ResourceController : Controller
    {
        private readonly IResourceRepository _repository;

        public ResourceController(IResourceRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet("affiliations")]
        public async Task<IActionResult> Affiliations()
        {
            var result = await _repository.GetAffiliationsAsync();
            return Ok(result);
        }
        [HttpGet("purposedroles")]
        public async Task<IActionResult> PurposedRoles()
        {
            var result = await _repository.GetPurposedRolesAsync();
            return Ok(result);
        }
    }
}