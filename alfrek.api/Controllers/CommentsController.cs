using System;
using System.Linq;
using System.Threading.Tasks;
using alfrek.api.Controllers.Resources.Input;
using alfrek.api.Controllers.Resources.View;
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
        
        // GET
        [HttpGet("")]
        public async Task<IActionResult> Get(int solutionId)
        {
            var comments = await _context.Comments.Where(x => x.SolutionId == solutionId).ToListAsync();
            
            if (comments != null && comments.Count > 0)
            {
                var result = comments.Select(s => new CommentListResource()
                {
                    Id = s.Id,
                    SolutionId = s.SolutionId,
                    UserId = s.UserId,
                    CommentBody = s.CommentBody
                }).ToList();
                
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
            
        }
        
        // POST
        [HttpPost("")]
        public IActionResult Post([FromBody] SaveCommentResource s)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var comment = new Comment();
                comment.SolutionId = s.SolutionId;
                comment.UserId = s.UserId;
                comment.CommentBody = s.CommentBody;

                try
                {
                    _context.Comments.Add(comment);
                    _context.SaveChanges();
                    return Ok(comment);
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
                
                var comment = await _context.Comments.FindAsync(id);
                if (comment != null)
                {
                    comment.CommentBody = s.CommentBody;

                    try
                    {
                        _context.Update(comment);
                        _context.SaveChanges();
                        return Ok(comment);
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
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                
                var comment = await _context.Comments.FindAsync(id);
                if (comment != null)
                {
                    try
                    {
                        _context.Comments.Remove(comment);
                        _context.SaveChanges();
                        return Ok();
                    }
                    catch (Exception e)
                    {
                        return StatusCode(500);
                    }
                }
                else
                {
                    return NotFound("No comment to delete with id" + id);
                }
                
            }
        }
        
        
    }
}