using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alfrek.api.Extensions;
using alfrek.api.Mappers;
using alfrek.api.Models;
using alfrek.api.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace alfrek.api.Controllers
{
    [Authorize]
    [Route("researchers")]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISolutionRepository _solutionRepository;
        private readonly IResourceRepository _resourceRepository;

        public ProfileController(UserManager<ApplicationUser> userManager, IResourceRepository resourceRepository, ISolutionRepository solutionRepository)
        {
            _userManager = userManager;
            _solutionRepository = solutionRepository;
            _resourceRepository = resourceRepository;
        }

        [AllowAnonymous]
        [HttpGet("{slug}")]
        public async Task<IActionResult> Get(string slug)
        {
            //TODO: Create method that finds user by slug.
            var result = _userManager.FindBySlugAsync(slug);
            if (result == null)
            {
                return NotFound();
            }
            var profile = result.ToPublicProfileResource();
            var toBeMapped = await _solutionRepository.GetSolutionsByAuthor(result);

            profile.Solutions = toBeMapped.Select(s => s.ToListSolution()).ToList();

            return Ok(profile);
        }
    }
}