using System;
using System.Linq;
using System.Threading.Tasks;
using alfrek.api.Controllers.Resources.Input;
using alfrek.api.Controllers.Resources.View;
using alfrek.api.Models;
using alfrek.api.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace alfrek.api.Controllers
{
    [Route("[controller]")]
    public class SolutionsController : Controller
    {
        private readonly AlfrekDbContext _context;

        public SolutionsController(AlfrekDbContext context)
        {
            _context = context;
        }
        
        [Authorize]
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
            var solution = await _context.Solutions.FindAsync(id);
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
                    solution.SolutionBody);

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
        public IActionResult Post([FromBody] SaveSolutionResource s)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var solution = new Solution();
                solution.Title = s.Title;
                solution.ByLine = s.ByLine;
                solution.ProblemBody = s.ProblemBody;
                solution.SolutionBody = s.SolutionBody;
                solution.Views = 0;
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