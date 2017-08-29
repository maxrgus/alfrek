namespace alfrek.api.Controllers.Resources.Input
{
    public class SaveSolutionResource
    {
        public string Title { get; set; }
        public string ByLine { get; set; }
        public string ProblemBody { get; set; }
        public string SolutionBody { get; set; }

        public SaveSolutionResource(string title, string byLine, string problemBody, string solutionBody)
        {
            Title = title;
            ByLine = byLine;
            ProblemBody = problemBody;
            SolutionBody = solutionBody;
        }
    }
}