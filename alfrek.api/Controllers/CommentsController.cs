using System;
using System.Linq;
using System.Threading.Tasks;
using alfrek.api.Controllers.Resources.Input;
using alfrek.api.Controllers.Resources.View;
using alfrek.api.Migrations;
using alfrek.api.Models;
using alfrek.api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace alfrek.api.Controllers
{
    [Route("{solutionId}/[controller]")]
    public class CommentsController : Controller
    {
        private readonly AlfrekDbContext _context;

        public CommentsController(AlfrekDbContext context)
        {
            _context = context;
        }
        
        
        // POST
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] SaveCommentResource s)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var solution = await _context.Solutions.Include(y => y.Comments).SingleOrDefaultAsync(x => x.Id == s.SolutionId);
                
                solution.Comments.Add(new Comment(s.UserId, s.SolutionId, s.CommentBody));
                
                try
                {
                    _context.Solutions.Update(solution);
                    _context.SaveChanges();
                    return Ok();
                }
                catch (Exception e)
                {
                    return StatusCode(500);
                }
            }
            
        }
        
        // EDIT

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditCommentResource s)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                
                var solution = await _context.Solutions.Include(y => y.Comments).SingleOrDefaultAsync(x => x.Id == s.SolutionId);
                if (solution != null)
                {

                    foreach (var c in solution.Comments)
                    {
                        if (c.Id == id)
                        {
                            c.CommentBody = s.CommentBody;
                        }
                    }
                   
                    try
                    {
                        _context.Update(solution);
                        _context.SaveChanges();
                        return Ok();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return StatusCode(500);
                    }
                }
                else
                {
                    return NotFound("No comment to update");
                }
                
            }
        }
        
        
        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int solutionId, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var solution = await _context.Solutions.Include(y => y.Comments).SingleOrDefaultAsync(x => x.Id == solutionId);
                
                foreach (var c in solution.Comments)
                {
                    if (c.Id == id)
                    {
                        solution.Comments.Remove(c);
                        try
                        {
                            _context.SaveChanges();
                            return Ok();
                        }
                        catch (Exception e)
                        {
                            return StatusCode(500);
                        }
                    }
                }
                return StatusCode(500);
             
            }
        }
       
    }
}