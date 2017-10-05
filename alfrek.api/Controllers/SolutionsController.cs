using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alfrek.api.Controllers.Resources.Input;
using alfrek.api.Controllers.Resources.View;
using alfrek.api.Controllers.Resources.View.Solutions;
using alfrek.api.Models;
using alfrek.api.Models.Solutions;
using alfrek.api.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace alfrek.api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class SolutionsController : Controller
    {
        private readonly AlfrekDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public SolutionsController(AlfrekDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {   
            var solutions = await _context.Solutions.ToListAsync();
            // Map returned solutions to SolutionListResource
            var result = solutions.Select(s => new SolutionListResource()
            {
                Id = s.Id,
                Title = s.Title,
                ByLine = s.ByLine
            }).ToList();

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var solution = await _context.Solutions
                .Include(s => s.Comments)
                .Include(s => s.Author)
                .Include(s => s.CoAuthors)
                .SingleOrDefaultAsync(x => x.Id == id);
            
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

        [HttpGet("preview/{id}")]
        public async Task<IActionResult> GetPreview(int id)
        {
            var solution = await _context.Solutions.FindAsync(id);
            if (solution == null)
            {
                return NotFound();
            }
            else
            {
                var result = new SolutionPreviewResource(
                    solution.Id,
                    solution.Title, 
                    solution.ByLine, 
                    solution.Rating, 
                    solution.ProblemBody);

                return Ok(result);
            }
        }

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
                    _context.Solutions.Add(solution);
                    _context.SaveChanges();
                    return Ok(solution);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return StatusCode(500);
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditSolutionResource s)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var solution = await _context.Solutions.FindAsync(id);
                if (solution != null)
                {
                    solution.Title = s.Title;
                    solution.ByLine = s.ByLine;
                    solution.ProblemBody = s.ProblemBody;
                    solution.SolutionBody = solution.SolutionBody;
                    try
                    {
                        _context.Update(solution);
                        _context.SaveChanges();
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var solution = await _context.Solutions.FindAsync(id);
            if (solution == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    _context.Solutions.Remove(solution);
                    _context.SaveChanges();
                    return Ok(solution);
            
                }
                catch (Exception e)
                {
                    return StatusCode(500);
                }
            }
            
        }
    }
}