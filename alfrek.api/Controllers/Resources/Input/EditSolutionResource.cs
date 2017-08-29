namespace alfrek.api.Controllers.Resources.Input
{
    public class EditSolutionResource
    {
        public string Title { get; set; }
        public string ByLine { get; set; }
        public string ProblemBody { get; set; }
        public string SolutionBody { get; set; }

        public EditSolutionResource(string title, string byLine, string problemBody, string solutionBody)
        {
            Title = title;
            ByLine = byLine;
            ProblemBody = problemBody;
            SolutionBody = solutionBody;
        }
    }
}