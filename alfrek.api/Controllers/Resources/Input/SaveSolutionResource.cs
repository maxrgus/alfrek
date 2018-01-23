using System.Collections.Generic;
using alfrek.api.Controllers.Resources.Input.Solutions;
using alfrek.api.Models.Solutions;

namespace alfrek.api.Controllers.Resources.Input
{
    public class SaveSolutionResource
    {
        public string Username { get; set; }
        public string Title { get; set; }
        public string ByLine { get; set; }
        public string ProblemBody { get; set; }
        public string SolutionBody { get; set; }
        public List<string> Keywords { get; set; }
        public List<PurposedRole> Roles { get; set; }
        public List<SaveAuthorsResource> CoAuthors { get; set; }

        public SaveSolutionResource()
        {
            Roles = new List<PurposedRole>();
            CoAuthors = new List<SaveAuthorsResource>();
        }

        public SaveSolutionResource(string username ,string title, string byLine, string problemBody, string solutionBody)
        {
            Username = username;
            Title = title;
            ByLine = byLine;
            ProblemBody = problemBody;
            SolutionBody = solutionBody;
        }


        public SaveSolutionResource(string username, string title, string byLine, string problemBody, 
            string solutionBody, List<SaveAuthorsResource> coAuthors)
        {
            Username = username;
            Title = title;
            ByLine = byLine;
            ProblemBody = problemBody;
            SolutionBody = solutionBody;
            CoAuthors = coAuthors;
        }
    }
}