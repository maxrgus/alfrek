using System.Data.Common;

namespace alfrek.api.Controllers.Resources.Input
{
    public class EditCommentResource
    {
        public int Id { get; set; }
        public int SolutionId { get; set; }
        public string CommentBody { get; set; }

        public EditCommentResource(int id, int solutionId, string commentBody)
        {
            Id = id; 
            SolutionId = solutionId;
            CommentBody = commentBody;
        }
    }
}