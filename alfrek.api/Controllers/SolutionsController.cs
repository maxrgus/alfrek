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
       // private readonly ICloudStorage _cloudStorage;


        public SolutionsController(UserManager<ApplicationUser> userManager, 
            IAuthorizationService authorizationService, ISolutionRepository repository)
        {
            _userManager = userManager;
            _authorizationService = authorizationService;
           // _cloudStorage = cloudStorage;
            _repository = repository;
        }
        
        [Authorize(Roles = "Researcher,Member,Admin")]
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var solutions = await _repository.GetSolutions();
            // Map returned solutions to SolutionListResource
            var result = solutions.Select(s => new SolutionListResource()
            {
                Id = s.Id,
                Title = s.Title,
                ByLine = s.ByLine
            }).ToList();
            
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
            else
            {
                var result = new SolutionResource(
                    solution.Id,
                    solution.Title, 
                    solution.ByLine, 
                    solution.Rating, 
                    solution.ProblemBody, 
                    solution.SolutionBody,
                    solution.Comments
                    );

                result.Author.Email = solution.Author.Email;
                result.Author.Name = solution.Author.UserName;

                result.CoAuthors = solution.CoAuthors.Select(a => new AuthorResource()
                {
                    Email = a.Email,
                    Name = a.Name
                }).ToList();
                

                return Ok(result);
            }
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
            else
            {
                var result = solution.ToPreviewSolution();
                return Ok(result);
            }
        }
        [Authorize(Roles = "Researcher")]
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] SaveSolutionResource s)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }
            else
            {
                var solution = new Solution();
                solution.Title = s.Title;
                solution.ByLine = s.ByLine;
                solution.ProblemBody = s.ProblemBody;
                solution.SolutionBody = s.SolutionBody;
                solution.Views = 0;

                var author = await _userManager.FindByEmailAsync(s.Username);
                if (author == null)
                {
                    return BadRequest(ModelState);
                }

                solution.Author = author;
                
                foreach (var coAuthor in s.CoAuthors)
                {
                    solution.CoAuthors.Add(new Author(coAuthor.Email, coAuthor.Name));
                }
                
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
        }
        [Authorize(Roles = "Researcher")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditSolutionResource s)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var solution = await _repository.GetSolution(id);
                if (solution != null)
                {
                    var auth = await _authorizationService.AuthorizeAsync(User, solution, Operations.Update);
                    if (!auth.Succeeded)
                    {
                        return Challenge();
                    }
                    
                    solution.Title = s.Title;
                    solution.ByLine = s.ByLine;
                    solution.ProblemBody = s.ProblemBody;
                    solution.SolutionBody = solution.SolutionBody;
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
                else
                {
                    return NotFound("No solution found");
                }
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
            //_cloudStorage.ListStorage();
            return Ok();
        }
    }
}