namespace alfrek.api.Controllers.Resources.View
{
    public class SolutionPreviewResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ByLine { get; set; }
        public double? Rating { get; set; }

        public string ProblemBody { get; set; }

        public SolutionPreviewResource()
        {
            
        }
        public SolutionPreviewResource(int id, string title, string byLine, double? rating, string problemBody)
        {
            Id = id;
            Title = title;
            ByLine = byLine;
            Rating = rating;
            ProblemBody = problemBody;
        }
    }
}