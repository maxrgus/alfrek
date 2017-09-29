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
    [Route("/[controller]")]
    public class CommentsController : Controller
    {
        private readonly AlfrekDbContext _context;

        public CommentsController(AlfrekDbContext context)
        {
            _context = context;
        }
        
        // GET
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var comments = await _context.Comments.Where(x => x.SolutionId == id).ToListAsync();
            
            if (comments != null && comments.Count > 0)
            {
                var result = comments.Select(s => new CommentListResource()
                {
                    Id = s.Id,
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
        [HttpPost("{id}")]
        public IActionResult Post([FromBody] SaveCommentResource s)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var comment = new Comment();
                comment.UserId = s.UserId;
                comment.SolutionId = s.SolutionId;
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
        public async Task<IActionResult> Put(int commentId, string commentBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                
                var comment = await _context.Comments.FindAsync(commentId);
                if (comment != null)
                {
                    comment.CommentBody = commentBody;

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
        public async Task<IActionResult> Delete(int commentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                
                var comment = await _context.Comments.FindAsync(commentId);
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
                    return NotFound("No comment to delete");
                }
                
            }
        }
        
        
    }
}