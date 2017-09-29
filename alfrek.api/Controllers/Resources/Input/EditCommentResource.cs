namespace alfrek.api.Controllers.Resources.Input
{
    public class EditCommentResource
    {
        public int Id { get; set; }
        public string CommentBody { get; set; }

        public EditCommentResource(int id, string commentBody)
        {
            Id = id;
            CommentBody = commentBody;
        }
    }
}