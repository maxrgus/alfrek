namespace alfrek.api.Controllers.Resources.View
{
    public class SolutionListResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ByLine { get; set; }

        public SolutionListResource(int id, string title, string byLine)
        {
            Id = id;
            Title = title;
            ByLine = byLine;
        }
    }
    
    
}