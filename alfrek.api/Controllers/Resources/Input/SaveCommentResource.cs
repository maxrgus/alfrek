namespace alfrek.api.Controllers.Resources.Input
{
    public class SaveCommentResource
    {
        public int SolutionId { get; set; }
        public int UserId { get; set; }
        public string CommentBody { get; set; }

        public SaveCommentResource(int solutionId, int userId, string commentBody)
        {
            SolutionId = solutionId;
            UserId = userId;
            CommentBody = commentBody;
        }
    }    
}

