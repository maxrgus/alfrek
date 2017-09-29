namespace alfrek.api.Controllers.Resources.Input
{
    public class EditCommentResource
    {
        public string CommentBody { get; set; }

        public EditCommentResource(string commentBody)
        {
            CommentBody = commentBody;
        }
    }
}