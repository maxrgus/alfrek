using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using alfrek.api.Models.Joins;
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
        public DateTime CreatedAt { get; set; }

        [Required]
        public ApplicationUser Author { get; set; }
        
        public List<Author> CoAuthors { get; set; }
        public List<MetaTag> Tags { get; set; }

        public List<SolutionRole> SolutionRoles { get; set; }

        public FeaturedImage FeaturedImage { get; set; }

        public string Slug { get; set; }

        [Required]
        public string ProblemBody { get; set; }
        [Required]
        public string SolutionBody { get; set; }

        public List<Attachment> Attachments { get; set; }
        public List<Comment> Comments { get; set; }

        public Solution()
        {
            CoAuthors = new List<Author>();
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
            CoAuthors = new List<Author>();
            SolutionRoles = new List<SolutionRole>();
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