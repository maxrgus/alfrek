﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace alfrek.api.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        //Properties
        [Required]
        public int UserId { get; set; }

        [Required]
        public int SolutionId { get; set; }

        [Required]
        public string CommentBody { get; set; }
        
        
        
        // Constructor
        public Comment()
        {
            
        }

        public Comment(int userId, int solutionId, string commentBody)
        {
            UserId = userId;
            SolutionId = solutionId;
            CommentBody = commentBody;
            
        }
        
        
    }
}