using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using alfrek.api.Controllers.Resources.Input;
using alfrek.api.Models;
using alfrek.api.Models.ApplicationUsers;
using alfrek.api.Persistence;
using alfrek.api.Repositories.Interfaces;
using alfrek.api.Services;
using alfrek.api.Services.Interfaces;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace alfrek.api.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IResourceRepository _repository;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
            ITokenService tokenService, IResourceRepository repository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _repository = repository;
        }

        [Authorize]
        [HttpGet("user")]
        public IActionResult GetUser()
        {
            return Ok(new { Email = User.FindFirstValue(ClaimTypes.NameIdentifier) });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new ApplicationUser
            {
                UserName = resource.Email,
                Email = resource.Email,
            };
            

            var result = await _userManager.CreateAsync(user, resource.Password);
            
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(x => x.Description).ToList());
            }
            
            await _userManager.AddToRoleAsync(user, "Member");

            await _signInManager.SignInAsync(user, false);

            return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(await _tokenService.GetToken(user))});

        }


        [HttpPost("register/researcher")]
        public async Task<IActionResult> RegisterResearcher([FromBody] RegisterResearcherResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var user = new ApplicationUser
            {
                FirstName = resource.FirstName,
                LastName = resource.LastName,
                UserName = resource.Email,
                Email = resource.Email,
                ResearchField = resource.Research
            };

            user.Affiliation = await _repository.GetAffiliationAsync(resource.Affiliation.Id);
            
            var result = await _userManager.CreateAsync(user, resource.Password);
    
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(x => x.Description).ToList());
            }

            await _userManager.AddToRoleAsync(user, "Researcher");
            
            await _signInManager.SignInAsync(user, false);

            return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(await _tokenService.GetToken(user))});
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(resource.Email, resource.Password, false, false);

            if (!result.Succeeded)
            {
                return BadRequest("We could not find any user with that email or password");
            }

            var user = await _userManager.FindByEmailAsync(resource.Email);

            return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(await _tokenService.GetToken(user))});


        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Signed out");
        }
    }
}