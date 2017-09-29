using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
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

//        public List<Author> Authors { get; set; }
//        public List<MetaTag> Tags { get; set; }
//        public FeaturedImage FeaturedImage { get; set; }

        [Required]
        public string ProblemBody { get; set; }
        [Required]
        public string SolutionBody { get; set; }

//        public List<Attachment> Attachments { get; set; }

        public List<Comment> Comments { get; set; }

        public Solution()
        {
            
        }
        public Solution(string title, string byLine, double? rating, int views, string problemBody, 
            string solutionBody)
        {
            Title = title;
            ByLine = byLine;
            Rating = rating;
            Views = views;
            ProblemBody = problemBody;
            SolutionBody = solutionBody;
            Comments = new List<Comment>();
        }
    }
}