using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alfrek.api.Authorization;
using alfrek.api.Controllers.Resources.Input;
using alfrek.api.Controllers.Resources.View;
using alfrek.api.Controllers.Resources.View.Solutions;
using alfrek.api.Mappers;
using alfrek.api.Models;
using alfrek.api.Models.Solutions;
using alfrek.api.Persistence;
using alfrek.api.Repositories.Interfaces;
using alfrek.api.Storage.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace alfrek.api.Controllers
{
    //TODO: Refactor all mapping to external mapper
    [Authorize]
    [Route("[controller]")]
    public class SolutionsController : Controller
    {
        private readonly ISolutionRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly ICloudStorage _cloudStorage;


        public SolutionsController(UserManager<ApplicationUser> userManager, 
            IAuthorizationService authorizationService, ISolutionRepository repository, ICloudStorage cloudStorage)
        {
            _userManager = userManager;
            _authorizationService = authorizationService;
            _repository = repository;
            _cloudStorage = cloudStorage;
        }
        
        [AllowAnonymous]
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var solutions = await _repository.GetSolutions();
            // Map returned solutions to SolutionListResource
            var result = solutions.Select(s => s.ToListSolution()).ToList();    
            return Ok(result);

        }
        
        [Authorize(Roles = "Researcher,Member")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var solution = await _repository.GetSolution(id);
            if (solution == null)
            {
                return NotFound();
            }
            var result = solution.ToSingleSolution();
            return Ok(result);
            
        }
        [AllowAnonymous]
        [HttpGet("preview/{id}")]
        public async Task<IActionResult> GetPreview(int id)
        {
            var solution = await _repository.GetSolution(id);
            if (solution == null)
            {
                return NotFound();
            }
            var result = solution.ToPreviewSolution();
            return Ok(result);

        }
        [AllowAnonymous]
        [HttpGet("search")]
        public async Task<IActionResult> Search(string query)
        {
            var results = await _repository.Search(query);
            if (results == null)
            {
                results = new List<Solution>();
            }
            return Ok(results);
        }
        [Authorize(Roles = "Researcher")]
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] SaveSolutionResource s)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }
            var author = await _userManager.FindByEmailAsync(s.Username);
            if (author == null)
            {
                return BadRequest(ModelState);
            }
            
            var solution = s.FromSaveSolutionResource();
            solution.Author = author;            
            try
            {
                await _repository.SaveSolutionAsync(solution);
                return Ok(solution);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }
        [Authorize(Roles = "Researcher")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditSolutionResource s)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }         
            var solution = await _repository.GetSolution(id);
            if (solution == null)
            {
                return NotFound("No solution found");

            }
            //TODO: Refactor to Policy
            var auth = await _authorizationService.AuthorizeAsync(User, solution, Operations.Update);
            if (!auth.Succeeded)
            {
                return Challenge();
            }
            solution.MapFromEditSolutionResource(s); 
            try
            {
                await _repository.UpdateSolutionAsync(solution);
                return Ok(solution);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _repository.DeleteSolutionAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
            
        }

        [AllowAnonymous]
        [HttpGet("buckets")]
        public IActionResult Buckets()
        {
            _cloudStorage.ListStorage();
            return Ok();
        }
    }
}