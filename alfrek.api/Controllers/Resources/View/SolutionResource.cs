using System.Collections.Generic;
using alfrek.api.Controllers.Resources.View.Solutions;
using alfrek.api.Models;

namespace alfrek.api.Controllers.Resources.View
{
    public class SolutionResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ByLine { get; set; }
        public double? Rating { get; set; }

        public string ProblemBody { get; set; }
        public string SolutionBody { get; set; }

        public AuthorResource Author { get; set; }

        public List<AuthorResource> CoAuthors { get; set; }
        
        public List<Comment> Comments { get; set; }

        public SolutionResource(int id, string title, string byLine, double? rating, string problemBody, 
            string solutionBody, List<Comment> comments)
        {
            Id = id;
            Title = title;
            ByLine = byLine;
            Rating = rating;
            ProblemBody = problemBody;
            SolutionBody = solutionBody;
            Comments = comments;
            Author = new AuthorResource();
            CoAuthors = new List<AuthorResource>();
        }
    }
}