﻿using System.Collections.Generic;
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

        public List<Comment> Comments { get; set; }

        public SolutionResource()
        {
            
        }
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
        }
    }
}