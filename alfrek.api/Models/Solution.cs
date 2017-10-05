using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using alfrek.api.Models.Solutions;

namespace alfrek.api.Models
{
    public class Solution
    {
        public int Id { get; set; }
        [Required]
        [StringLength(55)]
        public string Title { get; set; }
        [Required]
        [StringLength(255)]
        public string ByLine { get; set; }
        public double? Rating { get; set; }
        public int Views { get; set; }
        
        [Required]
        public ApplicationUser Author { get; set; }
        
        public List<Author> CoAuthors { get; set; }
        public List<MetaTag> Tags { get; set; }
        public FeaturedImage FeaturedImage { get; set; }

        [Required]
        public string ProblemBody { get; set; }
        [Required]
        public string SolutionBody { get; set; }

        public List<Attachment> Attachments { get; set; }
        public List<Comment> Comments { get; set; }

        public Solution()
        {
            
        }
        public Solution(string title, string byLine, double? rating, string problemBody, 
            string solutionBody)
        {
            Title = title;
            ByLine = byLine;
            Rating = rating;
            Views = 0;
            ProblemBody = problemBody;
            SolutionBody = solutionBody;
            Comments = new List<Comment>();
        }

        public Solution(int id, string title, string byLine, double? rating, ApplicationUser author, 
            List<Author> coAuthors, List<MetaTag> tags, FeaturedImage featuredImage, string problemBody, 
            string solutionBody, List<Attachment> attachments, List<Comment> comments)
        {
            Id = id;
            Title = title;
            ByLine = byLine;
            Rating = rating;
            Views = 0;
            Author = author;
            CoAuthors = coAuthors;
            Tags = tags;
            FeaturedImage = featuredImage;
            ProblemBody = problemBody;
            SolutionBody = solutionBody;
            Attachments = attachments;
            Comments = comments;
        }
    }
}