using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alfrek.api.Mappers;
using alfrek.api.Models;
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

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var result =  await _userManager.FindByEmailAsync(email);
            if (result == null)
            {
                return NotFound();
            }
            var profile = result.ToPublicProfileResource();
            return Ok(profile);
        }
    }
}